using Engine.Render.Attributes;
using OpenTK.Mathematics;

namespace Engine.Render.Data
{
    public struct Instance
    {
        [AttributeName("vIPosition", 2, OpenTK.Graphics.ES30.VertexAttribPointerType.Float, true)]
        public Vector2 Position { get; set; }

        [AttributeName("vIColour", 3, OpenTK.Graphics.ES30.VertexAttribPointerType.Float, true)]
        public Vector3 Colour { get; set; }
    }
}
