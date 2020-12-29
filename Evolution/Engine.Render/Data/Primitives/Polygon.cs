using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Render.Data.Primitives
{
    public class Polygon : VertexArray
    {
        private IList<Vector2> _points;

        public Polygon(IList<Vector2> points)
        {
            _points = points;
        }

        public override void Generate()
        {
            var vertices = new List<Vector2>();
            var indices = new List<ushort>();

            vertices.Add(new Vector2(0, 0));
            vertices.AddRange(_points);

            for(int i = 1; i < vertices.Count; i++)
            {
                indices.Add((ushort)i);
                indices.Add(0);
                indices.Add(i == vertices.Count - 1 ? (ushort)1 : (ushort)(i + 1));
            }

            Vertices = vertices.Select(x => new Vertex(x)).ToArray();
            Indices = indices.ToArray();
        }
    }
}
