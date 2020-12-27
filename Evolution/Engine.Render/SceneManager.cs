
using Engine.Render.Scenes;
using OpenTK.Graphics.ES30;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render
{
    public class SceneManager : GameWindow
    {
        public delegate void SceneDelegate(EventArgs e);
        public delegate void SceneDelegateFrame(FrameEventArgs e);

        public SceneDelegateFrame Renderer;
        public SceneDelegateFrame Updater;

        private Stack<BaseScene> _scenes;

        public BaseScene CurrentScene { get => _scenes.Peek(); }

        public SceneManager(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _scenes = new Stack<BaseScene>();
        }

        public void PushScene(BaseScene scene)
        {
            _scenes.Push(scene);

            Renderer = scene.OnRenderFrame;
            Updater = scene.OnUpdateFrame;

            scene.OnLoad(null);
        }

        public void PopScene()
        {
            _scenes.Pop();

            if (_scenes.Count == 0) throw new Exception("no scenes are being rendered");

            Renderer = CurrentScene.OnRenderFrame;
            Updater = CurrentScene.OnUpdateFrame;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            Updater(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            Renderer(args);
        }
    }
}
