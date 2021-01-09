using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Painters
{
    public static class TerrainPainterFactory
    {
        public static ITerrainPainter GetPainter(TerrainRenderMode mode)
        {
            switch (mode)
            {
                case TerrainRenderMode.Default:
                    return new BiomeTerrainPainter();
                case TerrainRenderMode.HeightMap:
                    return new HeightMapTerrainPainter();
                case TerrainRenderMode.Rainfall:
                    return new RainfallTerrainPainter();
                case TerrainRenderMode.Temperature:
                    return new TemperatureTerrainPainter();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
        }
    }
}