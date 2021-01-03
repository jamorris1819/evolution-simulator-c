using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Biomes
{
    public static class BiomePainter
    {
        public static Biome Determine(float height, float seaLevel, float tideLevel, float rainfall, float temperature)
        {
            float iceRange = 0.1f;
            // Deal with water

            if (height <= seaLevel - tideLevel * 2) return Biome.DeepWater;
            if (height <= seaLevel) return Biome.Water;


            // Deal with terrain
            //if (temperature <= iceRange) return Biome.Tundra;
            if (height <= seaLevel + tideLevel) return Biome.Sand;

            // Tropical
            if(temperature + height * 0.3f > 0.8f)
            {
                if (rainfall > 0f) return Biome.RainForest;
                if (rainfall > -0.4f) return Biome.Savanna;
                return Biome.HotDesert;
            }


            return Biome.TemperateGrassland;
        }
    }
}
