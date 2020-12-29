using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Grid
{
    public struct FractionalHex
    {
        public double Q { get; private set; }
        public double R { get; private set; }
        public double S { get; private set; }

        public FractionalHex(double q, double r, double s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public Hex Round()
        {
            int q = (int)Math.Round(this.Q);
            int r = (int)Math.Round(this.R);
            int s = (int)Math.Round(this.S);

            double qDiff = Math.Abs(q - this.Q);
            double rDiff = Math.Abs(r - this.R);
            double sDiff = Math.Abs(s - this.S);

            if (qDiff > rDiff && qDiff > sDiff) q = -r - s;
            else if (rDiff > sDiff) r = -q - s;
            else s = -q - r;

            return new Hex(q, r, s);
        }
    }
}
