using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Data
{
    public class Vertex
    {
        public static int BytesPerVertex = 5;

        public Vector2 Position { get; set; }
        public Vector3 Colour { get; set; }

        public Vertex(Vector2 pos) : this(pos, new Vector3(1, 1, 1)) { }

        public Vertex(Vector2 pos, Vector3 colour)
        {
            Position = pos;
            Colour = colour;
        }
    }
}
