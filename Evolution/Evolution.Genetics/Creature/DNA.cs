using Evolution.Genetics.Creature.Helper;
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

        public Genotype<int> BodySteps { get; }
        public Genotype<float> BodyOffset { get; }

        public DNA(Genotype<float> colourR, Genotype<float> colourG, Genotype<float> colourB, Genotype<int> bodySteps, Genotype<float> bodyOffset)
        {
            ColourR = colourR;
            ColourG = colourG;
            ColourB = colourB;

            BodySteps = bodySteps;
            BodyOffset = bodyOffset;
        }

        public DNA Mutate() => DNAHelper.MutateDNA(this);
    }
}
