using DotnetNoise;
using Engine.Terrain.Noise;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DotnetNoise.FastNoise;

namespace Engine.Terrain.Data
{
    public class NoiseCombinatorSet
    {
        public Dictionary<string, int> Hash { get; private set; }
        public Dictionary<string, float[]> Cache { get; private set; }

        private List<NoiseConfiguration> _configs;

        private static float scaler = 1 / 32.0f;

        public NoiseCombinatorSet(NoiseConfiguration[] configs)
        {
            Hash = new Dictionary<string, int>();
            Cache = new Dictionary<string, float[]>();
            _configs = configs.ToList();
        }

        public void Update(NoiseConfiguration[] configs)
        {
            var toAdd = new List<string>();
            var toRemove = new List<string>();

            for(int i = 0; i < configs.Length; i++)
            {
                if (Hash.ContainsValue(configs[i].GetHashCode())) continue;
                else toAdd.Add(configs[i].Name);
            }

            for (int i = 0; i < _configs.Count; i++)
            {
                if (Hash.ContainsValue(_configs[i].GetHashCode())) continue;
                else toRemove.Add(_configs[i].Name);
            }

            for(int i = 0; i < toRemove.Count; i++)
            {
                Hash.Remove(toRemove[i]);
                _configs.Remove(_configs.First(x => x.Name == toRemove[i]));
            }

            var addConfigs = configs.Where(x => toAdd.Contains(x.Name)).ToList();
            for(int i = 0; i < addConfigs.Count; i++)
            {
                Hash.Add(addConfigs[i].Name, 0);
                _configs.Add(addConfigs[i]);
            }
        }

        public float[] Generate(Vector2[] points)
        {
            float[] heights = new float[points.Length];

            for (int i = 0; i < _configs.Count; i++)
            {
                var noise = _configs[i];
                if (!noise.Visible) continue;

                bool needsCalculating = !Hash.ContainsKey(noise.Name) || Hash[noise.Name] != noise.GetHashCode();
                float[] calculatedHeights = new float[points.Length];

                if (needsCalculating)
                {
                    var generator = GetNoise(noise);

                    for (int j = 0; j < points.Length; j++)
                    {
                        calculatedHeights[j] = generator.GetNoise(
                            (points[j].X + noise.Offset.X) * scaler,
                            (points[j].Y + noise.Offset.Y) * scaler,
                            noise.Offset.Z);
                        calculatedHeights[j] *= noise.Scale;
                    }
                    Hash[noise.Name] = noise.GetHashCode();
                    Cache[noise.Name] = calculatedHeights;
                }
                else calculatedHeights = Cache[noise.Name];


                for (int j = 0; j < points.Length; j++)
                {
                    if (noise.Invert)
                    {
                        if (noise.Mask) heights[j] *= 1.0f - calculatedHeights[j];
                        else heights[j] += 1.0f - calculatedHeights[j];
                    }
                    else
                    {
                        if (noise.Mask) heights[j] *= calculatedHeights[j];
                        else heights[j] += calculatedHeights[j];
                    }
                }
            }

            return heights;
        }

        private static FastNoise GetNoise(NoiseConfiguration config)
           => new FastNoise(config.Seed)
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
