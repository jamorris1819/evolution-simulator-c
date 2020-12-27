using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Data.Primitives
{
    public class Rectangle : VertexArray
    {
		public float Width { get; set; }
		public float Height { get; set; }

		public Rectangle(float w, float h) : base()
		{
			Width = w;
			Height = h;
			Generate();
		}

		public override void Generate()
        {
			Vertices = new[]
			{
				new Vertex(new Vector2(-Width * 0.5f, Height * 0.5f)),
				new Vertex(new Vector2(-Width * 0.5f, -Height * 0.5f)),
				new Vertex(new Vector2(Width * 0.5f, -Height * 0.5f)),
				new Vertex(new Vector2(Width * 0.5f, Height * 0.5f))
			};

			Indices = new ushort[]
			{
				0, 1, 2, 2, 3, 0
			};
		}
    }
}
