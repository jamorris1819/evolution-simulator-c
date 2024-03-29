﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Core.Events;
using Engine.Core.Events.Input.Keyboard;
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
        private Vector2 _mouseScroll;
        private static Dictionary<Keys, bool> _keyMap = new Dictionary<Keys, bool>();

        public InputManager(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _mouseButtonsDown = new List<MouseButton>(4);
            _mouseScroll = new Vector2(0, 0);
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

        public void OnMouseWheel(MouseWheelEventArgs e)
        {
            _eventBus.Publish(new MouseWheelEvent()
            {
                Offset = e.Offset - _mouseScroll,
                Location = _mousePosition
            });

            _mouseScroll = e.Offset;
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
                Delta = e.Delta * new Vector2(1, -1)
            });

            _mousePosition = e.Position;

            var dragEvents = _mouseButtonsDown.Select(x => new MouseDragEvent()
            {
                Button = x,
                Location = e.Position,
                Delta = e.Delta * new Vector2(1, -1)
            }).ToList();

            for(int i = 0; i < dragEvents.Count; i++)
            {
                _eventBus.Publish(dragEvents[i]);
            }
        }

        public void OnKeyboardDown(KeyboardKeyEventArgs e)
        {
            _eventBus.Publish(new KeyDownEvent()
            {
                Key = e.Key
            });

            if (!_keyMap.ContainsKey(e.Key)) _keyMap.Add(e.Key, true);
            else _keyMap[e.Key] = true;
        }

        public void OnKeyboardUp(KeyboardKeyEventArgs e)
        {
           /* _eventBus.Publish(new KeyUpEvent()
            {
                Key = e.Key
            });*/

            if (!_keyMap.ContainsKey(e.Key)) _keyMap.Add(e.Key, false);
            else _keyMap[e.Key] = false;
        }

        public static bool IsKeyDown(Keys key) => _keyMap.ContainsKey(key) ? _keyMap[key] : false;
    }
}
