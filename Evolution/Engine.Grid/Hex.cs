using System;
using System.Collections;
using System.Collections.Generic;

namespace Engine.Grid
{
    public struct Hex
    {
        public int Q { get; private set; }
        public int R { get; private set; }
        public int S { get; private set; }

        public Hex(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public static bool operator ==(Hex a, Hex b) => a.Q == b.Q && a.R == b.R && a.S == b.S;

        public static bool operator !=(Hex a, Hex b) => !(a == b);

        public static Hex Add(Hex a, Hex b) => new Hex(a.Q + b.Q, a.R + b.R, a.S + b.S);

        public static Hex Subtract(Hex a, Hex b) => new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);

        public static Hex Multiply(Hex a, Hex b) => new Hex(a.Q * b.Q, a.R * b.R, a.S * b.S);

        public static int Length(Hex a) => (int)((Math.Abs(a.Q) + Math.Abs(a.R) + Math.Abs(a.S)) * 0.5f);

        public int Length() => Length(this);

        public static int Distance(Hex a, Hex b) => Length(Subtract(a, b));

        public static IList<Hex> Directions
            => new Hex[]
            {
                new Hex(1, 0, -1),
                new Hex(1, -1, 0),
                new Hex(0, -1, 1),
                new Hex(-1, 0, 1),
                new Hex(-1, 1, 0),
                new Hex(0, 1, -1)
            };

        public static Hex HexDirection(HexDirection dir) => Directions[(int)dir];

        public static Hex GetNeighbour(Hex hex, HexDirection dir)
            => Add(hex, HexDirection(dir));

        public override bool Equals(object obj)
        {
            return obj is Hex hex && hex == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Q, R, S);
        }

        public override string ToString()
        {
            return $"Hex ({Q}, {R}, {S})";
        }
    }
}
