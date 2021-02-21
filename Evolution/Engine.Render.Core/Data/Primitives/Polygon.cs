using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Render.Core.Data.Primitives
{
    public class Polygon
    {
        public static VertexArray Generate(IList<Vector2> points, bool centre = false)
        {
            var vertices = new List<Vertex>();
            var indices = new List<ushort>();

            points = centre ? Centre(points).ToList() : points;
            var tempVerts = points.Select(x => new Vertex(x)).ToArray();

            for (int i = 0; i < tempVerts.Length; i++)
            {
                var previous = i == 0 ? tempVerts[tempVerts.Length - 1] : tempVerts[i - 1];
                var current = tempVerts[i];
                var next = i == tempVerts.Length - 1 ? tempVerts[0] : tempVerts[i + 1];

                var dirOne = current.Position - previous.Position;
                var dirTwo = current.Position - next.Position;

                var angle = (float)Math.PI * 0.5f;

                var newNormal = (Rotate(dirOne, angle) + Rotate(dirTwo, -angle)).Normalized();

                var dx = next.Position.X - current.Position.X;
                var dy = next.Position.Y - current.Position.Y;

                tempVerts[i] = new Vertex(tempVerts[i].Position, tempVerts[i].Colour, newNormal);
            }

            for (int i = 0; i < tempVerts.Length; i++)
            {
                var v1 = tempVerts[i];
                var v2 = i == tempVerts.Length - 1 ? tempVerts[0] : tempVerts[i + 1];

                var v3 = new Vertex(new Vector2(0, 0));

                vertices.Add(v1);
                vertices.Add(v2);
                vertices.Add(v3);
            }


            return new VertexArray(
                vertices.ToArray(),
                Enumerable.Range(0, vertices.Count).Select(x => (ushort)x).ToArray()
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
