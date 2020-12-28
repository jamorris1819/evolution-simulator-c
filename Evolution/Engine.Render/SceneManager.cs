﻿
using Engine.Core.Managers;
using OpenTK.Graphics.ES30;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render
{
    public partial class SceneManager : GameWindow
    {
        public delegate void SceneDelegate(EventArgs e);
        public delegate void SceneDelegateFrame(FrameEventArgs e);

        public SceneDelegateFrame Renderer;
        public SceneDelegateFrame Updater;

        private Stack<BaseScene> _scenes;

        private InputManager _inputManager;
        private IEventBus _eventBus;

        public BaseScene CurrentScene { get => _scenes.Peek(); }

        public SceneManager(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, IEventBus eventBus)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _scenes = new Stack<BaseScene>();
            _eventBus = eventBus;
            _inputManager = new InputManager(eventBus);
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
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Renderer(args);

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
