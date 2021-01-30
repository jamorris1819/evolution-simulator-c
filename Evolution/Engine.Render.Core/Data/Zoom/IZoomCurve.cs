using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data.Zoom
{
    public interface IZoomCurve
    {
        float Get(float x);
    }
}
