using Engine.Render.Core.Attributes;
using Engine.Render.Core.Shaders;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Engine.Render.Core.VAO
{
    /// <summary>
    /// Handles the allocation and management of memory on the GPU
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class VertexBufferObject<T> : IVertexBufferObject where T: struct
    {
        private int _handle = -1;
        private T[] _buffer;
        private IList<Shader> _shaders;

        public T[] Buffer => _buffer;

        public string Name { get; }

        public BufferUsageHint BufferHint { get; set; }

        public BufferTarget BufferTarget { get; set; }

        public bool Initialised => _handle != -1;

        public bool NeedsUpdate { get; private set; }

        public VertexBufferObject(string name, T[] items, BufferUsageHint hint, BufferTarget target)
        {
            Name = name;
            _buffer = items;
            BufferHint = hint;
            BufferTarget = target;
        }

        public void Initialise(IList<Shader> shaders)
        {
            if (Initialised) throw new Exception($"The VBO ({Name}) already initialised");
            _shaders = shaders;

             _handle = GL.GenBuffer();

            Bind();
            AllocateMemory();
        }

        public void Load()
        {
            Bind();
            LoadToMemory();

            for (int i = 0; i < _shaders.Count; i++)
            {
                AssignAttributes(_shaders[i]);
            }
        }

        public void Reload()
        {
            Bind();
            LoadToMemory();
            NeedsUpdate = false;
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }

        public void Bind() => GL.BindBuffer(BufferTarget, _handle);

        private void AllocateMemory()
        {
            var expectedSize = Buffer.Length * Marshal.SizeOf<T>();
            GL.BufferData(BufferTarget, expectedSize, IntPtr.Zero, BufferHint);

            GL.GetBufferParameter(BufferTarget, BufferParameterName.BufferSize, out int size);
        }

        private void AssignAttributes(Shader shader)
        {
            var attributes = GetAttributes();
            int size = attributes.Select(x => x.Size).Sum();
            int cumulative = 0;

            for(int i = 0; i < attributes.Length; i++)
            {
                var attrib = attributes[i];

                int location = GL.GetAttribLocation(shader.ProgramId, attrib.Name);
                GL.EnableVertexAttribArray(location);
                GL.VertexAttribPointer(location, attrib.Size, attrib.Type, false, size * sizeof(float), cumulative * sizeof(float));

                if(attrib.Instanced)
                {
                    GL.VertexAttribDivisor(attrib.Size, 1);
                }

                cumulative += attrib.Size;
            }
        }

        private void LoadToMemory()
        {
            var expectedSize = Buffer.Length * Marshal.SizeOf<T>();
            GL.BufferSubData(BufferTarget, IntPtr.Zero, expectedSize, _buffer);
            GL.GetBufferParameter(BufferTarget, BufferParameterName.BufferSize, out int size);

            if (expectedSize != size)
            {
                throw new Exception($"Error occurred when loading to memory ({Name})");
            }
        }

        private AttributeNameAttribute[] GetAttributes()
        {
            var props = typeof(T).GetProperties();
            var dict = new Dictionary<AttributeNameAttribute, PropertyInfo>();

            for(int i = 0; i < props.Length; i++)
            {
                var attribute = (AttributeNameAttribute)Attribute.GetCustomAttribute(props[i], typeof(AttributeNameAttribute));
                if(attribute != null)
                {
                    dict.Add(attribute, props[i]);
                }
            }

            return dict.Keys.ToArray();
        }

        public void QueueReload()
        {
            NeedsUpdate = true;
        }
    }
}
