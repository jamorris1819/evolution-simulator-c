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
        public string Creator = string.Empty;
        public string Description = string.Empty;
        public float SeaLevel = 0.0f;
        public float TideLevel = 0.1f;
        public bool Island = true;
        public float DropOffPoint = 0.7f;
        public float DropOffSteepness = 1.0f;
        public int IslandSeed;
        public float IslandEdgeDistortion = 6.0f;
        public Vector2 Size { get; set; }
        public List<NoiseConfiguration> HeightNoise { get; set; } = new List<NoiseConfiguration>();
        public NoiseConfiguration RainfallNoise = new NoiseConfiguration("Rainfall") { Seed = 1 };

        public static TerrainProfile Default => new TerrainProfile()
        {
            Name = "untitled map",
            Size = new Vector2(30),
            SeaLevel = 0.0f,
            HeightNoise = new List<NoiseConfiguration>()
            {
                new NoiseConfiguration("base")
            }
        };
    }
}
