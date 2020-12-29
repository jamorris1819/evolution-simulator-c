using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Grid
{
    struct Orientation
    {
        public double f0 { get; private set; }
        public double f1 { get; private set; }
        public double f2 { get; private set; }
        public double f3 { get; private set; }

        public double b0 { get; private set; }
        public double b1 { get; private set; }
        public double b2 { get; private set; }
        public double b3 { get; private set; }

        public double startAngle { get; private set; }

        public Orientation(double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3, double startAngle)
        {
            this.f0 = f0;
            this.f1 = f1;
            this.f2 = f2;
            this.f3 = f3;
            this.b0 = b0;
            this.b1 = b1;
            this.b2 = b2;
            this.b3 = b3;
            this.startAngle = startAngle;
        }

        public static Orientation Layout_Pointy
            => new Orientation(
                Math.Sqrt(3.0),
                Math.Sqrt(3.0) / 2.0,
                0.0,
                3.0 / 2.0,
                Math.Sqrt(3.0) / 3.0,
                -1.0 / 3.0,
                0.0,
                2.0 / 3.0,
                0.5);
    }
}
