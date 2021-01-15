using Engine.Render.Shaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Data
{
    public interface IVertexBufferObject
    {
        void Initialise(IList<Shader> shaders);
        void Load();
        void Unload();
    }
}
