using Engine.Terrain.Data;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Painters
{
    public class TemperatureTerrainPainter : ITerrainPainter
    {
        public Vector3 GetColour(TerrainUnit unit) => new Vector3(unit.Temperature);
    }
}
