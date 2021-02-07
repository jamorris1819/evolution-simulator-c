using Engine.Physics.Core;
using Evolution.Genetics.Creature;
using OpenTK.Mathematics;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Dynamics;

namespace Evolution.Environment.Life.Creatures.Body.Physics
{
    public abstract class CreaturePhysicsBodyFactory
    {
        protected readonly World _world;

        public CreaturePhysicsBodyFactory(World world)
        {
            _world = world;
        }

        public abstract IEnumerable<PhysicsBody> CreateBody(in DNA dna, Vector2 position);
    }
}
