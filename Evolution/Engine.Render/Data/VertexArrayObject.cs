using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Data
{
    internal class VertexArrayObject
    {
        public bool Initialised { get; set; }
        public bool Reload { get; set; }
        public bool Enabled { get; set; }
        public VertexArray VertexArray { get; set; }
        public int[] VAO { get; set; }
        public int[] VBO { get; set; }

        public VertexArrayObject() : this(null, 2) { }

        public VertexArrayObject(int bufferCount) : this(null, bufferCount) { }

        public VertexArrayObject(VertexArray va) : this(va, 2) { }

        public VertexArrayObject(VertexArray va, int bufferCount)
        {
            Enabled = true;
            Initialised = false;

            VAO = new int[1];
            VBO = new int[bufferCount];

            VertexArray = va;
        }
    }
}
