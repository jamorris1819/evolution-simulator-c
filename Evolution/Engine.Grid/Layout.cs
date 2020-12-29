using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Engine.Grid
{
    public struct Layout
    {
        private Orientation orientation;
        private Vector2 size;
        private Vector2 origin;

        public Layout(Orientation orientation, Vector2 size, Vector2 origin)
        {
            this.orientation = orientation;
            this.size = size;
            this.origin = origin;
        }

        public Layout(Orientation orientation, Vector2 size) : this(orientation, size, new Vector2(0, 0)) { }

        public Vector2 HexToPixel(Hex hex)
        {
            var o = orientation;
            double x = (o.f0 * hex.Q + o.f1 * hex.R) * size.X;
            double y = (o.f2 * hex.Q + o.f3 * hex.R) * size.Y;
            return new Vector2((float)x + origin.X, (float)y + origin.Y);
        }

        public FractionalHex PixelToHex(Vector2 pos)
        {
            var o = orientation;
            Vector2 pt = new Vector2((pos.X - origin.X) / size.X, (pos.Y - origin.Y) / size.Y);
            double q = (o.b0 * pt.X + o.b1 * pt.Y) * 2.0;
            double r = (o.b2 * pt.X + o.b3 * pt.Y) * 2.0;

            return new FractionalHex(q, r, -q - r);
        }

        public IList<Vector2> GetHexPoints()
        {
            List<Vector2> corners = new List<Vector2>();
            Hex h = new Hex(0, 0, 0);
            Vector2 centre = HexToPixel(h);
            for(int i = 0; i < 6; i++)
            {
                Vector2 offset = CornerOffset(i);
                corners.Add(new Vector2(centre.X + offset.X, centre.Y + offset.Y));
            }

            return corners;
        }

        private Vector2 CornerOffset(int corner)
        {
            double angle = 2.0 * Math.PI * (orientation.startAngle + corner) / 6.0;
            return new Vector2(size.X * (float)Math.Cos(angle), size.Y * (float)Math.Sin(angle));
        }
    }
}
