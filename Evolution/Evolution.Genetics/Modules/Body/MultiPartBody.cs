using System;

namespace Evolution.Genetics.Creature.Modules.Body
{
    public class MultiPartBody : Body
    {
        public Genotype BodySteps { get; set; }
        public Genotype BodyOffset { get; set; }

        public MultiPartBody()
        {
            Type = BodyType.SinglePart;
        }

        public override IModule Cross(IModule other)
        {
            if (!(other is MultiPartBody a)) throw new Exception("Cannot be crossed");

            return new MultiPartBody()
            {
                BodySteps = BodySteps.Cross(a.BodySteps),
                BodyOffset = BodyOffset.Cross(a.BodyOffset),
                Size = Size.Cross(a.Size)
            };
        }
    }
}
