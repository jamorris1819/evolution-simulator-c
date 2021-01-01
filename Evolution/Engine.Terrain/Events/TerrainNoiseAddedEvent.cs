using Engine.Terrain.Noise;
using Redbus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Events
{
    public class TerrainNoiseAddedEvent : EventBase
    {
        public NoiseConfiguration Noise { get; set; }
    }
}
