using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace Engine.Physics.Core.Shapes
{
    public class CirclePhysicsBody : PhysicsBody
    {
        private readonly float _radius;

        public CirclePhysicsBody(float radius, float density) : base(density)
        {
            _radius = radius;
        }

        public override void CreateBody(World world, Vector2 pos)
        {
            _body = world.CreateCircle(_radius, Density, new tainicom.Aether.Physics2D.Common.Vector2(pos.X, pos.Y), BodyType);
            _body.LinearDamping = LinearDrag;
            _body.AngularDamping = AngularDrag;

            Initialised = true;
        }
    }
}
