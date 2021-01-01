using Engine.Grid;
using Engine.Render.Data;
using Engine.Render.Data.Primitives;
using Engine.Terrain.Noise;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Terrain.Generator
{
    public class HexTerrainGenerator : TerrainGenerator
    {
        private IList<TerrainUnit> _terrain;


        public HexTerrainGenerator(IEventBus eb) : base(eb)
        {
            _terrain = new List<TerrainUnit>();
            Layout = new Layout(Orientation.Layout_Pointy, new Vector2(4, 4));

            TerrainShape = new Polygon(Layout.GetHexPoints());
            TerrainShape.Generate();
        }

        public override void Generate(int size)
        {
            _previousSize = size;

            _terrain = new List<TerrainUnit>();
            var units = Map.GenerateHexagon(size);

            var positions = units.Select(x => Layout.HexToPixel(x)).ToArray();
            var heights = NoiseCombinator.Generate(HeightNoise, positions.ToArray()).ToArray();

            for (int i = 0; i < positions.Count(); i++)
            {
                _terrain.Add(new TerrainUnit()
                {
                    Position = positions[i],
                    Height = heights[i]
                });
            }
        }

        public override IList<TerrainUnit> GetTerrain()
        {
            return _terrain;
        }
    }
}
