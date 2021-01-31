using Evolution.Genetics.Creature.Enums;
using MiscUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public readonly struct GenotypeMetadata<T> where T: struct, IEquatable<T>
    {
        /// <summary>
        /// The minimum value that that a gene may hold
        /// </summary>
        public T? MinValue { get; }

        /// <summary>
        /// The maximum value that a gene may hold
        /// </summary>
        public T? MaxValue { get; }

        /// <summary>
        /// How likely a mutation is to occur
        /// </summary>
        public MutationChance MutationChance { get; }

        /// <summary>
        /// The standard amount to mutate value by
        /// </summary>
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
