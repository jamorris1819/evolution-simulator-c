using Engine.Core.Extensions;
using Engine.Render.Attributes;
using OpenTK.Mathematics;

namespace Engine.Render.VAO.Instanced
{
    public struct Instance
    {
        [AttributeName("vIPosition", 2, OpenTK.Graphics.ES30.VertexAttribPointerType.Float, true)]
        public Vector2 Position { get; set; }

        [AttributeName("vIColour", 3, OpenTK.Graphics.ES30.VertexAttribPointerType.Float, true)]
        public Vector3 Colour { get; set; }

        public override int GetHashCode()
        {
            return this.GetHashCodeOnProperties();
        }
    }
}
