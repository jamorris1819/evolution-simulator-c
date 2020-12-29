using DotnetNoise;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Noise
{
    public class HeightGenerator : INoiseGenerator
    {
        FastNoise noise;

        public HeightGenerator()
        {
            noise = new FastNoise();
        }

        public float Generate(float x, float y)
        {
            return noise.GetCubicFractal(x, y, 0);
        }
    }
}
