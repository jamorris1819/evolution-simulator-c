using Engine.Core.Managers;
using Engine.Render;
using Engine.Render.Core.Shaders.Enums;
using OpenTK.Graphics.ES30;
using OpenTK.Windowing.Common;
using Redbus.Interfaces;
using System;

namespace Engine
{
    public abstract class GameScene : BaseScene
    {
        public IEventBus EventBus { get; private set; }

        protected EntityManager EntityManager { get; private set; }

        protected SystemManager SystemManager { get; private set; }

        protected Game Game { get; private set; }

        public GameScene(Game game)
        {
            Game = game;
            EventBus = game.EventBus;
            EntityManager = new EntityManager(EventBus);
            SystemManager = new SystemManager(EntityManager, EventBus);

            Game.ShaderManager.CreateShader(Render.Core.Shaders.Enums.ShaderType.Standard, "Shaders/vshader.glsl", "Shaders/fshader.glsl");
            Game.ShaderManager.CreateShader(Render.Core.Shaders.Enums.ShaderType.StandardInstanced, "Shaders/vshaderinstanced.glsl", "Shaders/fshader.glsl");
            Game.ShaderManager.CreateShader(Render.Core.Shaders.Enums.ShaderType.InstancedRotated, "Shaders/vshaderinstancedrotated.glsl", "Shaders/fshader.glsl");

            var renderSystem = new RenderSystem(EventBus, Game.ShaderManager);
            SystemManager.AddSystem(renderSystem);
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
            SystemManager.Render();
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            SystemManager.Update();
        }
    }
}
