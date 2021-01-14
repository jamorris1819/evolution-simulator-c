using Redbus.Events;

namespace Engine.Render.Events
{
    public class CameraZoomEvent : EventBase
    {
        public float Scale { get; set; }
    }
}
