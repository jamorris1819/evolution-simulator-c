using System;

namespace Engine.Core.Randomisers
{
    /// <summary>
    /// A class responsible for generating random numbers.
    /// </summary>
    public abstract class Randomiser
    {
        protected Random _random = new Random();
        /// <summary>
        /// Creates a random number less than count and greater or equal to 0.
        /// </summary>
        public abstract int Roll(int count);
    }
}
