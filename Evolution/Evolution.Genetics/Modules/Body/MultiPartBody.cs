using System;

namespace Evolution.Genetics.Creature.Modules.Body
{
    public class MultiPartBody : BodyModule
    {
        public Genotype Length { get; set; }

        public MultiPartBody()
        {
            Type = BodyType.MultiPart;
        }

        public override IModule Cross(IModule other)
        {
            if (!(other is MultiPartBody a)) throw new Exception("Cannot be crossed");

            return new MultiPartBody()
            {
                BodySteps = BodySteps.Cross(a.BodySteps),
                BodyOffset = BodyOffset.Cross(a.BodyOffset),
                Length = Length.Cross(a.Length),
                Size = Size.Cross(a.Size),
                ColourR = ColourR.Cross(a.ColourR),
                ColourG = ColourG.Cross(a.ColourG),
                ColourB = ColourB.Cross(a.ColourB)
            };
        }

        public override IModule Mutate()
        {
            return new MultiPartBody()
            {
                BodySteps = BodySteps.Mutate(),
                BodyOffset = BodyOffset.Mutate(),
                Length = Length.Mutate(),
                ColourR = ColourR.Mutate(),
                ColourG = ColourG.Mutate(),
                ColourB = ColourB.Mutate()
            };
        }
    }
}
