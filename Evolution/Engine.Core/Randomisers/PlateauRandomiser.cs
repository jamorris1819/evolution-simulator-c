using System;

namespace Engine.Core.Randomisers
{
    public class PlateauRandomiser : Randomiser
    {
        public double N { get; set; }

        public double P { get; set; }

        public PlateauRandomiser(double n, double p)
        {
            N = n;
            P = p;
        }

        public override int Roll(int count)
        {
            double step = 1.0 / count;
            double random = _random.NextDouble();

            var result = Calculate(N, P, random);

            for(int i = 0; i < count; i++)
            {
                if (result < (i + 1) * step) return i;
            }

            throw new Exception("ooops");
        }

        public static double Calculate(double n, double p, double x)
        {
            return Math.Pow(4 * Math.Pow(x - 0.5, 3) * Math.Pow(1 - Math.Sin(Math.PI * x), n) + 0.5, Math.Log(p, 0.5));
        }
    }
}
