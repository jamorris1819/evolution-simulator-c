using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Data
{
    interface IBufferAttribute
    {
        IVertexBufferObject[] GenerateBufferObjects();
    }
}
