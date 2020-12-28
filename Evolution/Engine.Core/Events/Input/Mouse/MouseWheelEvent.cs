using OpenTK.Mathematics;
using Redbus.Events;

namespace Engine.Core.Events.Input.Mouse
{
    public class MouseWheelEvent : EventBase
    {
        public Vector2 Offset { get; set; }
        public Vector2 Location { get; set; }
    }
}
