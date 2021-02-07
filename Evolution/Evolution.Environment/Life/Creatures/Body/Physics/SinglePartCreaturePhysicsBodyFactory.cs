using Engine.Physics.Core;
using Engine.Physics.Core.Shapes;
using Evolution.Genetics.Creature;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace Evolution.Environment.Life.Creatures.Body.Physics
{
    public class SinglePartCreaturePhysicsBodyFactory : CreaturePhysicsBodyFactory
    {
        public SinglePartCreaturePhysicsBodyFactory(World world) : base(world)
        {
        }

        public override IEnumerable<PhysicsBody> CreateBody(in DNA dna, Vector2 position)
        {
            var body = new CirclePhysicsBody(0.05f, 10)
            {
                BodyType = BodyType.Dynamic,
                LinearDrag = 1f,
                AngularDrag = 2f
            };

            body.CreateBody(_world, position);

            return new[]
            {
                body
            };
        }
    }
}
