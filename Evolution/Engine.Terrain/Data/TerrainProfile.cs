using Engine.Terrain.Noise;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Engine.Terrain.Data
{
    public class TerrainProfile
    {
        public float SeaLevel;
        public Vector2 Size { get; set; }
        public List<NoiseConfiguration> HeightNoise { get; set; }
    }
}
