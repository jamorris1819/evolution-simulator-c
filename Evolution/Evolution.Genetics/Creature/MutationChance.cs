using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature
{
    /// <summary>
    /// An enum to represent how likely a mutation is to occur.
    /// </summary>
    public enum MutationChance
    {
        None = 0,
        Low = 1,
        Normal = 2,
        High = 4,
        Irradiate = 8 
    }
}
