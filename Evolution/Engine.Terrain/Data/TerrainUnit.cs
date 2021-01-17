using Engine.Grid;
using Engine.Terrain.Biomes;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Engine.Terrain.Data
{
    public struct TerrainUnit
    {
        public Hex Hex { get; set; }
        public Vector2 Position { get; set; }
        public float Height { get; set; }
        public float Rainfall { get; set; }
        public float Temperature { get; set; }
        public Biome Biome { get; set; }
        public Vector2[] GrowingPoints { get; set; }
    }
}
