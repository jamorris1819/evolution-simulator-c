using Engine.Core.Events.Input.Mouse;
using Engine.Render.Core.Shaders;
using Engine.Render.Events;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Redbus.Interfaces;
using System;

namespace Engine.Render
{
    public class MouseCamera : Camera
    {
        public MouseCamera(int width, int height, IEventBus eventBus)
            : base(width, height, eventBus)
        {
            _eventBus.Subscribe<MouseDragEvent>(x =>
            {
                if (x.Button != MouseButton.Right) return;
                TargetPosition += x.Delta * PixelsPerMetreInv / Scale; 
            });

            _eventBus.Subscribe<MouseWheelEvent>(HandleMouseScroll);
        }

        private void HandleMouseScroll(MouseWheelEvent e)
        {
            Console.WriteLine(e.Offset);
            float zoom = (float)Math.Exp(e.Offset.Y * 0.2);

            //if (Scale * zoom < 0.01) return;

            float newWidth = _width / (Scale * zoom);
            float newHeight = _height / (Scale * zoom);
            var mouseLoc = e.Location;

            var dx = -(mouseLoc.X / _width * (newWidth - _width));
            var dy = -(mouseLoc.Y / _height * (newHeight - _height));



            //var x = ((dx / (Scale * zoom)) - dx / Scale) * PixelsPerMetreInv;
            //var y = ((dy / (Scale * zoom)) - dy / Scale) * PixelsPerMetreInv;

            TargetScale *= (float)zoom;
            //TargetPosition += new OpenTK.Mathematics.Vector2(dx, dy) * PixelsPerMetreInv / Scale;

            // todo: figure this out
            _eventBus.Publish(new CameraZoomEvent() { Scale = TargetScale });
        }
    }
}
