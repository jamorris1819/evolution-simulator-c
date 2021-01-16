using OpenTK.Mathematics;

namespace Engine.Render.Core.Data.Primitives
{
    public class Rectangle
    {
		public static VertexArray Generate(float w, float h)
        {
			return new VertexArray()
			{
				Vertices = new[]
				{
					new Vertex(new Vector2(-w * 0.5f, h * 0.5f)),
					new Vertex(new Vector2(-w * 0.5f, -h * 0.5f)),
					new Vertex(new Vector2(w * 0.5f, -h * 0.5f)),
					new Vertex(new Vector2(w * 0.5f, h * 0.5f))
				},
				Indices = new ushort[]
				{
					0, 1, 2, 2, 3, 0
				}
			};
		}
    }
}
