using MiscUtil;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature.Helper
{
    public partial class DNAHelper
    {
        public static Genotype<T> MutateGenotype<T>(in Genotype<T> genotype) where T : struct, IEquatable<T>
            => MutateGenotype(genotype, (int)Math.Round(_random.NextDouble()));

        public static Genotype<T> MutateGenotype<T>(in Genotype<T> genotype, int geneIndex) where T : struct, IEquatable<T>
        {
            Gene<T> a = genotype.GeneA;
            Gene<T> b = genotype.GeneB;

            if (geneIndex == 0) a = MutateGene(a, genotype.Metadata);
            else b = MutateGene(a, genotype.Metadata);

            return new Genotype<T>(a, b, genotype.Metadata);
        }

        public static Gene<T> MutateGene<T>(in Gene<T> gene, in GenotypeMetadata<T> metadata) where T : struct, IEquatable<T>
            => new Gene<T>(GetMutateValue(gene.Data, metadata), gene.Dominant);

        private static T GetMutateValue<T>(T current, in GenotypeMetadata<T> metadata) where T: struct, IEquatable<T>
        {
            if(metadata.MutationChance == MutationChance.None) throw new Exception("This gene cannot be mutated.");
            if (Operator.Equal(metadata.MutationAmount, Operator<T>.Zero)) throw new Exception("This gene cannot be mutated.");

            T newData;
            if (_random.NextDouble() < 0.5) newData = Operator.Add(current, metadata.MutationAmount);
            else newData = Operator.Subtract(current, metadata.MutationAmount);

            if (metadata.MinValue.HasValue && Operator.LessThan(newData, metadata.MinValue.Value)) newData = metadata.MinValue.Value;
            if (metadata.MaxValue.HasValue && Operator.GreaterThan(newData, metadata.MaxValue.Value)) newData = metadata.MaxValue.Value;
            
            return newData;
        }
    }
}
