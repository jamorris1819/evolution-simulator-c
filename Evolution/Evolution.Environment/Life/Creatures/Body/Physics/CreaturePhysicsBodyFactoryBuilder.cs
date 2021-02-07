using System;
using tainicom.Aether.Physics2D.Dynamics;

namespace Evolution.Environment.Life.Creatures.Body.Physics
{
    public class CreaturePhysicsBodyFactoryBuilder
    {
        public static CreaturePhysicsBodyFactory Get(Enums.BodyType type, World world)
        {
            switch (type)
            {
                case Enums.BodyType.SinglePart:
                    return new SinglePartCreaturePhysicsBodyFactory(world);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}
