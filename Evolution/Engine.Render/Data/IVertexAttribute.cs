using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Data
{
    interface IVertexAttribute
    {
        IVertexBufferObject[] GenerateBufferObjects();
    }
}
