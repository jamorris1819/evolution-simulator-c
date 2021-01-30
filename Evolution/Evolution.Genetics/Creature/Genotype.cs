using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    /// <summary>
    /// Holds the diploid genetic information
    /// </summary>
    public readonly struct Genotype<T> where T: struct, IEquatable<T>
    {
        public Gene<T> GeneA { get; }
        public Gene<T> GeneB { get; }
        public GenotypeMetadata<T> Metadata { get; }

        public Genotype(Gene<T> a, Gene<T> b) : this(a, b, new GenotypeMetadata<T>()) { }

        public Genotype(Gene<T> a, Gene<T> b, GenotypeMetadata<T> metadata)
        {
            GeneA = a;
            GeneB = b;
            Metadata = metadata;
        }
    }
}
