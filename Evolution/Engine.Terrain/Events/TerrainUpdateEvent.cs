using Engine.Terrain.Data;
using Redbus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Events
{
    public class TerrainUpdateEvent : EventBase
    {
        public TerrainProfile Profile { get; set; }
    }
}
