using Box2DX.Common;
using Box2DX.Dynamics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Physics.Core
{
    public class PhysicsBody
    {
        private BodyDef _bodyDef;
        private FixtureDef[] _fixtureDef;
        private Body _body;
        private float _scale = 25f;

        public bool Initialised { get; private set; }

        public bool Debug { get; set; }

        public Vector2 Position
        {
            get
            {
                var pos = _body.GetPosition();

                return new Vector2(pos.X, pos.Y) / _scale;
            }
            set
            {
                _body.SetPosition(new Vec2(value.X * _scale, value.Y * _scale));
            }
        }

        public float Angle
        {
            get => _body.GetAngle();
            set => _body.SetAngle(value);
        }

        public PhysicsBody(BodyDef bodyDef, FixtureDef fixtureDef)
        {
            _bodyDef = bodyDef;
            _fixtureDef = new[] { fixtureDef };
        }

        public PhysicsBody(BodyDef bodyDef, FixtureDef[] fixtureDef)
        {
            _bodyDef = bodyDef;
            _fixtureDef = fixtureDef;
        }

        public void Initialise(Func<BodyDef, Body> createBody)
        {
            if (Initialised) throw new Exception("Physics body is already initialised!");
            _body = createBody(_bodyDef);

            for (int i = 0; i < _fixtureDef.Length; i++)
            {
                _body.CreateFixture(_fixtureDef[i]);
            }

            _body.SetMassFromShapes();
            Initialised = true;
        }

        public void ApplyForce(Vector2 force)
        {
            force *= _body.GetMass();
            _body.ApplyForce(new Vec2(force.X * _scale, force.Y * _scale), _body.GetWorldCenter());
        }
    }
}
