using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Data
{
    internal class InstancedVertexArrayObject : VertexArrayObject
    {
        public Vector2[] Positions { get; set; }
        public Vector3[] Colours { get; set; }
        public float[] Rotations { get; set; }
        public bool[] Visible { get; set; }

        public InstancedVertexArrayObject() : base(4) { }
        public InstancedVertexArrayObject(VertexArray va) : base(va, 4) { }
    }
}
