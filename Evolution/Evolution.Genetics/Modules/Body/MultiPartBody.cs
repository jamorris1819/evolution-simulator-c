using Evolution.Genetics.Creature.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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
                Size = Size.Cross(a.Size),
                ColourR = ColourR.Cross(a.ColourR),
                ColourG = ColourG.Cross(a.ColourG),
                ColourB = ColourB.Cross(a.ColourB)
            };
        }

        public override IModule Mutate()
        {
            var severities = new[]
            {
                MutationSeverity.Minor,
                MutationSeverity.Minor,
                MutationSeverity.Minor,
                MutationSeverity.Minor,
                MutationSeverity.Minor,
                MutationSeverity.Minor,
                MutationSeverity.Minor,
                MutationSeverity.Minor,
                MutationSeverity.Medium,
                MutationSeverity.Medium,
                MutationSeverity.Medium,
                MutationSeverity.Medium,
                MutationSeverity.Medium,
                MutationSeverity.Medium,
                MutationSeverity.Major,
                MutationSeverity.Extreme
            };

            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                severities = severities.OrderBy(x => random.NextDouble()).ToArray();
            }

            var stack = new Stack<MutationSeverity>(severities.Length);

            for(int i = 0; i < severities.Length; i++)
            {
                stack.Push(severities[i]);
            }

            return new MultiPartBody()
            {
                BodySteps = BodySteps.Mutate(stack.Pop()),
                BodyOffset = BodyOffset.Mutate(stack.Pop()),
                ColourR = ColourR.Mutate(stack.Pop()),
                ColourG = ColourG.Mutate(stack.Pop()),
                ColourB = ColourB.Mutate(stack.Pop())
            };
        }
    }
}
