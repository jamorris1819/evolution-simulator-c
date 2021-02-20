using Evolution.Genetics.Creature;
using Evolution.Genetics.Creature.Modules;
using System;

namespace Evolution.Genetics.Modules.Limbs
{
    public class WalkingLimbModule : IModule
    {
        public ModuleType ModuleType => ModuleType.WalkingLimbs;
        public LimbType LimbType => LimbType.WalkingLeg;

        public Genotype Length { get; set; }
        public Genotype LegDirection { get; set; }
        public Genotype LegThickness { get; set; }


        public IModule Cross(IModule other)
        {
            throw new NotImplementedException();
        }

        public IModule Mutate()
        {
            return this;
        }
    }
}
