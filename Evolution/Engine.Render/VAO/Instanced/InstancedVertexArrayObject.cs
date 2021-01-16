using Engine.Render.Data;
using Engine.Render.VAO;
using Engine.Render.VAO.Instanced;
using OpenTK.Graphics.ES30;
using System;
using System.Linq;

namespace Engine.Render.VAO.Instanced
{
    internal class InstancedVertexArrayObject : VertexArrayObject
    {

        public Instance[] Instances { get; set; }

        public InstancedVertexArrayObject(VertexArray va, Instance[] instances) : base(va)
        {
            Instances = instances;
            Attributes.Add(new BufferAttribute<Instance>("Instances", Instances));
        }

        public override void Render()
        {
            Bind();
            GL.DrawElementsInstanced(PrimitiveType.Triangles, VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero, Instances.Length);
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
    }
}
