using Engine.Core;
using Engine.Core.Components;
using Engine.Render.Data;
using Engine.Render.Events;
using Engine.Render.Managers;
using Engine.Render.Shaders;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render
{
    public class RenderSystem : SystemBase, ISystem
    {
        private IEventBus _eventBus;
        private ShaderManager _shaderManager;
        private VAOLoader _vaoLoader;
        private Camera _camera;

        private float _scale;

        public RenderSystem(IEventBus eventBus, ShaderManager shaderManager)
        {
            _eventBus = eventBus;
            _shaderManager = shaderManager;
            _vaoLoader = new VAOLoader();
            _scale = 1;

            eventBus.Subscribe<CameraZoomEvent>(x => _scale = x.Scale);
            eventBus.Subscribe<CameraChangeEvent>(x => _camera = x.Camera);
        }

        public override void OnRender(Entity entity)
        {
            if (!MaskMatch(entity)) return;

            RenderComponent comp = entity.GetComponent<RenderComponent>();
            PositionComponent posComp = entity.GetComponent<PositionComponent>();

            //if (!InView(posComp.Position)) return;

            //if (comp.MinZoom > _scale) return;

            var def = Matrix4.CreateTranslation(new Vector3(posComp.Position.X, posComp.Position.Y, 0));

            var shader = _shaderManager.GetShader(comp.Shader);
            shader.Bind();
            shader.SetUniformMat4(Shaders.Enums.ShaderUniforms.Model, def);

            comp.VertexArrayObject.Render();

           /* GL.BindVertexArray(comp.VertexArrayObject.VAO[0]);

            if (comp.VertexArrayObject is InstancedVertexArrayObject iVAO)
            {
                GL.DrawElementsInstanced(PrimitiveType.Triangles, iVAO.VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero, iVAO.Positions.Length);
            }
            else
            {
                GL.DrawElements(PrimitiveType.Triangles, comp.VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero);
            }

            GL.BindVertexArray(0);*/
        }

        public override void OnUpdate(Entity entity)
        {
            if (!MaskMatch(entity)) return;

            RenderComponent comp = entity.GetComponent<RenderComponent>();
            
            if(!comp.VertexArrayObject.Initialised)
            {
                comp.VertexArrayObject.Initialise(_shaderManager.All);
                comp.VertexArrayObject.Load();
            }
            /*if(comp.VertexArrayObject.Reload)
            {
                _vaoLoader.Reload(comp.VertexArrayObject);
            }*/
        }

        private void InitialiseVAO(VertexArrayObject vao)
        {
            _vaoLoader.Load(vao);
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
