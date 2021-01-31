namespace Evolution.Genetics.Creature.Enums
{
    /// <summary>
    /// An enum to represent how likely a mutation is to occur.
    /// </summary>
    public enum MutationChance
    {
        None = 0,
        Low = 8,
        Normal = 4,
        High = 2,
        Irradiate = 1,

        Maximum = 30 // used to calculate percentage (ie, low is 1 out of 40)
    }
}
