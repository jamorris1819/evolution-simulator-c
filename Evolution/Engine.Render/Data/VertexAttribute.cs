using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Engine.Render.Data
{
    public class BufferAttribute<T> : IBufferAttribute where T: struct
    {
        public T[] Data { get; set; }

        public ushort[] Indices { get; set; } = new ushort[0];

        public bool HasIndices => Indices?.Length > 0;

        public int Size => Marshal.SizeOf(typeof(T)) * Data.Length;

        public BufferUsageHint BufferHint { get; set; }

        public BufferTarget BufferTarget { get; set; }

        public BufferAttribute(T[] data) : this(data, BufferUsageHint.StaticDraw, BufferTarget.ArrayBuffer) { }

        public BufferAttribute(T[] data, BufferUsageHint hint, BufferTarget target)
        {
            Data = data;
            BufferHint = hint;
            BufferTarget = target;
        }

        public IVertexBufferObject[] GenerateBufferObjects()
        {
            List<IVertexBufferObject> vbos = new List<IVertexBufferObject>
            {
                new VertexBufferObject<T>(Data, BufferHint, BufferTarget)
            };

            if (HasIndices) vbos.Add(new VertexBufferObject<ushort>(Indices, BufferUsageHint.StaticDraw, BufferTarget.ElementArrayBuffer));

            return vbos.ToArray();
        }
    }
}
