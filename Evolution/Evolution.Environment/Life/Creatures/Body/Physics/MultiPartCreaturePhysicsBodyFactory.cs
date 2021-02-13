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
            var length = 20;
            var bodies = new List<CirclePhysicsBody>();

            var bc = new BezierCurveQuadric(new OpenTK.Mathematics.Vector2(0, 1), new OpenTK.Mathematics.Vector2(1, 0.7f), new OpenTK.Mathematics.Vector2(0.3f, 1.3f));
            for (int i = 0; i < length; i++)
            {
                var body = new CirclePhysicsBody(0.15f * bc.CalculatePoint((float)i / (float)(length - 1)).Y , i == 0 ? 10f : 0.3f)
                {
                    BodyType = BodyType.Dynamic,
                    LinearDrag =  i == 0 ? 1f : 10f,
                    AngularDrag = 2f
                };
                //body.CreateBody(_world, position - new OpenTK.Mathematics.Vector2(0, i * 0.25f));w
                bodies.Add(body);
            }

            for(int i = 1; i < length; i++)
            {
                /*var joint = new RopeJoint(bodies[i - 1]._body, bodies[i]._body, new tainicom.Aether.Physics2D.Common.Vector2(0), new tainicom.Aether.Physics2D.Common.Vector2(0));
                joint.MaxLength = 0.0001f;
                joint.Broke += Joint_Broke;
                _world.Add(joint);*/
                //JointFactory.CreateRevoluteJoint(_world, bodies[i - 1]._body, bodies[i]._body, new Vector2(0, i * -0.1f));

                bodies[i].SetParent(bodies[i - 1]);
            }

            //PathManager.AttachBodiesWithRevoluteJoint(_world, bodies.Select(x => x._body).ToList(), new tainicom.Aether.Physics2D.Common.Vector2(0, 0.01f), new tainicom.Aether.Physics2D.Common.Vector2(0, -0.01f), false, false);

            bodies[0].Debug = true;

            return bodies;
        }
    }
}
