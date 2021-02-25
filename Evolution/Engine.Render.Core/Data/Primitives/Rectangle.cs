using OpenTK.Mathematics;
using System.Linq;

namespace Engine.Render.Core.Data.Primitives
{
    public class Rectangle
    {
		public static VertexArray Generate(float w, float h, float z1 = 0, float z2 = 0)
        {
			var vertices = new[]
				{
					new Vertex(new Vector3(-w * 0.5f, h * 0.5f, z1), new Vector3(), new Vector2(-1, 1)),
					new Vertex(new Vector3(-w * 0.5f, -h * 0.5f, z1), new Vector3(), new Vector2(-1, -1)),
					new Vertex(new Vector3(w * 0.5f, -h * 0.5f, z2), new Vector3(), new Vector2(1, -1)),
					new Vertex(new Vector3(w * 0.5f, h * 0.5f, z2), new Vector3(), new Vector2(1, 1))
				};

			var indices = new[]
			{
				0, 1, 2, 2, 3, 0
			}.Select(x => (ushort)x).ToArray();

			return new VertexArray(vertices, indices);
		}
    }
}
