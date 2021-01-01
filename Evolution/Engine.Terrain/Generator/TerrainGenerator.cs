using Engine.Grid;
using Engine.Render.Data;
using Engine.Terrain.Events;
using Engine.Terrain.Noise;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Terrain.Generator
{
    public abstract class TerrainGenerator : ITerrainGenerator
    {
        protected int _previousSize;

        public VertexArray TerrainShape { get; protected set; }

        public Layout Layout { get; protected set; }

        public List<NoiseConfiguration> HeightNoise { get; protected set; }

        public abstract void Generate(int size);

        public abstract IList<TerrainUnit> GetTerrain();

        public TerrainGenerator(IEventBus eventBus)
        {
            eventBus.Subscribe<TerrainNoiseAddedEvent>(x => HeightNoise.Add(x.Noise));

            // Add default layers
            HeightNoise = new List<NoiseConfiguration>();
            HeightNoise.Add(new NoiseConfiguration("Base"));
        }
    }
}
