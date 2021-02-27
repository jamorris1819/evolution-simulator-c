using Engine.Render.Core.Data;
using Engine.Render.Core.Shaders;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Render.Core.VAO
{
    public class VertexArrayObject
    {
        private int _handle;
        private float _alpha;

        public bool Initialised { get; protected set; }
        public bool NeedsUpdate => VBO.Any(x => x.NeedsUpdate);
        public bool Enabled { get; set; }
        public VertexArray VertexArray { get; set; }
        public IVertexBufferObject[] VBO { get; set; }

        public IList<IBufferAttribute> Attributes { get; protected set; }

        public VertexArrayObject(VertexArray va)
        {
            _alpha = 1;
            Enabled = true;
            Initialised = false;

            VertexArray = va;

            Attributes = new List<IBufferAttribute>() { 
                new BufferAttribute<Vertex>("Vertex data", VertexArray.Vertices)
                {
                    Indices = VertexArray.Indices
                }
            };
        }

        public virtual void Initialise(Shader[] shaders)
        {
            if (Initialised) throw new Exception("The VAO is already initialised");
            AddAttributes();
            VBO = Attributes.SelectMany(x => x.GenerateBufferObjects()).ToArray();

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

        public void Reload()
        {
            Bind();

            var needsReload = VBO.Where(x => x.NeedsUpdate).ToArray();
            for(int i = 0; i < needsReload.Length; i++)
            {
                needsReload[i].Reload();
            }
        }

        public void Unload()
        {
            for (int i = 0; i < VBO.Length; i++)
            {
                VBO[i].Unload();
            }

            throw new NotImplementedException();
        }

        public void Bind()
        {
            if (!Initialised) throw new Exception("The VAO can't be bound as it's not been initialised");

            GL.BindVertexArray(_handle);
        }

        public virtual void Render(Shader shader)
        {
            if (!Enabled) return;
            GL.DrawElements(shader.PrimitiveType, VertexArray.Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero);
        }

        protected virtual void AddAttributes() { }
    }
}
