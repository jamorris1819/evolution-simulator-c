using Redbus.Events;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Engine.Core.Events.Input.Keyboard
{
    public abstract class KeyEvent : EventBase
    {
        public Keys Key { get; set; }
    }
}
