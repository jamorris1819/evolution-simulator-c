using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data.Zoom
{
    public class BezierZoomCurve : IZoomCurve
    {
        private BezierCurveCubic _curve;

        public BezierZoomCurve(Vector2 handleA, Vector2 handleB)
        {
            _curve = new BezierCurveCubic(Vector2.Zero, Vector2.One, handleA, handleB);
        }

        public float Get(float x) => _curve.CalculatePoint(x).Y;
    }
}
