﻿using Engine.Grid;
using Engine.Render.Core.Data;
using Engine.Terrain.Data;
using System.Collections.Generic;

namespace Engine.Terrain.Generator
{
    public interface ITerrainGenerator
    {
        VertexArray TerrainShape { get; }
        Layout Layout { get; }
        TerrainProfile TerrainProfile { get; }
        void Generate();
        void SetProfile(TerrainProfile profile);
        IList<TerrainUnit> GetTerrain();
    }
}
