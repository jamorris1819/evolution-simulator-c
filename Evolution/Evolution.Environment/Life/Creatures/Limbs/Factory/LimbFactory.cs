using Engine.Core;
using Engine.Core.Managers;
using Evolution.Genetics;
using Evolution.Genetics.Modules.Limbs;

namespace Evolution.Environment.Life.Creatures.Limbs.Factory
{
    public abstract class LimbFactory
    {
        public EntityManager EntityManager { get; set; }

        public abstract LimbType Type { get; }

        public abstract Limb CreateLimb(Entity parent, in DNA dna, bool leftSide);
    }
}
