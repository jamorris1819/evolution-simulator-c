using Engine.Grid;
using Engine.Render.Core.Data;
using Engine.Terrain.Data;
using Engine.Terrain.Noise;
using Redbus.Interfaces;
using System.Collections.Generic;

namespace Engine.Terrain.Generator
{
    public abstract class TerrainGenerator : ITerrainGenerator
    {
        protected int _previousSize;
        protected IEventBus _eventBus;

        public VertexArray TerrainShape { get; protected set; }

        public Layout Layout { get; protected set; }

        public TerrainProfile TerrainProfile { get; protected set; }

        protected NoiseCombinator Combinator { get; }

        public abstract IList<TerrainUnit> GetTerrain();

        public abstract void Generate();

        public void SetProfile(TerrainProfile profile)
        {
            TerrainProfile = profile;
        }

        public TerrainGenerator(IEventBus eventBus)
        {
            _eventBus = eventBus;
            SetProfile(TerrainProfile.Default);
            Combinator = new NoiseCombinator(TerrainProfile);
            //eventBus.Subscribe<TerrainNoiseAddedEvent>(x => HeightNoise.Add(x.Noise));

            // Add default layers
            //HeightNoise = new List<NoiseConfiguration>();
            //HeightNoise.Add(new NoiseConfiguration("Base"));
        }
    }
}
