using Engine.Core.Managers;
using Evolution.Genetics.Creature;

namespace Evolution.Environment.Life.Creatures.Mouth.Factory
{
    public abstract class MouthFactory
    {
        public abstract Mouth CreateMouth(in DNA dna, float scale);
    }
}
