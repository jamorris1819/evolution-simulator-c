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
                case Biome.Grass:
                    return new Vector3(0, 1, 0);
                case Biome.Water:
                    return new Vector3(0, 0, 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(biome));
            }
        }
    }
}
