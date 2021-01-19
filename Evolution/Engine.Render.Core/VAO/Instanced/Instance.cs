using Engine.Core.Extensions;
using Engine.Render.Core.Attributes;
using OpenTK.Mathematics;

namespace Engine.Render.Core.VAO.Instanced
{
    public struct Instance
    {
        [AttributeName("vIPosition", 2, OpenTK.Graphics.ES30.VertexAttribPointerType.Float, true, 1)]
        public Vector2 Position { get; set; }

        [AttributeName("vIColour", 3, OpenTK.Graphics.ES30.VertexAttribPointerType.Float, true, 1)]
        public Vector3 Colour { get; set; }

        public override int GetHashCode()
        {
            return this.GetHashCodeOnProperties();
        }
    }
}
