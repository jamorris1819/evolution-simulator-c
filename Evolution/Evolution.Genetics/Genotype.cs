using Evolution.Genetics.Creature.Enums;
using Evolution.Genetics.Utilities;
using System;

namespace Evolution.Genetics.Creature
{
    /// <summary>
    /// Holds the diploid genetic information
    /// </summary>
    public readonly struct Genotype
    {
        public Gene GeneA { get; }

        public Gene GeneB { get; }

        /// <summary>
        /// Creates a genotype which holds the 2 provided genes.
        /// </summary>
        public Genotype(Gene a, Gene b)
        {
            GeneA = a;
            GeneB = b;
        }

        /// <summary>
        /// Copies the genotype with possiblity of mutation according to genotype metadata
        /// </summary>
        public Genotype Copy() => new Genotype(GeneA, GeneB);

        /// <summary>
        /// Determines the expressed value of the genotype.
        /// </summary>
        public byte GetExpression()
        {
            if (GeneA.Dominant && !GeneB.Dominant) return GeneA.Data;
            if (!GeneA.Dominant && GeneB.Dominant) return GeneB.Data;

            int geneA = GeneA.Data;
            int geneB = GeneB.Data;

            return (byte)((geneA + geneB) / 2);
        }

        /// <summary>
        /// Crosses the genotype's genes with another genotype.
        /// </summary>
        public Genotype Cross(Genotype other)
        {
            Random random = new Random();
            bool keepLeftSide = random.NextDouble() > 0.5;

            if (keepLeftSide) return new Genotype(GeneA, other.GeneB);

            return new Genotype(other.GeneA, GeneB);
        }

        public Genotype Mutate() => Mutate(DNAMutator.GetRandomMutationSeverity());

        public Genotype Mutate(MutationSeverity severity)
        {
            return DNAMutator.Mutate(this, severity);
        }

        public static Genotype Create()
        {
            Random random = new Random();
            bool dominant = random.NextDouble() > 0.5;

            int val = (int)(255 * random.NextDouble());
            var separation = 15;

            return new Genotype(
                new Gene((byte)(val - separation), dominant),
                new Gene((byte)(val + separation), dominant));
        }

        public static Genotype CreateRandom()
        {
            Random random = new Random();

            return new Genotype(
                new Gene((byte)(random.NextDouble() * 255), random.NextDouble() > 0.5),
                new Gene((byte)(random.NextDouble() * 255), random.NextDouble() > 0.5));
        }
    }
}
