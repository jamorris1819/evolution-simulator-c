using Engine.Render.Core.Attributes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data
{
    public struct Vertex
    {
        public static int BytesPerVertex = 5;

        [AttributeName("vPosition", 2, OpenTK.Graphics.ES30.VertexAttribPointerType.Float)]
        public Vector2 Position { get; set; }

        [AttributeName("vColour", 3, OpenTK.Graphics.ES30.VertexAttribPointerType.Float)]
        public Vector3 Colour { get; set; }

        public Vertex(Vector2 pos) : this(pos, new Vector3(1, 1, 1)) { }

        public Vertex(Vector2 pos, Vector3 colour)
        {
            Position = pos;
            Colour = colour;
        }
    }
}
