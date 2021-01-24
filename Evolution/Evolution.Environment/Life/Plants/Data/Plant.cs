using Engine.Render.Core;
using Engine.Render.Core.Data;
using Evolution.Environment.Life.Plants.Data;
using Evolution.Genetics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

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

            body = VertexHelper.Scale(body, (float)(_dna.Size) / 100.0f);
            body = VertexHelper.SetColour(body, new Vector3(0, 0, 0));

            List<VertexArray> layers = new List<VertexArray>();
            step = (float)(Math.PI * 2) / _dna.Layers;

            for(int i = 0; i < _dna.Layers; i++)
            {
                var newLayer = VertexHelper.Rotate(body, step * (float)(i + 1));
                newLayer = VertexHelper.Scale(newLayer, (float)Math.Pow(0.9, i + 1));
                newLayer = VertexHelper.SetColour(newLayer, new Vector3(i * 0.03f));
                layers.Add(newLayer);
            }

            for(int i = 0; i <layers.Count; i++)
            {
                body = VertexHelper.Combine(body, layers[i]);
            }

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
