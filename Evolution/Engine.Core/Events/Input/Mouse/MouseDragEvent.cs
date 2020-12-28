using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Engine.Core.Events.Input.Mouse
{
    public class MouseDragEvent : MouseMoveEvent
    {
        public MouseButton Button { get; set; }
    }
}
