using OpenTK.Mathematics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Engine.Physics.Core
{
    public abstract class PhysicsBody
    {
        public bool Debug { get; set; } // TODO: phase this out

        /// <summary>
        /// The density of the physics object
        /// </summary>
        public float Density { get; protected set; }

        /// <summary>
        /// The linear drag of the physics object
        /// </summary>
        public float LinearDrag { get; set; }

        /// <summary>
        /// The angular drag of the physics object
        /// </summary>
        public float AngularDrag { get; set; }

        /// <summary>
        /// The box2d body.
        /// </summary>
        public Body Body { get; protected set; }

        /// <summary>
        /// The position of the physics object
        /// </summary>
        public Vector2 Position
        {
            get
            {
                var pos = Body.Position;

                return new Vector2(pos.X, pos.Y) * PhysicsSettings.InvScale;
            }
            set
            {
                var newPos = value * PhysicsSettings.Scale;
                Body.Position = new tainicom.Aether.Physics2D.Common.Vector2(newPos.X, newPos.Y);
            }
        }

        /// <summary>
        /// The rotation of the physics object
        /// </summary>
        public float Rotation
        {
            get => Body.Rotation;
            set => Body.Rotation = value;
        }

        /// <summary>
        /// The body type of the physics object
        /// </summary>
        public BodyType BodyType { get; set; } = BodyType.Static;

        public Vector2 LinearVelocity => new Vector2(Body.LinearVelocity.X, Body.LinearVelocity.Y);

        public float AngularVelocity => Body.AngularVelocity;

        public PhysicsBody(float density)
        {
            Density = density;
        }

        /// <summary>
        /// Abstract method for creating the physics body
        /// </summary>
        protected abstract void CreateBodyImpl(World world, Vector2 pos);

        /// <summary>
        /// Creates the body at the specified location
        /// </summary>
        public void CreateBody(World world, Vector2 pos)
            => CreateBodyImpl(world, pos * PhysicsSettings.Scale);

        /// <summary>
        /// Applies a force to the physics body
        /// </summary>
        public void ApplyForce(Vector2 force)
        {
            force *= PhysicsSettings.Scale;
            Body.ApplyForce(new tainicom.Aether.Physics2D.Common.Vector2(force.X, force.Y));
        }

        /// <summary>
        /// Applies a force in the direction the physics body is facing
        /// </summary>
        public void MoveForward(float force)
            => ApplyForce(new Vector2(-(float)System.Math.Sin(Rotation) * force, (float)System.Math.Cos(Rotation) * force));

        /// <summary>
        /// Apply torque to the physics body
        /// </summary>
        public void ApplyTorque(float torque)
        {
            torque *= PhysicsSettings.Scale;
            Body.ApplyTorque(torque);
        }
    }
}
