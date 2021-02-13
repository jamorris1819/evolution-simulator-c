using OpenTK.Mathematics;
using System;
using tainicom.Aether.Physics2D.Dynamics;

namespace Engine.Physics.Core
{
    public abstract class PhysicsBody
    {
        public Body _body; // TODO: change back to protected
        
        public bool Initialised { get; protected set; }

        public bool Debug { get; set; }

        public float Density { get; protected set; }

        public float LinearDrag { get; set; }

        public float AngularDrag { get; set; }

        public PhysicsBody Parent { get; private set; }

        public Vector2 Position
        {
            get
            {
                var pos = _body.Position;

                return new Vector2(pos.X, pos.Y);
            }
            set
            {
                _body.Position = new tainicom.Aether.Physics2D.Common.Vector2(value.X, value.Y);
            }
        }

        public float Rotation
        {
            get => _body.Rotation;
            set => _body.Rotation = value;
        }

        public BodyType BodyType { get; set; }

        public PhysicsBody(float density)
        {
            Density = density;
        }

        public abstract void CreateBody(World world, Vector2 pos);

        public void Initialise(World world, Vector2 pos)
        {
            if (Initialised) throw new Exception("Physics body is already initialised!");

            CreateBody(world, pos);

            Initialised = true;
        }

        public void ApplyForce(Vector2 force)
        {
            _body.ApplyForce(new tainicom.Aether.Physics2D.Common.Vector2(force.X, force.Y));
        }

        public void MoveForward(float force)
            => ApplyForce(new Vector2(-(float)System.Math.Sin(Rotation) * force, (float)System.Math.Cos(Rotation) * force));

        public void ApplyTorque(float torque)
        {
            _body.ApplyTorque(torque);
        }

        public void SetParent(PhysicsBody body)
        {
            Parent = body;
        }
    }
}
