using Engine.Terrain.Biomes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Painters
{
    public class BiomeTerrainPainter : ITerrainPainter
    {
        public Vector3 GetColour(TerrainUnit unit)
        {
            return BiomeColour.Lookup(unit.Biome);
        }
    }
}
