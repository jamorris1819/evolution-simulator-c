using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Life
{
    public readonly struct LeafData
    {
        public Vector2 A { get; }
        
        public Vector2 B { get; }

        public LeafData(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }
    }
}
