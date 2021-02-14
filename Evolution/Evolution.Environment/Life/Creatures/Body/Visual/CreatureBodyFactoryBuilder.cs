using Evolution.Genetics.Creature.Modules.Body;
using System;

namespace Evolution.Environment.Life.Creatures.Body.Visual
{
    public class CreatureBodyFactoryBuilder
    {
        public static CreatureBodyFactory Get(BodyType type)
        {
            return type switch
            {
                BodyType.SinglePart => new SinglePartCreatureBodyFactory(),
                BodyType.MultiPart => new MultiPartCreatureBodyFactory(),
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            };
        }
    }
}
