using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Render.Data
{
    public class VertexArray
    {
        public Vertex[] Vertices { get; protected set; }
        public ushort[] Indices { get; protected set; }

        public virtual void Generate() { }

        public void SetColour(Vector3 colour)
        {
            foreach(Vertex vertex in Vertices)
            {
                vertex.Colour = colour;
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
            for(int i = 0; i< Vertices.Length;i++)
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

        public VertexArray Copy()
        {
            return new VertexArray()
            {
                Vertices = Vertices,
                Indices = Indices
            };
        }

        protected void GenerateIndices()
            => Indices = Enumerable.Range(0, Vertices.Length).Select(x => (ushort)x).ToArray();
    }
}
