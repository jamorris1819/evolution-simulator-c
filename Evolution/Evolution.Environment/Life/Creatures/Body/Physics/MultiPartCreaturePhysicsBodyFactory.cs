using Engine.Physics.Core;
using Engine.Physics.Core.Shapes;
using Evolution.Environment.Life.Creatures.Body.Visual;
using Evolution.Genetics;
using Evolution.Genetics.Creature;
using Evolution.Genetics.Creature.Modules;
using Evolution.Genetics.Creature.Modules.Body;
using Evolution.Genetics.Creature.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var bodyModule = (MultiPartBody)dna.GetModule(ModuleType.Body);

            var points = NoiseBodyGenerator.CreateThoraxCurve(dna);

            var first = points.First().Y * PhysicsSettings.Scale;
            var last = points.Last().Y * PhysicsSettings.Scale;

            var buffer = 0.1f;

            var length = DNAReader.ReadValueInt(bodyModule.Length, DNAReader.BodySegmentCountReader);
            var bodies = new List<CirclePhysicsBody>();

            for (int i = 0; i < length; i++)
            {
                var body = new CirclePhysicsBody(0.15f, i == 0 ? 5f : 3f)
                {
                    BodyType = tainicom.Aether.Physics2D.Dynamics.BodyType.Dynamic,
                    LinearDrag =   i == length - 1 ? 4f : 2f,
                    AngularDrag = 2f
                };
                body.CreateBody(_world, position - new OpenTK.Mathematics.Vector2(0, i * (first - last) * PhysicsSettings.InvScale));
                bodies.Add(body);
            }

            for (int i = 1; i < length; i++)
            {
                var joint = JointFactory.CreateRevoluteJoint(_world,
                   bodies[i - 1].Body,
                   bodies[i].Body,
                   new tainicom.Aether.Physics2D.Common.Vector2(0, last + buffer),
                   new tainicom.Aether.Physics2D.Common.Vector2(0, first - buffer));
                joint.LimitEnabled = true;
                joint.CollideConnected = false;
                joint.LowerLimit = -1;
                joint.UpperLimit = 1;

                if(i > 1 && i < length * 0.5f)
                {
                    joint.LowerLimit = -0.75f;
                    joint.UpperLimit = 0.75f;
                }
            }

            //PathManager.AttachBodiesWithRevoluteJoint(_world, bodies.Select(x => x._body).ToList(), new tainicom.Aether.Physics2D.Common.Vector2(0, 0.01f), new tainicom.Aether.Physics2D.Common.Vector2(0, -0.01f), false, false);

            bodies[0].Debug = true;
            bodies[1].Debug = true;

            return bodies;
        }
    }
}
