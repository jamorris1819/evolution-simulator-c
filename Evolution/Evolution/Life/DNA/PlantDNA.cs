using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Life.DNA
{
    public readonly struct PlantDNA
    {
        public int Leaves { get; }

        public LeafData LeafShape { get; }

        public PlantDNA(int leaves, LeafData leafShape)
        {
            Leaves = leaves;
            LeafShape = leafShape;
        }
    }
}
