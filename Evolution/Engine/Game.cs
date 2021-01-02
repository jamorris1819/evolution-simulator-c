using Engine.Render;
using Engine.Render.Shaders;
using Engine.UI;
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

        public ShaderManager ShaderManager { get; }

        public UIManager UIManager { get; }

        public Game()
        {
            var gameWindowSettings = new GameWindowSettings();
            var nativeWindowSettings = new NativeWindowSettings();

            nativeWindowSettings.Size = new OpenTK.Mathematics.Vector2i(1920, 1080);

            EventBus = new EventBus();
            ShaderManager = new ShaderManager();

            _sceneManager = new SceneManager(gameWindowSettings, nativeWindowSettings, EventBus);
            UIManager = new UIManager(_sceneManager, EventBus);
            _sceneManager.SetUIManager(UIManager);
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
