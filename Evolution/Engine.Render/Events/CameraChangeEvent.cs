using Redbus.Events;

namespace Engine.Render.Events
{
    public class CameraChangeEvent : EventBase
    {
        public Camera Camera { get; set; }
    }
}
