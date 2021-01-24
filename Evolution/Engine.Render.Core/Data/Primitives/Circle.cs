using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Render.Core.Data.Primitives
{
    public static class Circle
    {
        public static VertexArray Generate(float radius, int steps)
        {
            List<Vector2> points = new List<Vector2>();
            float step = (float)(Math.PI * 2) / (float)steps;
            for(int i = 0; i < steps; i++)
            {
                Vector2 point = new Vector2((float)Math.Sin(step * i), (float)Math.Cos(step * i));
                point *= radius;
                points.Add(point);
            }

            return Polygon.Generate(points);
        }
    }
}
