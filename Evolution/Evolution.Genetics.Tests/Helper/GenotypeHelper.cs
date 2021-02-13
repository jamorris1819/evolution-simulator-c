using Evolution.Genetics.Creature;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Tests.Helper
{
    internal class GenotypeHelper
    {
        public static Gene CreateGene(byte data, bool dominant) => new Gene(data, dominant);

        public static Genotype CreateGenotype(Gene a, Gene b) => new Genotype(a, b);

        public static Genotype CreateGenotype(byte data)
            => CreateGenotype(CreateGene(data, true), CreateGene(data, true));

        public static Genotype CreateGenotype(byte data1, byte data2)
            => CreateGenotype(CreateGene(data1, true), CreateGene(data2, true));
    }
}
