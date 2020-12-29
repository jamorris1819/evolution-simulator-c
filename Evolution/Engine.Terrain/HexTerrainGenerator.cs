using Engine.Grid;
using Engine.Render.Data;
using Engine.Render.Data.Primitives;
using Engine.Terrain.Noise;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain
{
    public class HexTerrainGenerator : ITerrainGenerator
    {
        private IList<TerrainUnit> _terrain;
        private HeightGenerator _heightGenerator;

        public Layout Layout { get; private set; }

        public VertexArray TerrainShape { get; private set; }

        public HexTerrainGenerator()
        {
            _terrain = new List<TerrainUnit>();
            _heightGenerator = new HeightGenerator();
            Layout = new Layout(Orientation.Layout_Pointy, new OpenTK.Mathematics.Vector2(4, 4));

            TerrainShape = new Polygon(Layout.GetHexPoints());
            TerrainShape.Generate();
        }

        public void Generate(int size)
        {
            _terrain = new List<TerrainUnit>();
            var units = Map.GenerateHexagon(size);
            
            foreach(Hex hex in units)
            {
                var pos = Layout.HexToPixel(hex);
                _terrain.Add(new TerrainUnit()
                {
                    Position = pos,
                    Colour = new Vector3(_heightGenerator.Generate(pos.X, pos.Y))
                });
            }
        }

        public IList<TerrainUnit> GetTerrain()
        {
            return _terrain;
        }
    }
}
