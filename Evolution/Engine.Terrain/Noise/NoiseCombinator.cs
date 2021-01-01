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
        private static float scaler = 1 / 32.0f;

        public static float[] Generate(NoiseConfiguration config, Vector2[] points)
        {
            var noise = GetNoise(config);

            float[] results = new float[points.Length];

            for(int i = 0; i < results.Length; i++)
            {
                var point = points[i] * scaler;
                point += new Vector2(config.Offset.X, config.Offset.Y);

                results[i] = (noise.GetNoise(point.X, point.Y, 0) + 0.5f) * config.Scale;
                if (config.Round) results[i] = results[i] > 0.5f ? 1f : 0f;
                if (config.Invert) results[i] = 1 - results[i];
            }

            return results;
        }

        public static float[] Generate(List<NoiseConfiguration> config, Vector2[] points)
        {
            float[] results = new float[points.Length];

            config = config.Where(x => x.Visible).ToList();

            var masks = config.Where(x => x.Mask).ToArray();
            var standard = config.Where(x => !x.Mask).ToArray();

            // Add all standard noise configs
            for (int j = 0; j < standard.Length; j++)
            {
                var res = Generate(standard[j], points);
                for (int i = 0; i < points.Length; i++)
                {
                    results[i] += res[i];
                }
            }

            // Then multiply the masks
            for (int j = 0; j < masks.Length; j++)
            {
                var res = Generate(masks[j], points);
                for (int i = 0; i < points.Length; i++)
                {
                    results[i] *= res[i];
                }
            }

            // make an island

            /*var radiusSqd = Math.Pow(100 * 6 * Math.Sqrt(3.0 / 2.0) * 0.8f, 2);
            for (int i = 0; i < points.Length; i++) { 
                var distSq = points[i].X * points[i].X + points[i].Y * points[i].Y;
                if (distSq > radiusSqd)
                {
                    var amount = distSq - radiusSqd;
                    var normalisedDist = amount / Math.Pow(100 * 6 * Math.Sqrt(3.0 / 2.0) * 0.2f, 2);
                    normalisedDist = 1.0f - normalisedDist;
                    results[i] *= (float)normalisedDist;
                }
            }*/

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
