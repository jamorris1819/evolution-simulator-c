using DotnetNoise;
using Engine.Render.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Data.Primitives;
using Evolution.Genetics;
using Evolution.Genetics.Creature;
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
    public class MultiPartCreatureBodyFactory : CreatureBodyFactory
    {
        public override IEnumerable<VertexArray> CreateBody(in DNA dna)
        {
            var thorax = NoiseBodyGenerator.CreateBody(dna);


            var vertexArrays = new List<VertexArray>();

            var bodyModule = (MultiPartBody)dna.GetModule(ModuleType.Body);

            var length = DNAReader.ReadValueInt(bodyModule.Length, DNAReader.BodySegmentCountReader);

            for (int i = 0; i < length; i++)
            {
                if(i % 2 == 1) vertexArrays.Add(thorax);
                else vertexArrays.Add(VertexHelper.SetColour(thorax, thorax.Vertices[0].Colour * 0.85f));
            }

            var eyes = CreateEyes(dna);
            vertexArrays[0] = VertexHelper.SetColour(vertexArrays[0], vertexArrays[0].Vertices[0].Colour * new Vector3(0.35f));
            vertexArrays[0] = VertexHelper.Combine(vertexArrays[0], eyes);
            return vertexArrays;// new[] { VertexHelper.Combine(thorax, eyes) };
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
            var points = NoiseBodyGenerator.CreateThoraxCurve(dna).ToArray();
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
