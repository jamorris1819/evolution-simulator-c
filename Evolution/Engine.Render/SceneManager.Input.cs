using ImGuiNET;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render
{
    public partial class SceneManager : GameWindow
    {
        private Vector2 _mouseScroll;

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (ImGui.GetIO().WantCaptureMouse) return;
            base.OnMouseDown(e);
            _inputManager.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            _inputManager.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            _inputManager.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (ImGui.GetIO().WantCaptureMouse)
            {
                _uiManager.MouseScroll(e.Offset - _mouseScroll);
                _mouseScroll = e.Offset;
                return;
            }

            base.OnMouseWheel(e);
            if (e.Offset.LengthSquared == 0) return;
            _inputManager.OnMouseWheel(e);
        }


        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);
            _uiManager.PressChar((char)e.Unicode);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            _inputManager.OnKeyboardDown(e);
        }
    }
}
