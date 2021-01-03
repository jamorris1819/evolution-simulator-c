using DotnetNoise;
using Engine.Terrain.Data;
using OpenTK.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DotnetNoise.FastNoise;

namespace Engine.Terrain.Noise
{
    public class NoiseCombinator
    {
        public NoiseCombinatorSet HeightSet;
        public NoiseCombinatorSet RainfallSet;

        public NoiseCombinator(TerrainProfile profile)
        {
            HeightSet = new NoiseCombinatorSet(profile.HeightNoise.ToArray());
            RainfallSet = new NoiseCombinatorSet(new[] { profile.RainfallNoise });
        }

        public GeneratedTerrainProfile Generate(TerrainProfile profile, Vector2[] points)
        {
            HeightSet.Update(profile.HeightNoise.ToArray());
            RainfallSet.Update(new[] { profile.RainfallNoise });

            var generatedProfile = new GeneratedTerrainProfile();

            float[] heights = HeightSet.Generate(points);
            float[] rainfall = RainfallSet.Generate(points);
            float highestRainfall = rainfall.OrderBy(x => x).Last();
            float highestRainfallInv = 1.0f / highestRainfall;
            rainfall = rainfall.Select(x => x * highestRainfallInv).ToArray();
            

            generatedProfile.Heights = heights;
            generatedProfile.Rainfall = rainfall;

            return generatedProfile;
        }
    }
}
