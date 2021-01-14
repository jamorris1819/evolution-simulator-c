using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Render.Data.Primitives
{
    public class Triangle : VertexArray
    {
        private int _height;
        private int _width;

        public Triangle(int h, int w) : base()
        {
            _height = h;
            _width = w;
        }

        public override void Generate()
        {
            Vertices = new[]
            {
                new Vertex(new Vector2(-_width * 0.5f, 0)),
                new Vertex(new Vector2(_width * 0.5f, 0)),
                new Vertex(new Vector2(0, _height))
            };
            Indices = new[]
            {
                0, 1, 2
            }.Select(x => (ushort)x).ToArray();
        }
    }
}
