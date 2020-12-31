using DotnetNoise;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DotnetNoise.FastNoise;

namespace Engine.Terrain.Noise
{
    public static class NoiseCombinator
    {
        public static float[] Generate(NoiseConfiguration config, Vector2[] points, int size)
        {
            var noise = GetNoise(config);

            float[] results = new float[points.Length];

            for(int i = 0; i < results.Length; i++)
            {
                var point = points[i] / size;
                point += new Vector2(config.Offset.X, config.Offset.Y);

                results[i] = noise.GetNoise(point.X, point.Y, 0) * config.Scale;
                if (config.Invert) results[i] = 1 - results[i];
            }

            return results;
        }

        public static float[] Generate(List<NoiseConfiguration> config, Vector2[] points, int size)
        {
            float[] results = new float[points.Length];

            var masks = config.Where(x => x.Mask).ToArray();
            var standard = config.Where(x => !x.Mask).ToArray();

            // Add all standard noise configs
            for (int j = 0; j < standard.Length; j++)
            {
                var res = Generate(standard[j], points, size);
                for (int i = 0; i < points.Length; i++)
                {
                    results[i] += res[i];
                }
            }

            // Then multiply the masks
            for (int j = 0; j < masks.Length; j++)
            {
                var res = Generate(masks[j], points, size);
                for (int i = 0; i < points.Length; i++)
                {
                    results[i] *= res[i];
                }
            }

            return results;
        }

        private static FastNoise GetNoise(NoiseConfiguration config)
        {
            return new FastNoise(config.Seed)
            {
                UsedNoiseType = (NoiseType)config.Type,
                FractalTypeMethod = (FractalType)config.FractalType,
                Frequency = config.Frequency,
                Octaves = config.Octaves,
                Lacunarity = config.Lacunarity,
                Gain = config.Gain
            };
        }
    }
}
