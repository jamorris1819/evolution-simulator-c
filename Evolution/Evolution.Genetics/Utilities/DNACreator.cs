using Evolution.Genetics.Creature;
using Evolution.Genetics.Creature.Modules;
using Evolution.Genetics.Creature.Modules.Body;
using Evolution.Genetics.Modules.Limbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Utilities
{
    public static class DNACreator
    {
        private static Random _random = new Random();

        public static DNA CreateDNA()
        {
            var bodyModule = new MultiPartBody()
            {
                BodyOffset = Genotype.Create(),
                BodySteps = Genotype.Create(),
                Size = Genotype.Create(),
                Length = Genotype.Create((byte)_random.Next(0, 256), true, 3),
                ColourB = Genotype.Create(),
                ColourG = Genotype.Create(),
                ColourR = Genotype.Create()
            };

            var limbsModule = new WalkingLimbModule()
            {
                LegDirection = Genotype.Create(),
                Length = Genotype.Create(),
                LegThickness = Genotype.Create()
            };

            return new DNA(new IModule[] { bodyModule, limbsModule });
        }
    }
}
