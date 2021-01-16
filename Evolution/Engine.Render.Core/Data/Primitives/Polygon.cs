using OpenTK.Mathematics;
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
            vertices.AddRange(points);

            for (int i = 1; i < vertices.Count; i++)
            {
                indices.Add((ushort)i);
                indices.Add(0);
                indices.Add(i == vertices.Count - 1 ? (ushort)1 : (ushort)(i + 1));
            }

            return new VertexArray()
            {
                Vertices = vertices.Select(x => new Vertex(x)).ToArray(),
                Indices = indices.ToArray()
            };
        }
    }
}
