using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Core.Events;
using Engine.Core.Events.Input.Mouse;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Redbus.Interfaces;

namespace Engine.Core.Managers
{
    public class InputManager
    {
        private IEventBus _eventBus;
        private List<MouseButton> _mouseButtonsDown;
        private Vector2 _mousePosition;

        public InputManager(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _mouseButtonsDown = new List<MouseButton>(4);
        }

        public void OnMouseDown(MouseButtonEventArgs e)
        {
            _eventBus.Publish(new MouseDownEvent()
            {
                Button = e.Button,
                Location = _mousePosition
            });

            if(!_mouseButtonsDown.Contains(e.Button))
            {
                _mouseButtonsDown.Add(e.Button);
            }
        }

        public void OnMouseUp(MouseButtonEventArgs e)
        {
            _eventBus.Publish(new MouseUpEvent()
            {
                Button = e.Button,
                Location = _mousePosition
            });

            if (_mouseButtonsDown.Contains(e.Button))
            {
                _mouseButtonsDown.Remove(e.Button);
            }
        }

        public void OnMouseMove(MouseMoveEventArgs e)
        {
            _eventBus.Publish(new MouseMoveEvent()
            {
                Location = e.Position,
                Delta = e.Delta
            });

            _mousePosition = e.Position;

            var dragEvents = _mouseButtonsDown.Select(x => new MouseDragEvent()
            {
                Button = x,
                Location = e.Position,
                Delta = e.Delta
            }).ToList();

            for(int i = 0; i < dragEvents.Count; i++)
            {
                _eventBus.Publish(dragEvents[i]);
            }
        }
    }
}
