using Engine.Core;
using Engine.Grid;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Terrain
{
    public static class PointPicker
    {
        private static Dictionary<int, List<Vector2[]>> _points;

        private static Layout _layout;
        private static Hex _hex;
        private static Random _random;

        public static void Initialise()
        {
            _points = new Dictionary<int, List<Vector2[]>>();

            _layout = new Layout(Orientation.Layout_Pointy, new Vector2(4, 4));
            _hex = new Hex();

            for(int i = 0; i < 10; i++)
            {
                _points.Add(i + 1, GenerateMulti(5, i + 1));
            }

            _random = new Random();
        }

        public static Vector2[] GetPoints(int div)
        {
            if (div == 0) return new Vector2[0];
            var list = _points[div];
            var count = list.Count;

            return list[_random.Next(0, count)];
        }

        private static List<Vector2[]> GenerateMulti(int count, int div)
        {
            var list = new List<Vector2[]>();
            
            for(int i = 0; i < count; i++)
            {
                list.Add(GeneratePoints(div));
            }

            return list;
        }

        private static Vector2[] GeneratePoints(int div)
        {
            var items = UniformPoissonDiskSampler.SampleRectangle(new Vector2(-6, -6), new Vector2(6, 6), 4f / div);
            List<Vector2> points = new List<Vector2>();

            for(int i = 0; i < items.Count; i++)
            {
                var offset = (items[i] * 0.5f) + (items[i].Normalized() * 0.1f);
                var hex = _layout.PixelToHex(offset).Round();
                if (hex.Length() > 0) continue;
                points.Add(items[i]);
            }

            return points.ToArray();
        }
    }
}
