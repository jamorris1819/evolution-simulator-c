using Evolution.Genetics.Creature;
using Evolution.Genetics.Creature.Modules;
using Evolution.Genetics.Creature.Modules.Body;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Utilities
{
    public static class DNACreator
    {
        public static DNA CreateDNA()
        {
            var bodyModule = new SinglePartBody()
            {
                BodyOffset = Genotype.Create(),
                BodySteps = Genotype.Create(),
                Size = Genotype.Create(),
                ColourB = Genotype.Create(),
                ColourG = Genotype.Create(),
                ColourR = Genotype.Create()
            };

            return new DNA(new IModule[] { bodyModule });
        }
    }
}
