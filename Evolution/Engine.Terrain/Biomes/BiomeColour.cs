using OpenTK.Mathematics;
using System;

namespace Engine.Terrain.Biomes
{
    public class BiomeColour
    {
        public static Vector3 Lookup(Biome biome)
        {
            switch (biome)
            {
                case Biome.TemperateGrassland:
                    return Colour(124, 141, 76);
                case Biome.Water:
                    return Colour(119, 166, 182);
                case Biome.Sand:
                    return Colour(248, 220, 172);
                case Biome.DeepWater:
                    return Colour(3, 94, 123);
                case Biome.IceSheet:
                    return Colour(184, 242, 230);
                case Biome.Tundra:
                    return Colour(235, 244, 243);
                case Biome.RainForest:
                    return Colour(5, 71, 42);
                case Biome.Savanna:
                    return Colour(209, 163, 110);
                case Biome.HotDesert:
                    return Colour(237, 142, 74);
                default:
                    throw new ArgumentOutOfRangeException(nameof(biome));
            }
        }

        private static Vector3 Colour(int r, int g, int b)
            => new Vector3(r / 255.0f, g / 255.0f, b / 255.0f);
    }
}
