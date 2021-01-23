﻿using Engine.Render.Core.Data;
using Engine.Render.Core.Data.Primitives;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Life
{
    public class Leaf
    {
        private BezierCurveCubic _curve;

        public Leaf(Vector2 a, Vector2 b, Vector2 p1) : this(a, b, p1, p1) { }

        public Leaf(Vector2 a, Vector2 b, Vector2 p1, Vector2 p2)
        {
            _curve = new BezierCurveCubic(a, b, p1, p2);
        }

        public VertexArray Generate(int resolution)
        {
            float step = 1.0f / resolution;

            Vector2[] points = new Vector2[resolution + 1];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = _curve.CalculatePoint(i * step);
            }

            Vector2[] flippedPoints = new Vector2[resolution + 1];
            points.CopyTo(flippedPoints, 0);
            flippedPoints = flippedPoints.Reverse().Select(x => x * new Vector2(-1, 1)).ToArray();

            var allPoints = new List<Vector2>();
            allPoints.AddRange(points);
            allPoints.AddRange(flippedPoints);

            return Polygon.Generate(allPoints.ToList());
        }
    }
}
