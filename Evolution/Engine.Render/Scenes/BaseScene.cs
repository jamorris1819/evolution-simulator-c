using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Scenes
{
    public abstract class BaseScene
    {
        public bool Loaded { get; set; }

        public abstract void OnLoad(EventArgs e);
        public abstract void OnUpdateFrame(FrameEventArgs e);
        public abstract void OnRenderFrame(FrameEventArgs e);
        public abstract void OnFocusChanged(EventArgs e);
    }
}
