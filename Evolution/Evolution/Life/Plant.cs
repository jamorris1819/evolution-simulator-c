using Engine.Core;
using Engine.Core.Components;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.VAO.Instanced;
using Evolution.Life.DNA;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Evolution.Life
{
    public class Plant
    {
        private Leaf _leaf;
        private PlantDNA _dna;

        public Plant(PlantDNA dna)
        {
            _dna = dna;
            _leaf = new Leaf(_dna.LeafShape);
        }

        public VertexArray GenerateShape()
        {
            var leafBody = _leaf.Generate(16);

            var body = new VertexArray(new Vertex[0], new ushort[0]);

            int leafCount = _dna.Leaves;
            float step = (float)(Math.PI * 2) / leafCount;

            for (int i = 0; i < leafCount; i++)
            {
                var newLeaf = leafBody;
                newLeaf = VertexHelper.Rotate(newLeaf, i * step);
                body = VertexHelper.Combine(body, newLeaf);
            }

            body = VertexHelper.Scale(body, 0.1f);
            body = VertexHelper.SetColour(body, new Vector3(0, 0.6f, 0));
            return body;
        }

        /*public static Entity Generate(List<Vector2> points)
        {
            Entity entity = new Entity("plant");

            var tri = layer(0);
            var tri2 = VertexHelper.SetColour(layer(45), new Vector3(1, 0, 0));
            tri = VertexHelper.Combine(tri, tri2);



            Random random = new Random();

            int ee = 0;

            var instances = points.Select(x => new Instance()
            {
                Position = x,
                Colour = new Vector3(0, 0.5f, 0) + new Vector3((float)(random.NextDouble() * 0.1f))
            }).ToArray();

            entity.AddComponent(new RenderComponent(tri, new Engine.Render.Core.VAO.Instanced.InstanceSettings()
            {
                Instances = instances
            })
            { MinZoom = 0.1f, Shader = Engine.Render.Core.Shaders.Enums.ShaderType.InstancedRotated });
            entity.AddComponent(new PositionComponent());

            return entity;
        }

        private VertexArray GenerateRing(int count)
        {

        }

        private static VertexArray layer(float angle)
        {
            var tri = new VertexArray(new Vertex[0], new ushort[0]);

            int count = 6;
            float step = (float)(Math.PI * 2) / count;

            
        }*/
    }
}
