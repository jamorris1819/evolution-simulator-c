﻿using Engine.Core;
using Engine.Core.Components;
using Engine.Render.Core;
using Engine.Render.Core.Shaders;
using Engine.Render.Core.Shaders.Enums;
using Engine.Render.Core.VAO;
using Engine.Render.Events;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Render
{
    public class RenderSystem : SystemBase, ISystem
    {
        private HashSet<Guid> _registeredEntities;
        private Dictionary<int, Dictionary<int, List<Entity>>> _renderComponents;
        private FrameBufferObject _mainFBO;
        private FrameBufferRenderer _fboRenderer;

        private IEventBus _eventBus;
        private Camera _camera;

        public RenderSystem(IEventBus eventBus)
        {
            _eventBus = eventBus;

            Mask = ComponentType.COMPONENT_RENDER | ComponentType.COMPONENT_TRANSFORM;

            _eventBus.Subscribe<CameraChangeEvent>(x => _camera = x.Camera);

            _registeredEntities = new HashSet<Guid>();
            _renderComponents = new Dictionary<int, Dictionary<int, List<Entity>>>();

            Shaders.Initialise();

            _mainFBO = new FrameBufferObject();
            _mainFBO.Initialise();
            _mainFBO.Bind();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            _fboRenderer = new FrameBufferRenderer();
            _fboRenderer.Load();
        }

        public override void OnRender()
        {
            _mainFBO.Bind();
            _mainFBO.Clear();

            GL.Enable(EnableCap.MultisampleSgis);

            // Order the sorting layers
            var sortingLayers = _renderComponents.Keys.OrderBy(x => x).ToArray();

            for (int i = 0; i < sortingLayers.Length; i++)
            {
                RenderSortingLayer(sortingLayers[i]);
            }

            GL.Disable(EnableCap.DepthTest);

            _fboRenderer.Render(_mainFBO);


            //_mainFBO.Bind();
        }

        public void RenderSortingLayer(int sortingLayer)
        {
            var layers = _renderComponents[sortingLayer].Keys.OrderBy(x => x).ToArray();

            for (int i = 0; i < layers.Length; i++)
            {
                RenderLayer(sortingLayer, layers[i]);
            }
        }

        /// <summary>
        /// Renders all the tracked entities on the given layer.
        /// </summary>
        private void RenderLayer(int sortingLayer, int layer)
        {
            if (!_renderComponents[sortingLayer].ContainsKey(layer)) return;

            var toRender = _renderComponents[sortingLayer][layer];

            for (int i = 0; i < toRender.Count; i++)
            {
                RenderEntity(toRender[i], sortingLayer);
            }
        }

        private void RenderEntity(Entity entity, int sortingLayer)
        {
            var positionComponent = entity.GetComponent<TransformComponent>();
            var renderComponent = entity.GetComponent<RenderComponent>();

            // Ensure this entity should be drawn.
            if (positionComponent == null || renderComponent == null) return;
            if (!IsEntityInView(entity)) return;
            if (!IsVAOReady(renderComponent)) return;

            // Ensure there are no critical issues.
            if (renderComponent.Shaders.Count == 0) return; // throw new RenderException("An entity must have at least 1 shader in order to be rendered");

            // Calculate the matrix to render at.
            var entityPosition = GetWorldPosition(entity);
            var matrix = Matrix4.CreateRotationZ(GetWorldAngle(entity)) * Matrix4.CreateTranslation(new Vector3(entityPosition.X, entityPosition.Y, 0));

            var shadersToUse = renderComponent.Shaders.Where(x => x.SortingLayer == sortingLayer).ToArray();

            for (int i = 0; i < shadersToUse.Length; i++)
            {
                RenderEntityWithShader(renderComponent, matrix, shadersToUse[i]);
            }
        }

        private void RenderEntityWithShader(RenderComponent renderComponent, Matrix4 matrix, ShaderConfiguration shaderConfig)
        {
            // Load shaders.
            Shader desiredShader = PrimeShader(shaderConfig.MainShader, matrix, renderComponent.Alpha);

            // Set opengl flags.
            EnableGLFeaturesPredraw(shaderConfig.StencilWrite || shaderConfig.StencilRead);

            if (shaderConfig.StencilWrite)
            {
                GL.StencilFunc(StencilFunction.Always, 1, 0xFF);
                GL.StencilMask(0xFF);
            }

            if (shaderConfig.StencilRead)
            {
                GL.StencilFunc(StencilFunction.Notequal, 1, 0xFF);
                GL.StencilMask(0x00);

                GL.Disable(EnableCap.DepthTest);
            }

            desiredShader.Bind();

            // Bind and render the shape.
            renderComponent.VertexArrayObject.Bind();
            renderComponent.VertexArrayObject.Render(desiredShader);

            // If we wanted an outline, then disable writing to stencil buffer and draw with the outline shader.
            if (shaderConfig.StencilRead)
            {
                GL.StencilMask(0xFF);
                GL.StencilFunc(StencilFunction.Always, 1, 0xFF);
            }
        }

        /// <summary>
        /// Enables the required OpenGL flags required for rendering.
        /// </summary>
        /// <param name="useStencilBuffer">Whether a stencil buffer should be used</param>
        private void EnableGLFeaturesPredraw(bool useStencilBuffer)
        {
            GL.Enable(EnableCap.Blend);

            if (useStencilBuffer)
            {
                GL.Enable(EnableCap.StencilTest);
                GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
            }
            else
            {
                GL.StencilMask(0);
                GL.Disable(EnableCap.StencilTest);
                GL.Disable(EnableCap.DepthTest);
            }

            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.DepthFunc(DepthFunction.Less);
        }

        private bool IsEntityInView(Entity entity) => true; // TODO: implement

        private bool IsVAOReady(RenderComponent component) => component.VertexArrayObject.Initialised;

        /// <summary>
        /// Loads the shader and primes it for use by setting uniform values.
        /// </summary>
        private Shader PrimeShader(Shader shader, Matrix4 model, float alpha)
        {
            shader.Bind();
            shader.SetUniformMat4("uModel", model);
            shader.SetUniform("alpha", alpha);

            return shader;
        }

        /// <summary>
        /// Loads the shader and primes it for use by setting uniform values.
        /// </summary>
        private Shader PrimeShader(Shader shaderType, Matrix4 model) => PrimeShader(shaderType, model, 1f);

        /// <summary>
        /// Retrieves the world position of the given entity.
        /// </summary>
        private Vector2 GetWorldPosition(Entity entity)
        {
            var position = entity.GetComponent<TransformComponent>().Position;

            if (entity.Parent == null) return position;

            var parentPosComp = entity.Parent.GetComponent<TransformComponent>();
            var parentRotation = parentPosComp.Angle;

            var rotPos = Rotate(position, parentRotation);

            return rotPos + GetWorldPosition(entity.Parent);
        }

        private static Vector2 Rotate(Vector2 v, float rad)
        {
            float sin = (float)Math.Sin(rad);
            float cos = (float)Math.Cos(rad);

            float tx = v.X;
            float ty = v.Y;
            v.X = (cos * tx) - (sin * ty);
            v.Y = (sin * tx) + (cos * ty);
            return v;
        }

        private float GetWorldAngle(Entity entity)
        {
            var angle = entity.GetComponent<TransformComponent>().Angle;

            if (entity.Parent == null) return angle;

            return angle + GetWorldAngle(entity.Parent);
        }

        public override void OnUpdate(Entity entity, float deltaTime)
        {
            if (!MaskMatch(entity)) return;

            // Track it if we're not already
            if (!_registeredEntities.Contains(entity.Id))
                AddEntityToTracking(entity);

            RenderComponent comp = entity.GetComponent<RenderComponent>();

            if(comp.ZoomProfile.HasValue)
            {
                var alpha = comp.ZoomProfile.Value.GetAlpha(_camera.Scale);
                comp.Alpha = alpha;
            }

            if (!comp.VertexArrayObject.Initialised)
            {
                comp.VertexArrayObject.Initialise(Shaders.All);
                comp.VertexArrayObject.Load();
            }

            if (comp.VertexArrayObject.NeedsUpdate)
            {
                comp.VertexArrayObject.Reload();
            }
        }

        /// <summary>
        /// Adds the entity to the tracking system.
        /// </summary>
        private void AddEntityToTracking(Entity entity)
        {
            // Check if we are already tracking
            if (_registeredEntities.Contains(entity.Id)) return;

            var renderComponent = entity.GetComponent<RenderComponent>();
            int layer = renderComponent.Layer;

            if (renderComponent.Layer == 1)
            {
                var a = "";

            }

            int[] sortingLayers = renderComponent.Shaders.Select(x => x.SortingLayer).Distinct().ToArray();

            // Check that we are currently rendering at this sorting layer
            for (int i = 0; i < sortingLayers.Length; i++)
            {
                if (!_renderComponents.ContainsKey(sortingLayers[i]))
                {
                    _renderComponents.Add(sortingLayers[i], new Dictionary<int, List<Entity>>());
                }

                // Add layer if doesn't exist.
                if (!_renderComponents[sortingLayers[i]].ContainsKey(renderComponent.Layer))
                {
                    _renderComponents[sortingLayers[i]].Add(renderComponent.Layer, new List<Entity>());
                }

                // Register the layer
                _renderComponents[sortingLayers[i]][renderComponent.Layer].Add(entity);
            }

            // Register the entity
            _registeredEntities.Add(entity.Id);
        }

        private bool InView(Vector2 pos)
        {
            var screenPosMin = _camera.ScreenToWorld(new Vector2(0, 0));
            var screenPosMax = _camera.ScreenToWorld(new Vector2(1920, 1080));

            return (pos.X >= screenPosMin.X && pos.Y <= screenPosMin.Y)
                && (pos.X <= screenPosMax.X && pos.Y >= screenPosMax.Y);
        }
    }
}
