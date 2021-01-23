using OpenTK.Mathematics;
using System;
using System.Linq;

namespace Engine.Render.Core.Data
{
    public readonly struct VertexArray
    {
        public Vertex[] Vertices { get; }

        public ushort[] Indices { get; }

        public VertexArray(Vertex[] verts, ushort[] ind)
        {
            Vertices = verts;
            Indices = ind;
        }
    }
}
