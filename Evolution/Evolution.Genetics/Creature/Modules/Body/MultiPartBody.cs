using Evolution.Genetics.Creature.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Genetics.Creature.Modules.Body
{
    public class MultiPartBody : Body
    {
        public Genotype<int> BodySteps { get; set; }
        public Genotype<float> BodyOffset { get; set; }

        public MultiPartBody()
        {
            Type = BodyType.SinglePart;
        }

        public override IModule Cross(IModule other)
        {
            if (!(other is MultiPartBody a)) throw new Exception("Cannot be crossed");

            return new MultiPartBody()
            {
                BodySteps = DNAHelper.Cross(BodySteps, a.BodySteps),
                BodyOffset = DNAHelper.Cross(BodyOffset, a.BodyOffset),
                Size = DNAHelper.Cross(Size, a.Size)
            };
        }
    }
}
