using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data
{
    interface IBufferData<T>
    {
        T[] Data { get; }
        IBufferData<T> Add(IBufferData<T> data);
    }
}
