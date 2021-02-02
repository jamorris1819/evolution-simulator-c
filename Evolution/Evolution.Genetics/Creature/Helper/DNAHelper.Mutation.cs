using Evolution.Genetics.Creature.Enums;
using MiscUtil;
using System;

namespace Evolution.Genetics.Creature.Helper
{
    public partial class DNAHelper
    {
        /// <summary>
        /// Copies DNA with the possiblity of mutation according to genotype metadata
        /// </summary>
        public static DNA CopyDNA(in DNA dna)
            => new DNA(dna.ColourR.Copy(), dna.ColourG.Copy(), dna.ColourB.Copy(), dna.BodySteps.Copy(), dna.BodyOffset.Copy());

        /// <summary>
        /// Copies a genotype with the possibility of mutation according to genotype metadata
        /// </summary>
        public static Genotype<T> CopyGenotype<T>(in Genotype<T> genotype) where T: struct, IEquatable<T>
        {
            if (genotype.Metadata.MutationChance == MutationChance.None) return genotype;

            int index = _random.Next((int)MutationChance.Maximum);

            if (index < (int)genotype.Metadata.MutationChance) return MutateGenotype(genotype, GetRandomSeverity());

            return genotype;
        }

        /// <summary>
        /// Randomly chooses a severity (it is more likely to get a lesser severity)
        /// </summary>
        private static MutationSeverity GetRandomSeverity()
        {
            int index = _random.Next(16);

            if (index < 8) return MutationSeverity.Minor;
            if (index < 12) return MutationSeverity.Medium;
            if (index < 14) return MutationSeverity.Major;

            return MutationSeverity.Extreme;
        }

        /// <summary>
        /// Mutates the genotype according to it's mutation rules
        /// </summary>
        /// <param name="genotype">The genotype  to mutate</param>
        /// <param name="severity">Severity of the mutation</param>
        public static Genotype<T> MutateGenotype<T>(in Genotype<T> genotype, MutationSeverity severity) where T : struct, IEquatable<T>
            => MutateGenotype(genotype, (int)Math.Round(_random.NextDouble()), severity);

        /// <summary>
        /// Mutates the genotype according to it's mutation rules
        /// </summary>
        /// <param name="genotype">The genotype  to mutate</param>
        /// <param name="geneIndex">Which gene to mutate (0 or 1)</param>
        /// <param name="severity">Severity of the mutation</param>
        public static Genotype<T> MutateGenotype<T>(in Genotype<T> genotype, int geneIndex, MutationSeverity severity) where T : struct, IEquatable<T>
        {
            Gene<T> a = genotype.GeneA;
            Gene<T> b = genotype.GeneB;

            if (geneIndex == 0) a = MutateGene(a, genotype.Metadata, severity);
            else b = MutateGene(a, genotype.Metadata, severity);

            return new Genotype<T>(a, b, genotype.Metadata);
        }

        /// <summary>
        /// Mutates a gene according to how the metadata specifies
        /// </summary>
        /// <param name="gene">The gene to mutate</param>
        /// <param name="metadata">The metadata of the genotype</param>
        /// <param name="severity">How severe the mutation is</param>
        public static Gene<T> MutateGene<T>(in Gene<T> gene, in GenotypeMetadata<T> metadata, MutationSeverity severity = MutationSeverity.Minor) where T : struct, IEquatable<T>
        {
            var dominant = gene.Dominant;

            switch (severity)
            {
                case MutationSeverity.Minor:
                case MutationSeverity.Medium:
                    return new Gene<T>(MutateValue(gene.Data, metadata, (int)severity), dominant);
                case MutationSeverity.Major:
                    return new Gene<T>(gene.Data, !dominant);
                case MutationSeverity.Extreme:
                    return new Gene<T>(MutateValue(gene.Data, metadata, (int)severity), !dominant);
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity));
            }
        }

        /// <summary>
        /// Mutates a gene's value according to how the metadata specifies
        /// </summary>
        /// <param name="current">The current value of the gene</param>
        /// <param name="metadata">The metadata of the genotype</param>
        /// <param name="multiplier">A multiplier to the intensity of the mutation</param>
        private static T MutateValue<T>(T current, in GenotypeMetadata<T> metadata, int multiplier = 1) where T: struct, IEquatable<T>
        {
            if(metadata.MutationChance == MutationChance.None) throw new Exception("This gene cannot be mutated.");
            if (Operator.Equal(metadata.MutationAmount, Operator<T>.Zero)) throw new Exception("This gene cannot be mutated.");

            T newData;
            if (_random.NextDouble() < 0.5) newData = Operator.Add(current, metadata.MutationAmount);
            else newData = Operator.Subtract(current, metadata.MutationAmount);

            newData = Operator.MultiplyAlternative(newData, 1);

            if (metadata.MinValue.HasValue && Operator.LessThan(newData, metadata.MinValue.Value)) newData = metadata.MinValue.Value;
            if (metadata.MaxValue.HasValue && Operator.GreaterThan(newData, metadata.MaxValue.Value)) newData = metadata.MaxValue.Value;
            
            return newData;
        }
    }
}
