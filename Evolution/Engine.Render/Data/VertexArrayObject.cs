using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Data
{
    internal class VertexArrayObject
    {
        public bool Initialised { get; set; }
        public bool Enabled { get; set; }
        public VertexArray VertexArray { get; set; }
        public int[] VAO { get; set; }
        public int[] VBO { get; set; }

        public VertexArrayObject() : this(null)
        {
        }

        public VertexArrayObject(VertexArray va)
        {
            Enabled = true;
            Initialised = false;

            VAO = new int[1];
            VBO = new int[2];

            VertexArray = va;
        }
    }
}
