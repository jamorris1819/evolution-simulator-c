using Evolution.Genetics.Creature.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature.Modules.Body
{
    public class SinglePartBody : Body
    {
        public Genotype<int> BodySteps { get; set; }
        public Genotype<float> BodyOffset { get; set; }

        public SinglePartBody()
        {
            Type = BodyType.SinglePart;
        }

        public override IModule Cross(IModule other)
        {
            if (!(other is SinglePartBody a)) throw new Exception("Cannot be crossed");

            return new SinglePartBody()
            {
                BodySteps = DNAHelper.Cross(BodySteps, a.BodySteps),
                BodyOffset = DNAHelper.Cross(BodyOffset, a.BodyOffset),
                Size = DNAHelper.Cross(Size, a.Size)
            };
        }
    }
}
