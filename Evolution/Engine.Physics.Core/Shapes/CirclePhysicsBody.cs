using OpenTK.Mathematics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Engine.Physics.Core.Shapes
{
    public class CirclePhysicsBody : PhysicsBody
    {
        private readonly float _radius;

        public CirclePhysicsBody(float radius, float density) : base(density)
        {
            _radius = radius * Physics.Scale;
        }

        protected override void CreateBodyImpl(World world, Vector2 pos)
        {
            Body = world.CreateCircle(_radius, Density, new tainicom.Aether.Physics2D.Common.Vector2(pos.X, pos.Y), BodyType);
            Body.LinearDamping = LinearDrag;
            Body.AngularDamping = AngularDrag;
        }
    }
}
