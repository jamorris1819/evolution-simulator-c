using OpenTK.Graphics.OpenGL4;

namespace Engine.Render.Core.Shaders
{
    struct UniformFieldInfo
    {
        public int Location;
        public string Name;
        public int Size;
        public ActiveUniformType Type;
    }
}
