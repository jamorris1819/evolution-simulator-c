using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public readonly struct DNA
    {
        public Genotype<Vector3> Colour { get; }

        public DNA(Genotype<Vector3> colour)
        {
            Colour = colour;
        }
    }
}
