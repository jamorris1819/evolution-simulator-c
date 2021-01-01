using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static DotnetNoise.FastNoise;

namespace Engine.Terrain.Noise
{
    public class NoiseConfiguration
    {
        public string Name = "Default";
        public bool Visible = true;
        public bool Invert = false;
        public int Seed = 0;
        public float Scale = 1.0f;
        public int Type = 9;
        public float Frequency = 0.5f;
        public int FractalType = 0;
        public int Octaves = 4;
        public float Lacunarity = 0.1f;
        public float Gain = 0.1f;
        public Vector2 Offset;
        public bool Mask;
        public bool Round;

        public NoiseConfiguration() { }

        public NoiseConfiguration(string name)
        {
            Name = name;
        }
    }
}
