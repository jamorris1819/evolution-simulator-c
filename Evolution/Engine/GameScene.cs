using Engine.Core;
using Engine.Core.Events;
using Engine.Core.Managers;
using Engine.Render;
using Engine.Render.Data.Primitives;
using Engine.Render.Shaders;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Redbus;
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

            Game.ShaderManager.CreateShader(Render.Shaders.Enums.ShaderType.Standard, "vshader.glsl", "fshader.glsl");

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
