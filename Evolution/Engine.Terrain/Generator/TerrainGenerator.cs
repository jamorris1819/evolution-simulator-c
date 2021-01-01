using Engine.Grid;
using Engine.Render.Data;
using Engine.Terrain.Data;
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
            SetProfile(new TerrainProfile()
            {
                HeightNoise = new List<NoiseConfiguration>()
                {
                    new NoiseConfiguration("base")
                },
                Size = new System.Numerics.Vector2(30, 30)
            });
            Combinator = new NoiseCombinator(TerrainProfile);
            //eventBus.Subscribe<TerrainNoiseAddedEvent>(x => HeightNoise.Add(x.Noise));

            // Add default layers
            //HeightNoise = new List<NoiseConfiguration>();
            //HeightNoise.Add(new NoiseConfiguration("Base"));
        }
    }
}
