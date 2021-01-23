using OpenTK.Mathematics;
using System.Linq;

namespace Engine.Render.Core.Data.Primitives
{
    public class Rectangle
    {
		public static VertexArray Generate(float w, float h)
        {
			var vertices = new[]
				{
					new Vertex(new Vector2(-w * 0.5f, h * 0.5f)),
					new Vertex(new Vector2(-w * 0.5f, -h * 0.5f)),
					new Vertex(new Vector2(w * 0.5f, -h * 0.5f)),
					new Vertex(new Vector2(w * 0.5f, h * 0.5f))
				};

			var indices = new[]
			{
				0, 1, 2, 2, 3, 0
			}.Select(x => (ushort)x).ToArray();

			return new VertexArray(vertices, indices);
		}
    }
}
