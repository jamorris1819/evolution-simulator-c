using OpenTK.Mathematics;
using System;
using System.Linq;

namespace Engine.Render.Core.Data
{
    public struct VertexArray
    {
        public Vertex[] Vertices { get; set; }

        public ushort[] Indices { get; set; }

        public void SetColour(Vector3 colour)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].Colour = colour;
            }
        }

        public void Add(VertexArray va)
        {
            Vertex[] vertices = new Vertex[Vertices.Length + va.Vertices.Length];
            ushort[] indices = new ushort[Indices.Length + va.Indices.Length];

            for (int i = 0; i < Vertices.Length; i++)
            {
                vertices[i] = Vertices[i];
            }

            for (int i = 0; i < Indices.Length; i++)
            {
                indices[i] = Indices[i];
            }

            for (int i = 0; i < va.Vertices.Length; i++)
            {
                vertices[i + Vertices.Length] = va.Vertices[i];
            }

            for (int i = 0; i < va.Indices.Length; i++)
            {
                indices[i + Indices.Length] = (ushort)(va.Indices[i] + (ushort)Vertices.Length);
            }

            Vertices = vertices;
            Indices = indices;
        }

        public void Translate(Vector2 pos)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].Position += pos;
            }
        }

        public void Rotate(float rotation)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                var pos = Vertices[i].Position;
                float s = (float)Math.Sin(rotation);
                float c = (float)Math.Cos(rotation);

                Vertices[i].Position = new Vector2(pos.X * c - pos.Y * s, pos.X * s + pos.Y * c);
            }
        }

        public void Scale(float scale)
        {
            Vertices = Vertices.Select(x => new Vertex(x.Position * scale)).ToArray();
        }

        public void GenerateIndices()
            => Indices = Enumerable.Range(0, Vertices.Length).Select(x => (ushort)x).ToArray();
    }
}
