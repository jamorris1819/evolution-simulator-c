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
        private FixtureDef _fixtureDef;

        public Body Body { get; private set; }

        public bool Initialised { get; private set; }

        public Vector2 Position
        {
            get
            {
                var pos = Body.GetPosition();

                return new Vector2(pos.X, pos.Y);
            }
        }

        public PhysicsBody(BodyDef bodyDef, FixtureDef fixtureDef)
        {
            _bodyDef = bodyDef;
            _fixtureDef = fixtureDef;
        }

        public void Initialise(Func<BodyDef, Body> createBody)
        {
            if (Initialised) throw new Exception("Physics body is already initialised!");
            Body = createBody(_bodyDef);
            Body.CreateFixture(_fixtureDef);
            Body.SetMassFromShapes();
            Initialised = true;

            
        }
    }
}
