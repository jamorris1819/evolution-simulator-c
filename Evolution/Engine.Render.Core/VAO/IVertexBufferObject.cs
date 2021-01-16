using Engine.Render.Core.Shaders;
using System.Collections.Generic;

namespace Engine.Render.Core.VAO
{
    public interface IVertexBufferObject
    {
        string Name { get; }
        public bool NeedsUpdate { get; }
        void Initialise(IList<Shader> shaders);
        void Load();
        void Reload();
        void QueueReload();
        void Unload();
    }
}
