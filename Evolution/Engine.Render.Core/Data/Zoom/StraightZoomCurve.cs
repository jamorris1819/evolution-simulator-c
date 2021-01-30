using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data.Zoom
{
    public class StraightZoomCurve : IZoomCurve
    {
        public float Get(float x) => x;
    }
}
