using System;

namespace Evolution.Genetics.Creature.Modules.Body
{
    public class SinglePartBody : BodyModule
    {
        public Genotype BodySteps { get; set; }
        public Genotype BodyOffset { get; set; }

        public SinglePartBody()
        {
            Type = BodyType.SinglePart;
        }

        public override IModule Cross(IModule other)
        {
            if (!(other is SinglePartBody a)) throw new Exception("Cannot be crossed");

            return new SinglePartBody()
            {
                BodySteps = BodySteps.Cross(a.BodySteps),
                BodyOffset = BodyOffset.Cross(a.BodyOffset),
                Size = Size.Cross(a.Size)
            };
        }

        public override IModule Mutate()
                => new SinglePartBody()
                {
                    BodySteps = BodySteps.Mutate(),
                    BodyOffset = BodyOffset.Mutate(),
                    ColourR = ColourR.Mutate(),
                    ColourG = ColourG.Mutate(),
                    ColourB = ColourB.Mutate()
                };
    }
}
