using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using Evolution.Genetics.Creature;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Environment.Life.Creatures
{
    public class PhysicsBodyBuilder
    {
        private readonly CreatureBodyBuilder _bodyBuilder;

        public PhysicsBodyBuilder()
        {
            _bodyBuilder = new CreatureBodyBuilder();
        }

        /// <summary>
        /// Creates a creature's physics body from DNA
        /// </summary>
        public PolygonDef[] CreateBody(in DNA dna)
            => new[] { CreateRect(dna) };

        private PolygonDef CreateRect(in DNA dna)
        {
            var curve = _bodyBuilder.CreateThoraxCurve(dna).Select(x => x * 4f);

            float top = curve.First().Y;
            float right = curve.OrderByDescending(x => x.X).First().X;
            float bottom = curve.Last().Y;

            var shape = new PolygonShape();
            /*shape.Set(new[]
            {
                    new Vec2(-right, top),
                    new Vec2(-right, bottom),
                    new Vec2(right, bottom),
                    new Vec2(right, top)
            }, 3);*/
            var height = top + System.Math.Abs(bottom);

            var newTop = height / 2.0f;

            shape.SetAsBox(right, newTop, new Vec2(0, -(newTop - top)), 0);
            //shape.SetAsBox(0.2f, 0.2f);

            return new PolygonDef()
            {
                Vertices = shape.Vertices,
                VertexCount = shape.Vertices.Length,
                Density = 1,
                Friction = 0.3f,
                Restitution = 0.3f
            };
        }
    }
}
