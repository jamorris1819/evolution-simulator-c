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

        public NoiseCombinator(TerrainProfile profile)
        {
            HeightSet = new NoiseCombinatorSet(profile.HeightNoise.ToArray());
        }

        public GeneratedTerrainProfile Generate(TerrainProfile profile, Vector2[] points)
        {
            var generatedProfile = new GeneratedTerrainProfile();

            float[] heights = HeightSet.Generate(profile.HeightNoise.ToArray(), points);
            

            generatedProfile.Heights = heights;


            return generatedProfile;
        }
    }
}
