﻿using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Painters
{
    public class RainfallTerrainPainter : ITerrainPainter
    {
        public Vector3 GetColour(TerrainUnit unit) => new Vector3(unit.Rainfall);
    }
}
