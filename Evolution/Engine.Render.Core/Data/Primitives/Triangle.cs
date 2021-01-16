using OpenTK.Mathematics;
using System.Linq;

namespace Engine.Render.Core.Data.Primitives
{
    public class Triangle
    {
        public static VertexArray Generate(int h, int w)
        {
            return new VertexArray()
            {
                Vertices = new[]
                {
                    new Vertex(new Vector2(-w * 0.5f, 0)),
                    new Vertex(new Vector2(w * 0.5f, 0)),
                    new Vertex(new Vector2(0, h))
                },
                Indices = new[]
                {
                    0, 1, 2
                }.Select(x => (ushort)x).ToArray()
            };
        }
    }
}
