using Engine.Grid;
using Engine.Render.Data;
using Engine.Terrain.Noise;
using System;
using System.Collections.Generic;

namespace Engine.Terrain.Generator
{
    public interface ITerrainGenerator
    {
        VertexArray TerrainShape { get; }
        Layout Layout { get; }
        List<NoiseConfiguration> HeightNoise { get; }

        void Generate(int size);
        IList<TerrainUnit> GetTerrain();
    }
}
