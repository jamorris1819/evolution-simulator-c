using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data.Zoom
{
    public readonly struct ZoomProfile
    {
        public float MaxZoom { get; }

        public float MinZoom { get; }

        public IZoomCurve Curve { get; }

        public bool Invert { get; }

        public ZoomProfile(float minZoom, float maxZoom, bool invert) : this(minZoom, maxZoom, invert, new StraightZoomCurve()) { }

        public ZoomProfile(float minZoom, float maxZoom, bool invert, IZoomCurve curve)
        {
            if (minZoom >= maxZoom) throw new ArgumentException("Min Zoom must be smaller than Max Zoom");

            MinZoom = minZoom;
            MaxZoom = maxZoom;
            Curve = curve;
            Invert = invert;
        }

        public float GetAlpha(float zoom)
        {
            if (zoom < MinZoom) return !Invert ? 0 : 1;
            if (zoom > MaxZoom) return !Invert ? 1 : 0;

            if(Invert) return Curve.Get(1.0f - ((zoom - MinZoom) / (MaxZoom - MinZoom)));
            else return Curve.Get((zoom - MinZoom) / (MaxZoom - MinZoom));
        }
    }
}
