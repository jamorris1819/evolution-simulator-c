using Engine.Render;
using Engine.Render.Core.Shaders;
using Engine.UI;
using OpenTK.Graphics.ES30;
using OpenTK.Windowing.Desktop;
using Redbus;
using Redbus.Interfaces;
using System;

namespace Engine
{
    public class Game : IDisposable
    {
        private SceneManager _sceneManager;

        public IEventBus EventBus { get; }

        public UIManager UIManager { get; }

        public Game()
        {
            var gameWindowSettings = new GameWindowSettings();
            var nativeWindowSettings = new NativeWindowSettings();


            nativeWindowSettings.Size = new OpenTK.Mathematics.Vector2i(1920, 1080);
            nativeWindowSettings.NumberOfSamples = 4;

            EventBus = new EventBus();

            _sceneManager = new SceneManager(gameWindowSettings, nativeWindowSettings, EventBus);
            UIManager = new UIManager(_sceneManager, EventBus);
            _sceneManager.SetUIManager(UIManager);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        public void Run()
        {
            _sceneManager.Run();
        }

        public void AddScene(BaseScene scene)
        {
            _sceneManager.PushScene(scene);
        }

        public void Dispose()
        {
        }
    }
}
