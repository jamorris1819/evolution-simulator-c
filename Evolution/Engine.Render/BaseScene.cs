using OpenTK.Windowing.Common;
using System;

namespace Engine.Render
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
