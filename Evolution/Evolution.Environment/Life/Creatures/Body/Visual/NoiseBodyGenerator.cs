using DotnetNoise;
using Engine.Render.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Data.Primitives;
using Evolution.Genetics;
using Evolution.Genetics.Creature.Modules;
using Evolution.Genetics.Creature.Modules.Body;
using Evolution.Genetics.Creature.Readers;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Environment.Life.Creatures.Body.Visual
{
    /// <summary>
    /// Util class used for generating bodies from noise
    /// </summary>
    public static class NoiseBodyGenerator
    {
        /// <summary>
        /// Creates the Thorax curve (half of the shape)
        /// </summary>
        public static IEnumerable<Vector2> CreateThoraxCurve(DNA dna)
        {
            var bodyModule = (BodyModule)dna.GetModule(ModuleType.Body);

            FastNoise noise = new FastNoise();
            noise.Octaves = 5;
            noise.UsedNoiseType = FastNoise.NoiseType.PerlinFractal;

            var offset = DNAReader.ReadValueFloat(bodyModule.BodyOffset, DNAReader.BodyOffsetsReader);

            int steps = DNAReader.ReadValueInt(bodyModule.BodySteps, DNAReader.BodyStepsReader);
            float stepSize = (float)Math.PI / steps;

            for (int i = 0; i < steps; i++)
            {
                var point = new Vector2((float)Math.Sin(stepSize * i), (float)Math.Cos(stepSize * i));
                point.Normalize();
                var dist = Math.Abs(noise.GetNoise(i + offset, 0));
                point *= Math.Max(dist, 0.1f);

                yield return point;
            }
        }

        public static VertexArray CreateBody(in DNA dna)
        {
            var bodyModule = (MultiPartBody)dna.GetModule(ModuleType.Body);

            var thoraxPoints = CreateThoraxCurve(dna).ToList();

            var size = thoraxPoints.Count;

            // Add thorax points
            Vector2[] flippedPoints = new Vector2[size];
            thoraxPoints.CopyTo(flippedPoints, 0);
            flippedPoints = flippedPoints.Reverse().Select(x => x * new Vector2(-1, 1)).ToArray();
            thoraxPoints.AddRange(flippedPoints);

            var shape = Polygon.Generate(thoraxPoints.ToList(), true);
            shape = VertexHelper.SetColour(shape, DNAReader.ReadValueColour(bodyModule.ColourR, bodyModule.ColourG, bodyModule.ColourB));

            return shape;
        }
    }
}
