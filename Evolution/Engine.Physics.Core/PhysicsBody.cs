using OpenTK.Mathematics;
using System;
using tainicom.Aether.Physics2D.Dynamics;

namespace Engine.Physics.Core
{
    public abstract class PhysicsBody
    {
        protected Body _body;
        
        public bool Initialised { get; private set; }

        public bool Debug { get; set; }

        public float Density { get; protected set; }

        public float LinearDrag { get; set; }

        public float AngularDrag { get; set; }

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

        public abstract void CreateBody(World world, Vector2 position);

        public void CreateBody(World world) => CreateBody(world, new Vector2());

        public void Initialise(World world, Vector2 position = default)
        {
            if (Initialised) throw new Exception("Physics body is already initialised!");

            CreateBody(world, position);

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
    }
}
