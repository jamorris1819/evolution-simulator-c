using Engine.Render.Core.Data;
using Engine.Render.Core.Shaders;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Render.Core.VAO.Instanced
{
    public class InstancedVertexArrayObject : VertexArrayObject
    {

        public Instance[] Instances { get; set; }

        public InstancedVertexArrayObject(VertexArray va, Instance[] instances) : base(va)
        {
            Instances = instances;
        }

        public override void Render(Shader shader)
        {
            GL.DrawElementsInstanced(shader.PrimitiveType, VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero, Instances.Length);
        }

        public void Update(Instance[] instances)
        {
            int changes = 0;
            for(int i = 0; i < instances.Length; i++)
            {
                int hash = instances[i].GetHashCode();
                int currHash = Instances[i].GetHashCode();
                if (hash == currHash) continue;

                Instances[i] = instances[i];
                changes++;
            }

            VBO.First(x => x.Name == "Instances").QueueReload();
        }

        public void Update(List<Instance> instances)
        {
            Instances = instances.ToArray();

            if (VBO == null) return;

            (VBO.First(x => x.Name == "Instances") as VertexBufferObject<Instance>).UpdateData(Instances);
            VBO.First(x => x.Name == "Instances").QueueReload();
        }

        protected override void AddAttributes()
        {
            Attributes.Add(new BufferAttribute<Instance>("Instances", Instances, BufferUsageHint.DynamicDraw));
        }
    }
}
