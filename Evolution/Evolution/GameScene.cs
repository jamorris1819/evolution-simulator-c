using Engine.Core;
using Engine.Core.Managers;
using Engine.Render;
using Engine.Render.Data.Primitives;
using Engine.Render.Scenes;
using Engine.Render.Shaders;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Redbus;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution
{
    public class GameScene : BaseScene
    {
        private EntityManager _entityManager;
        private SystemManager _systemManager;
        private IEventBus _eventBus;

        public GameScene()
        {
            _eventBus = new EventBus();
            _entityManager = new EntityManager(_eventBus);
            _systemManager = new SystemManager(_entityManager, _eventBus);

            ShaderManager s = new ShaderManager();
            s.CreateShader(Engine.Render.Shaders.Enums.ShaderType.Standard, "vshader.glsl", "fshader.glsl");

            var renderSystem = new RenderSystem(_eventBus, s);
            _systemManager.AddSystem(renderSystem);


            Entity e = new Entity("test");
            var rect = new Rectangle(1, 1);
            rect.SetColour(new Vector3(1, 1, 1));
            var rc = new RenderComponent(rect);
            e.AddComponent(rc);
            _entityManager.AddEntity(e);
        }

        public override void OnFocusChanged(EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0, 0, 0, 1.0f);
        }

        public override void OnRenderFrame(FrameEventArgs e)
        {
            _systemManager.Render();
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            _systemManager.Update();
        }
    }
}
