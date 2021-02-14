using System;
using tainicom.Aether.Physics2D.Dynamics;

namespace Evolution.Environment.Life.Creatures.Body.Physics
{
    public class CreaturePhysicsBodyFactoryBuilder
    {
        public static CreaturePhysicsBodyFactory Get(Genetics.Creature.Modules.Body.BodyType type, World world)
        {
            switch (type)
            {
                case Genetics.Creature.Modules.Body.BodyType.SinglePart:
                    return new SinglePartCreaturePhysicsBodyFactory(world);
                case Genetics.Creature.Modules.Body.BodyType.MultiPart:
                    return new MultiPartCreaturePhysicsBodyFactory(world);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}
