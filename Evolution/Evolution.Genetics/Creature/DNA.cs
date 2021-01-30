using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public readonly struct DNA
    {
        public Genotype<float> ColourR { get; }
        public Genotype<float> ColourG { get; }
        public Genotype<float> ColourB { get; }

        public DNA(Genotype<float> colourR, Genotype<float> colourG, Genotype<float> colourB)
        {
            ColourR = colourR;
            ColourG = colourG;
            ColourB = colourB;
        }
    }
}
