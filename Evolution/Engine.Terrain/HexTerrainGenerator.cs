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
        private Layout _layout;
        private Map _map;

        public VertexArray TerrainShape { get; private set; }

        public HexTerrainGenerator()
        {
            _terrain = new List<TerrainUnit>();
            _layout = new Layout(Orientation.Layout_Pointy, new OpenTK.Mathematics.Vector2(4, 4));
            _map = new Map();

            TerrainShape = new Polygon(_layout.GetHexPoints());
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
                    Position = _layout.HexToPixel(hex)
                });
            }
        }

        public IList<TerrainUnit> GetTerrain()
        {
            return _terrain;
        }
    }
}
