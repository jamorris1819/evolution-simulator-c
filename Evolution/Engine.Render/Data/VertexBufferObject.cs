using Engine.Render.Attributes;
using Engine.Render.Shaders;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Engine.Render.Data
{
    internal class VertexBufferObject<T1> : IVertexBufferObject where T1: struct
    {
        private int _handle = -1;
        private T1[] _buffer;
        private IList<Shader> _shaders;

        public T1[] Buffer => _buffer;

        public BufferUsageHint BufferHint { get; set; }

        public BufferTarget BufferTarget { get; set; }

        public bool Initialised => _handle != -1;

        public bool NeedsUpdate { get; set; }

        public VertexBufferObject(T1[] items, BufferUsageHint hint, BufferTarget target)
        {
            _buffer = items;
            BufferHint = hint;
            BufferTarget = target;
        }

        public void Initialise(IList<Shader> shaders)
        {
            if (Initialised) throw new Exception("VBO already initialised");
            _shaders = shaders;

             _handle = GL.GenBuffer();

            Bind();
            AllocateMemory();
        }

        public void Load()
        {
            Bind();

            var expectedSize = Buffer.Length * Marshal.SizeOf<T1>();

            GL.BufferSubData(BufferTarget, IntPtr.Zero, expectedSize, _buffer);
            GL.GetBufferParameter(BufferTarget, BufferParameterName.BufferSize, out int size);

            if (expectedSize != size)
            {
                throw new Exception("Error occurred when loading to memory");
            }

            for (int i = 0; i < _shaders.Count; i++)
            {
                AssignAttributes(_shaders[i]);
            }
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }

        public void Bind() => GL.BindBuffer(BufferTarget, _handle);

        private void AllocateMemory()
        {
            var expectedSize = Buffer.Length * Marshal.SizeOf<T1>();
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
                cumulative += attrib.Size;
            }
        }

        private AttributeNameAttribute[] GetAttributes()
        {
            var props = typeof(T1).GetProperties();
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
    }
}
