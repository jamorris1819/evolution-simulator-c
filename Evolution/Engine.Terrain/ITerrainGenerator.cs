using Engine.Grid;
using Engine.Render.Data;
using System;
using System.Collections.Generic;

namespace Engine.Terrain
{
    public interface ITerrainGenerator
    {
        VertexArray TerrainShape { get; }

        void Generate(int size);
        IList<TerrainUnit> GetTerrain(); 
    }
}
