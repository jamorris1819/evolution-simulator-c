using Evolution.Genetics.Creature.Enums;
using Evolution.Genetics.Creature.Helper;
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

        /// <summary>
        /// Create a genotype which holds the 2 provided genes
        /// </summary>
        public Genotype(Gene<T> a, Gene<T> b) : this(a, b, new GenotypeMetadata<T>()) { }

        /// <summary>
        /// Creates a genotype which holds the 2 provided genes & the metadata.
        /// </summary>
        public Genotype(Gene<T> a, Gene<T> b, GenotypeMetadata<T> metadata)
        {
            GeneA = a;
            GeneB = b;
            Metadata = metadata;
        }

        /// <summary>
        /// Copies the genotype with possiblity of mutation according to genotype metadata
        /// </summary>
        public Genotype<T> Copy() => DNAHelper.CopyGenotype(this);

        /// <summary>
        /// Creates a phenotype from the genotype
        /// </summary>
        public Phenotype<T> GetPhenotype() => Phenotype<T>.GetFromGenotype(this);
    }
}
