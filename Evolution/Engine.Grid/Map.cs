using System;
using System.Collections.Generic;

namespace Engine.Grid
{
    public class Map
    {
        public static HashSet<Hex> GenerateHexagon(int radius)
        {
            HashSet<Hex> ht = new HashSet<Hex>();

            for(int q = -radius; q <= radius; q++)
            {
                int r1 = Math.Max(-radius, -q - radius);
                int r2 = Math.Min(radius, -q + radius);
                for(int r = r1; r <= r2; r++)
                {
                    ht.Add(new Hex(q, r, -q - r));
                }
            }

            return ht;
        }
    }
}
