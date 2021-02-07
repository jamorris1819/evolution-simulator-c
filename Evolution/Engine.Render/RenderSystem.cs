using Engine.Core;
using Engine.Core.Components;
using Engine.Render.Core.Shaders;
using Engine.Render.Core.Shaders.Enums;
using Engine.Render.Core.VAO;
using Engine.Render.Events;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;

namespace Engine.Render
{
    public class RenderSystem : SystemBase, ISystem
    {
        private IEventBus _eventBus;
        private ShaderManager _shaderManager;
        private Camera _camera;

        private float _scale;

        public RenderSystem(IEventBus eventBus, ShaderManager shaderManager)
        {
            _eventBus = eventBus;
            _shaderManager = shaderManager;
            _scale = 1;

            Mask = ComponentType.COMPONENT_RENDER | ComponentType.COMPONENT_POSITION;

            eventBus.Subscribe<CameraZoomEvent>(x => { _scale = x.Scale; Console.WriteLine(x.Scale); });
            eventBus.Subscribe<CameraChangeEvent>(x => _camera = x.Camera);
        }

        public override void OnRender(Entity entity)
        {
            if (!MaskMatch(entity)) return;

            RenderComponent comp = entity.GetComponent<RenderComponent>();
            PositionComponent posComp = entity.GetComponent<PositionComponent>();

            //if (!InView(posComp.Position)) return;

            if (comp.Shaders.Count == 0) throw new Exception("Cannot render object without any shaders");

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            var pos = GetWorldPosition(entity);
            var def = Matrix4.CreateRotationZ(GetWorldAngle(entity)) * Matrix4.CreateTranslation(new Vector3(pos.X, pos.Y, 0));

            if (comp.VertexArrayObject.Alpha == 0) return;

            comp.VertexArrayObject.Bind();

            for (int i = 0; i < comp.Shaders.Count; i++)
            {
                var shader = _shaderManager.GetShader(comp.Shaders[i]);
                shader.Bind();
                shader.SetUniformMat4(ShaderUniforms.Model, def);
                shader.SetUniform(ShaderUniforms.Alpha, comp.VertexArrayObject.Alpha);

                comp.VertexArrayObject.Render(shader);
            }
        }

        private Vector2 GetWorldPosition(Entity entity)
        {
            var position = entity.GetComponent<PositionComponent>().Position;

            if (entity.Parent == null) return position;

            var parentPosComp = entity.Parent.GetComponent<PositionComponent>();
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
            var angle = entity.GetComponent<PositionComponent>().Angle;

            if (entity.Parent == null) return angle;

            return angle + GetWorldAngle(entity.Parent);
        }

        public override void OnUpdate(Entity entity, float deltaTime)
        {
            if (!MaskMatch(entity)) return;

            RenderComponent comp = entity.GetComponent<RenderComponent>();

            if(comp.ZoomProfile.HasValue)
            {
                var alpha = comp.ZoomProfile.Value.GetAlpha(_camera.Scale);
                comp.VertexArrayObject.Alpha = alpha;
            }

            if (!comp.VertexArrayObject.Initialised)
            {
                comp.VertexArrayObject.Initialise(_shaderManager);
                comp.VertexArrayObject.Load();
            }

            if (comp.VertexArrayObject.NeedsUpdate)
            {
                comp.VertexArrayObject.Reload();
            }
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
