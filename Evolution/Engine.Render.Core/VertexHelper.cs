using Engine.Render.Core.Data;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Render.Core
{
    /// <summary>
    /// Utility that offers methods for manipulating vertices and vertex arrays
    /// </summary>
    public static class VertexHelper
    {
        public static VertexArray SetColour(in VertexArray va, Vector3 colour)
        {
            var vertices = va.Vertices.Select(x => SetColour(x, colour));

            return new VertexArray(vertices.ToArray(), va.Indices);
        }

        public static Vertex SetColour(in Vertex v, Vector3 colour) => new Vertex(v.Position, colour, v.Normal);

        public static VertexArray Translate(in VertexArray va, Vector2 pos)
        {
            var vertices = va.Vertices.Select(x => Translate(x, pos));

            return new VertexArray(vertices.ToArray(), va.Indices);
        }

        public static Vertex Translate(in Vertex v, Vector2 pos) => new Vertex(v.Position + pos.ToVector3(), v.Colour, v.Normal);

        public static VertexArray Scale(in VertexArray va, float scale)
        {
            var vertices = va.Vertices.Select(x => Scale(x, scale));

            return new VertexArray(vertices.ToArray(), va.Indices);
        }

        public static Vertex Scale(in Vertex v, float scale) => new Vertex(v.Position * scale, v.Colour, v.Normal);

        public static VertexArray Multiply(in VertexArray va, Vector2 m)
        {
            var vertices = va.Vertices.Select(x => Multiply(x, m)).ToArray();
            return new VertexArray(vertices, va.Indices);
        }

        public static Vertex Multiply(in Vertex v, Vector2 m) => new Vertex(v.Position * m.ToVector3(), v.Colour, v.Normal);

        public static VertexArray Combine(in VertexArray va1, in VertexArray va2)
        {
            Vertex[] vertices = new Vertex[va1.Vertices.Length + va2.Vertices.Length];
            ushort[] indices = new ushort[va1.Indices.Length + va2.Indices.Length];

            for (int i = 0; i < va1.Vertices.Length; i++)
            {
                vertices[i] = va1.Vertices[i];
            }

            for (int i = 0; i < va1.Indices.Length; i++)
            {
                indices[i] = va1.Indices[i];
            }

            for (int i = 0; i < va2.Vertices.Length; i++)
            {
                vertices[i + va1.Vertices.Length] = va2.Vertices[i];
            }

            for (int i = 0; i < va2.Indices.Length; i++)
            {
                indices[i + va1.Indices.Length] = (ushort)(va2.Indices[i] + (ushort)va1.Vertices.Length);
            }

            return new VertexArray(vertices, indices);
        }

        public static VertexArray Rotate(in VertexArray va, float rotation)
        {
            var vertices = va.Vertices.Select(x => (Rotate(x, rotation)));

            return new VertexArray(vertices.ToArray(), va.Indices);
        }

        public static Vertex Rotate(in Vertex v, float rotation)
        {
            var pos = v.Position;
            float s = (float)Math.Sin(rotation);
            float c = (float)Math.Cos(rotation);

            var newPos = new Vector3(pos.X * c - pos.Y * s, pos.X * s + pos.Y * c, v.Position.Z);

            return new Vertex(newPos, v.Colour, v.Normal);
        }
    }
}
