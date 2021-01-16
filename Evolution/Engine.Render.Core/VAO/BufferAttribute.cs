using OpenTK.Graphics.ES30;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Engine.Render.Core.VAO
{
    /// <summary>
    /// Describes some data that we want to send to the graphics card.
    /// </summary>
    public class BufferAttribute<T> : IBufferAttribute where T: struct
    {
        public string Name { get; set; }

        public T[] Data { get; set; }

        public ushort[] Indices { get; set; } = new ushort[0];

        public bool HasIndices => Indices?.Length > 0;

        public int Size => Marshal.SizeOf(typeof(T)) * Data.Length;

        public BufferUsageHint BufferHint { get; set; }

        public BufferTarget BufferTarget { get; set; }

        public BufferAttribute(string name, T[] data) : this(name, data, BufferUsageHint.StaticDraw, BufferTarget.ArrayBuffer) { }

        public BufferAttribute(string name, T[] data, BufferUsageHint hint, BufferTarget target)
        {
            Name = name;
            Data = data;
            BufferHint = hint;
            BufferTarget = target;
        }

        /// <summary>
        /// Create VertexBufferObjects configured for this data
        /// </summary>
        /// <returns></returns>
        public IVertexBufferObject[] GenerateBufferObjects()
        {
            List<IVertexBufferObject> vbos = new List<IVertexBufferObject>
            {
                new VertexBufferObject<T>(Name, Data, BufferHint, BufferTarget)
            };

            if (HasIndices) vbos.Add(new VertexBufferObject<ushort>($"{Name} (index)", Indices, BufferUsageHint.StaticDraw, BufferTarget.ElementArrayBuffer));

            return vbos.ToArray();
        }
    }
}
