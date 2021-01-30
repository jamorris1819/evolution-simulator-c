using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    /// <summary>
    /// Holds the diploid genetic information
    /// </summary>
    public readonly struct Genotype<T> where T: IEquatable<T>
    {
        public Gene<T> GeneA { get; }
        public Gene<T> GeneB { get; }

        public Genotype(Gene<T> a, Gene<T> b)
        {
            GeneA = a;
            GeneB = b;
        }
    }
}
