using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Redbus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core.Events.Input.Mouse
{
    public abstract class MouseEvent : EventBase
    {
        public MouseButton Button { get; set; }

        public Vector2 Location { get; set; }
    }
}
