using Engine.Terrain.Noise;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Engine.Terrain.Data
{
    public class TerrainProfile
    {
        public string Name = string.Empty;
        public string Description = string.Empty;
        public float SeaLevel = 0.5f;
        public Vector2 Size { get; set; }
        public List<NoiseConfiguration> HeightNoise { get; set; } = new List<NoiseConfiguration>();

        public static TerrainProfile Default => new TerrainProfile()
        {
            Name = "untitled map",
            Size = new Vector2(30),
            SeaLevel = 0.5f,
            HeightNoise = new List<NoiseConfiguration>()
            {
                new NoiseConfiguration("base")
            }
        };
    }
}
