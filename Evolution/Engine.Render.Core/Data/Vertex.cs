using Engine.Render.Core.Attributes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data
{
    public readonly struct Vertex
    {
        public static int BytesPerVertex = 8;

        [AttributeName("vPosition", 3, OpenTK.Graphics.ES30.VertexAttribPointerType.Float)]
        public Vector3 Position { get; }

        [AttributeName("vColour", 3, OpenTK.Graphics.ES30.VertexAttribPointerType.Float)]
        public Vector3 Colour { get; }

        [AttributeName("vNormal", 2, OpenTK.Graphics.ES30.VertexAttribPointerType.Float)] // todo: could this be vector3 and be used to add outline depth..?
        public Vector2 Normal { get; }

        public Vertex(Vector3 pos) : this(pos, new Vector3(0, 0, 0)) { }

        public Vertex(Vector3 pos, Vector3 colour) : this(pos, colour, (new Vector2(pos.X, pos.Y)).Normalized()) { }


        public Vertex(Vector3 pos, Vector3 colour, Vector2 normal)
        {
            Position = pos;
            Colour = colour;
            Normal = normal;
            if (double.IsNaN(normal.X)) Normal = new Vector2(0, 0);
        }
    }
}
