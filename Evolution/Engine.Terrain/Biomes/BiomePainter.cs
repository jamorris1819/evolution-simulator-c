using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Biomes
{
    public static class BiomePainter
    {
        public static Biome Determine(float height, float seaLevel)
        {
            return height > seaLevel ? Biome.Grass : Biome.Water;
        }
    }
}
