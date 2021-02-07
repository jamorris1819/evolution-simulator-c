using DotnetNoise;
using Engine.Render.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Data.Primitives;
using Evolution.Genetics.Creature;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Environment.Life.Creatures.Body.Visual
{
    public class SinglePartCreatureBodyFactory : CreatureBodyFactory
    {
        public override IEnumerable<VertexArray> CreateBody(in DNA dna)
        {
            var thorax = CreateThorax(dna);
            var eyes = CreateEyes(dna);

            return new[] { VertexHelper.Combine(thorax, eyes) };
        }

        /// <summary>
        /// Creates the Thorax curve (half of the shape)
        /// </summary>
        public IEnumerable<Vector2> CreateThoraxCurve(DNA dna)
        {
            FastNoise noise = new FastNoise();
            noise.Octaves = 5;
            noise.UsedNoiseType = FastNoise.NoiseType.PerlinFractal;

            int steps = dna.BodySteps.GetPhenotype().Data;
            float stepSize = (float)Math.PI / steps;

            for (int i = 0; i < steps; i++)
            {
                var point = new Vector2((float)Math.Sin(stepSize * i), (float)Math.Cos(stepSize * i));
                point.Normalize();
                var dist = Math.Abs(noise.GetNoise(i + dna.BodyOffset.GetPhenotype().Data, 0));
                point *= Math.Max(dist, 0.1f);

                yield return point;
            }
        }

        private VertexArray CreateThorax(in DNA dna)
        {
            float borderThickness = 0.01f;
            var thoraxPoints = CreateThoraxCurve(dna).ToList();
            var borderPoints = thoraxPoints.Select(x => x + x.Normalized() * borderThickness).ToList();

            var size = thoraxPoints.Count;

            // Add thorax points
            Vector2[] flippedPoints = new Vector2[size];
            thoraxPoints.CopyTo(flippedPoints, 0);
            flippedPoints = flippedPoints.Reverse().Select(x => x * new Vector2(-1, 1)).ToArray();
            thoraxPoints.AddRange(flippedPoints);

            // Add thorax border points
            flippedPoints = new Vector2[size];
            borderPoints.CopyTo(flippedPoints, 0);
            flippedPoints = flippedPoints.Reverse().Select(x => x * new Vector2(-1, 1)).ToArray();
            borderPoints.AddRange(flippedPoints);

            var shape = Polygon.Generate(thoraxPoints);
            shape = VertexHelper.SetColour(shape, Phenotype<Vector3>.GetFromGenotypes(dna.ColourR, dna.ColourG, dna.ColourB).Data);

            var border = Polygon.Generate(borderPoints);
            border = VertexHelper.SetColour(border, new Vector3(0));

            return VertexHelper.Combine(border, shape);
        }

        private VertexArray CreateEye(Vector2 pos)
        {
            var size = pos.Length * 0.5f;
            size = Math.Min(size, 0.1f);

            // Create white of eye
            var eye = VertexHelper.Translate(Circle.Generate(size, 32), pos);
            eye = VertexHelper.SetColour(eye, new Vector3(1));

            // Create black border around eye
            var eyeBorder = VertexHelper.Translate(Circle.Generate(size + 0.01f, 32), pos);
            eyeBorder = VertexHelper.SetColour(eyeBorder, new Vector3(0));

            eye = VertexHelper.Combine(eyeBorder, eye);

            // Create pupil
            var pupil = VertexHelper.Translate(Circle.Generate(size * 0.3f, 26), pos + new Vector2(0, size * 0.5f));
            pupil = VertexHelper.SetColour(pupil, new Vector3(0));

            return VertexHelper.Combine(eye, pupil);
        }

        private VertexArray CreateEyes(in DNA dna)
        {
            var points = CreateThoraxCurve(dna).ToArray();
            var length = points.Length;
            int dist = (int)(length * 0.3f);

            var point = points[dist];

            var eye1 = CreateEye(point);
            var eye2 = CreateEye(point * new Vector2(-1, 1));

            var eyes = VertexHelper.Combine(eye1, eye2);

            return eyes;
        }
    }
}
