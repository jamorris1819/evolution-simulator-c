using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public class DNAHelper
    {
        private static Random _random = new Random();

        public static DNA Cross(DNA a, DNA b)
        {
            var colour = Cross(a.Colour, b.Colour);

            return new DNA(colour);
        }

        public static Genotype<T> Cross<T>(Genotype<T> a, Genotype<T> b) where T: IEquatable<T>
        {
            var rand = Math.Round(_random.NextDouble());

            Gene<T> geneA;
            Gene<T> geneB;

            if(rand == 0)
            {
                geneA = a.GeneA;
                geneB = b.GeneB;
            }
            else
            { 
                geneA = b.GeneA;
                geneB = a.GeneB;
            }

            return new Genotype<T>(geneA, geneB);
        }
    }
}
