using Engine.Physics.Core;
using Engine.Physics.Core.Shapes;
using Evolution.Genetics.Creature;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Linq;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Joints;

namespace Evolution.Environment.Life.Creatures.Body.Physics
{
    public class MultiPartCreaturePhysicsBodyFactory : CreaturePhysicsBodyFactory
    {
        public MultiPartCreaturePhysicsBodyFactory(World world) : base(world)
        {
        }

        public override IEnumerable<PhysicsBody> CreateBody(in DNA dna, OpenTK.Mathematics.Vector2 position)
        {
            var length = 3;
            var bodies = new List<CirclePhysicsBody>();

            for (int i = 0; i < length; i++)
            {
                var body = new CirclePhysicsBody(0.15f , i == 0 ? 10f : 0.3f)
                {
                    BodyType = BodyType.Dynamic,
                    LinearDrag =  1f,
                    AngularDrag = 2f
                };
                body.CreateBody(_world, (position - new OpenTK.Mathematics.Vector2(0, i * 0.25f)));
                bodies.Add(body);
            }

            for(int i = 1; i < length; i++)
            {
                var joint = JointFactory.CreateRopeJoint(_world,
                   bodies[i - 1].Body,
                   bodies[i].Body,
                   new tainicom.Aether.Physics2D.Common.Vector2(0, 0.05f),
                   new tainicom.Aether.Physics2D.Common.Vector2(0, -0.05f));
                joint.CollideConnected = true;
            }

            //PathManager.AttachBodiesWithRevoluteJoint(_world, bodies.Select(x => x._body).ToList(), new tainicom.Aether.Physics2D.Common.Vector2(0, 0.01f), new tainicom.Aether.Physics2D.Common.Vector2(0, -0.01f), false, false);

            bodies[0].Debug = true;

            return bodies;
        }
    }
}
