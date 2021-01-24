using Engine.Render.Core.Attributes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data
{
    public readonly struct Vertex
    {
        public static int BytesPerVertex = 5;

        [AttributeName("vPosition", 2, OpenTK.Graphics.ES30.VertexAttribPointerType.Float)]
        public Vector2 Position { get; }

        [AttributeName("vColour", 3, OpenTK.Graphics.ES30.VertexAttribPointerType.Float)]
        public Vector3 Colour { get; }

        public Vertex(Vector2 pos) : this(pos, new Vector3(0, 0, 0)) { }

        public Vertex(Vector2 pos, Vector3 colour)
        {
            Position = pos;
            Colour = colour;
        }
    }
}
