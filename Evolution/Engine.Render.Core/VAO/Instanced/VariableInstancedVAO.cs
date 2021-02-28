using Engine.Render.Core.Data;
using Engine.Render.Core.Shaders;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.VAO.Instanced
{
    public class VariableInstancedVAO : InstancedVertexArrayObject
    {
        public int Total { get; }

        public int Visible { get; set; }

        public VariableInstancedVAO(VertexArray va, Instance[] instances) : base(va, instances)
        {
            Random random = new Random();
            Total = instances.Length;
            Visible = (int)(Total * random.NextDouble());
        }

        public override void Render(Shader shader)
        {
            GL.DrawElementsInstanced(shader.PrimitiveType, VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero, Visible);
        }

        public void Add(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (Visible >= Total) break; ;
                Visible++;
            }
        }

        public void Remove(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (Visible == 0) break;
                Visible--;
            }
        }
    }
}
