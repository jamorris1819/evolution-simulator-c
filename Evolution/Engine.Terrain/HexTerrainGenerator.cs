using Engine.Grid;
using Engine.Render.Data;
using Engine.Render.Data.Primitives;
using Engine.Terrain.Noise;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Terrain
{
    public class HexTerrainGenerator : ITerrainGenerator
    {
        private IList<TerrainUnit> _terrain;
        private HeightGenerator _heightGenerator;

        public Layout Layout { get; private set; }

        public VertexArray TerrainShape { get; private set; }

        public List<NoiseConfiguration> HeightNoise { get; private set; }

        public HexTerrainGenerator(NoiseConfiguration[] heightNoise)
        {
            _terrain = new List<TerrainUnit>();
            _heightGenerator = new HeightGenerator();
            Layout = new Layout(Orientation.Layout_Pointy, new Vector2(4, 4));

            HeightNoise = heightNoise.ToList();

            TerrainShape = new Polygon(Layout.GetHexPoints());
            TerrainShape.Generate();
        }

        public void Generate(int size)
        {
            _terrain = new List<TerrainUnit>();
            var units = Map.GenerateHexagon(size);

            var positions = units.Select(x => Layout.HexToPixel(x)).ToArray();
            var heights = NoiseCombinator.Generate(HeightNoise, positions.ToArray(), 8).ToArray();

            for (int i = 0; i < positions.Count(); i++)
            {
                _terrain.Add(new TerrainUnit()
                {
                    Position = positions[i],
                    Colour = new Vector3(heights[i])
                });
            }
        }

        public IList<TerrainUnit> GetTerrain()
        {
            return _terrain;
        }
    }
}
