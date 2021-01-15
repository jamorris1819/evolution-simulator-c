using Engine.Render.Attributes;
using Engine.Render.Shaders;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Render.Data
{
    internal class VertexArrayObject
    {
        private int _handle;
        public bool Initialised { get; protected set; }
        public bool Reload { get; set; }
        public bool Enabled { get; set; }
        public VertexArray VertexArray { get; set; }
        public IVertexBufferObject[] VBO { get; set; }

        public IVertexAttribute[] Attributes { get; protected set; }

        public VertexArrayObject(VertexArray va)
        {
            Enabled = true;
            Initialised = false;

            VertexArray = va;

            Attributes = new[] {
                new VertexAttribute<Vertex>(VertexArray.Vertices)
                {
                    Indices = VertexArray.Indices
                }
            };

            VBO = Attributes.SelectMany(x => x.GenerateBufferObjects()).ToArray();
        }

        public void Initialise(IList<Shader> shaders)
        {
            if (Initialised) throw new Exception("The VAO is already initialised");

            // Generate VAO buffer
            _handle = GL.GenVertexArray();
            Initialised = true;

            Bind();

            for (int i = 0; i < VBO.Length; i++)
            {
                VBO[i].Initialise(shaders);
            }
        }

        public void Load()
        {
            Bind();

            for (int i = 0; i < VBO.Length; i++)
            {
                VBO[i].Load();
            }
        }

        public void Unload()
        {
            for (int i = 0; i < VBO.Length; i++)
            {
                VBO[i].Unload();
            }
        }

        public void Bind()
        {
            if (!Initialised) throw new Exception("The VAO can't be bound as it's not been initialised");

            GL.BindVertexArray(_handle);
        }

        public virtual void Render()
        {
            Bind();
            GL.DrawElements(PrimitiveType.Triangles, VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero);
        }
    }
}
