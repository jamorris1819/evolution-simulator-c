using Engine.Grid;
using Engine.Render.Data;
using Engine.Render.Data.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain
{
    public class HexTerrainGenerator : ITerrainGenerator
    {
        private IList<TerrainUnit> _terrain;

        public Layout Layout { get; private set; }

        public VertexArray TerrainShape { get; private set; }

        public HexTerrainGenerator()
        {
            _terrain = new List<TerrainUnit>();
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
                _terrain.Add(new TerrainUnit()
                {
                    Position = Layout.HexToPixel(hex)
                });
            }
        }

        public IList<TerrainUnit> GetTerrain()
        {
            return _terrain;
        }
    }
}
