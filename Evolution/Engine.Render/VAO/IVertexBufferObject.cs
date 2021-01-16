using Engine.Render.Shaders;
using System.Collections.Generic;

namespace Engine.Render.VAO
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
