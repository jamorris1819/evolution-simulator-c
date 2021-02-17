using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Render.Core.Data.Primitives
{
    public class Polygon
    {
        public static VertexArray Generate(IList<Vector2> points)
        {
            var vertices = new List<Vector2>();
            var indices = new List<ushort>();

            vertices.Add(new Vector2(0, 0));
            vertices.AddRange(Centre(points));

            for (int i = 1; i < vertices.Count; i++)
            {
                indices.Add((ushort)i);
                indices.Add(0);
                indices.Add(i == vertices.Count - 1 ? (ushort)1 : (ushort)(i + 1));
            }

            return new VertexArray(
                vertices.Select(x => new Vertex(x)).ToArray(),
                indices.ToArray()
            );
        }

        public static VertexArray Generate(IList<Vertex> vertices)
        {
            /*for (int i = 0; i < vertices.Count; i++)
            {
                var previous = i == 0 ? vertices[vertices.Count - 1] : vertices[i - 1];
                var current = vertices[i];
                var next = i == vertices.Count - 1 ? vertices[0] : vertices[i + 1];

                var dirOne = current.Position - previous.Position;
                var dirTwo = next.Position - current.Position;

                var angle = (float)Math.PI * 0.5f;

                var newNormal = (Rotate(dirOne, -angle) + Rotate(dirTwo, -angle)).Normalized();

                var dx = next.Position.X - current.Position.X;
                var dy = next.Position.Y - current.Position.Y;

                vertices[i] = new Vertex(vertices[i].Position, vertices[i].Colour, newNormal);
            }*/

            var verts = new List<Vertex>();
            var indices = new List<ushort>();

            verts.Add(new Vertex(new Vector2(0, -0.01f)));
            verts.AddRange(vertices);

            for (int i = 1; i < vertices.Count; i++)
            {
                indices.Add((ushort)i);
                indices.Add(0);
                indices.Add(i == vertices.Count - 1 ? (ushort)1 : (ushort)(i + 1));
            }

           

            return new VertexArray(
                verts.ToArray(),
                indices.ToArray()
            );
        }

        private static IEnumerable<Vector2> Centre(IEnumerable<Vector2> vectors)
        {
            var count = vectors.Count();
            float averageX = (float)(vectors.Sum(x => x.X) / (float)count);
            float averageY = (float)(vectors.Sum(x => x.Y) / (float)count);

            return vectors.Select(x => x - new Vector2(averageX, averageY));
        }

        private static Vector2 Rotate(Vector2 v, float rad)
        {
            float sin = (float)Math.Sin(rad);
            float cos = (float)Math.Cos(rad);

            float tx = v.X;
            float ty = v.Y;
            v.X = (cos * tx) - (sin * ty);
            v.Y = (sin * tx) + (cos * ty);
            return v;
        }
    }
}
