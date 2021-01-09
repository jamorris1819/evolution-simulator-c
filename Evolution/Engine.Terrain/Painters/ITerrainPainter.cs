using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Painters
{
    public interface ITerrainPainter
    {
        Vector3 GetColour(TerrainUnit unit);
    }
}
