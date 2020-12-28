using OpenTK.Mathematics;
using Redbus.Events;

namespace Engine.Core.Events.Input.Mouse
{
    public class MouseMoveEvent : EventBase
    {
        public Vector2 Location { get; set; }
        public Vector2 Delta { get; set; }
    }
}
