using MiscUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public readonly struct GenotypeMetadata<T> where T: struct, IEquatable<T>
    {
        public T? MinValue { get; }
        public T? MaxValue { get; }
        public MutationChance MutationChance { get; }
        public T MutationAmount { get; }

        public GenotypeMetadata(MutationChance chance, T mutationAmount) : this(chance, mutationAmount, null, null) { }

        public GenotypeMetadata(MutationChance chance, T mutationAmount, T? minValue, T? maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            MutationChance = chance;
            MutationAmount = mutationAmount;
        }
    }
}
