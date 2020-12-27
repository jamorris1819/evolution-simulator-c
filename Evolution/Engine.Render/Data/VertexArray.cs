using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Render.Data
{
    public abstract class VertexArray
    {
        public Vertex[] Vertices { get; protected set; }
        public ushort[] Indices { get; protected set; }

        public VertexArray()
        {
            Generate();
        }

        public abstract void Generate();

        public void SetColour(Vector3 colour)
        {
            foreach(Vertex vertex in Vertices)
            {
                vertex.Colour = colour;
            }
        }

        protected void GenerateIndices()
            => Indices = Enumerable.Range(0, Vertices.Length).Select(x => (ushort)x).ToArray();
    }
}
