using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Noise
{
    interface INoiseGenerator
    {
        float Generate(float x, float y);
    }
}
