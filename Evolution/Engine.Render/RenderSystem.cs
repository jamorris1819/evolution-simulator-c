using Engine.Core;
using Engine.Core.Components;
using Engine.Render.Core.Shaders;
using Engine.Render.Core.Shaders.Enums;
using Engine.Render.Core.VAO;
using Engine.Render.Events;
using OpenTK.Mathematics;
using Redbus.Interfaces;

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

            for (int i = 0; i < comp.Shaders.Count; i++)
            {
                var shader = _shaderManager.GetShader(comp.Shaders[i]);
                shader.Bind();
                shader.SetUniformMat4(ShaderUniforms.Model, def);

                comp.VertexArrayObject.Render();
            }
        }

        public override void OnUpdate(Entity entity)
        {
            if (!MaskMatch(entity)) return;

            RenderComponent comp = entity.GetComponent<RenderComponent>();
            
            if(!comp.VertexArrayObject.Initialised)
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
