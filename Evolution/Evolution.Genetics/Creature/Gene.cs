using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    public readonly struct Gene<T> where T: IEquatable<T>
    {
        /// <summary>
        /// The data held inside the gene
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// Whether the gene is a dominant gene
        /// </summary>
        public bool Dominant { get; }

        /// <summary>
        /// Create a gene to hold the specified data
        /// </summary>
        /// <param name="data">The data to hold</param>
        /// <param name="dominant">Whether this gene is dominant</param>
        public Gene(T data, bool dominant)
        {
            Data = data;
            Dominant = dominant;
        }
    }
}
