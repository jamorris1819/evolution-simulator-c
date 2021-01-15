using Engine.Render.Attributes;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Data
{
    internal class InstancedVertexArrayObject : VertexArrayObject
    {
        
        public Instance[] Instances { get; set; }

        public InstancedVertexArrayObject(VertexArray va, Instance[] instances) : base(va)
        {
            Instances = instances;
            Attributes.Add(new BufferAttribute<Instance>(Instances));
        }

        public override void Render()
        {
            Bind();
            GL.DrawElementsInstanced(PrimitiveType.Triangles, VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero, Instances.Length);
        }
    }
}
