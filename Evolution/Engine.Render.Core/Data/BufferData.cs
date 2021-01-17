using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data
{
    internal struct BufferData<T> : IBufferData<T>
    {
        public T[] Data { get; }

        public BufferData(T[] data)
        {
            Data = data;
        }

        public IBufferData<T> Add(IBufferData<T> data)
        {
            int currentSize = Data.Length;
            int addSize = data.Data.Length;

            T[] newData = new T[currentSize + addSize];

            for (int i = 0; i < currentSize; i++)
            {
                newData[i] = Data[i];
            }

            for (int i = 0; i < addSize; i++)
            {
                newData[currentSize + i] = Data[i];
            }

            return new BufferData<T>(newData);
        }
    }
}
