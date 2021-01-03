using Engine.Core;
using Engine.Core.Managers;
using Engine.Render;
using Engine.Render.Data;
using Engine.Terrain.Biomes;
using Engine.Terrain.Data;
using Engine.Terrain.Events;
using Engine.Terrain.Generator;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Terrain
{
    public class TerrainManager
    {
        public int RenderModeInt = 0;

        private EntityManager _entityManager;
        private ITerrainGenerator _generator;
        private IEventBus _eventBus;

        private RenderComponent _renderComponent;

        public TerrainRenderMode RenderMode
        {
            get => (TerrainRenderMode)RenderModeInt;
            set
            {
                RenderModeInt = (int)value;
            }
        }

        public TerrainProfile Profile => _generator.TerrainProfile;

        public TerrainManager(EntityManager em, ITerrainGenerator gen, IEventBus eb)
        {
            _entityManager = em;
            _generator = gen;
            _eventBus = eb;

            _eventBus.Subscribe<TerrainUpdateEvent>(OnTerrainUpdate);
        }

        public void Initialise()
        {
            Entity entity = new Entity("Terrain");
            _renderComponent = new RenderComponent(_generator.TerrainShape, GenerateTerrain(Profile));
            _renderComponent.Shader = Render.Shaders.Enums.ShaderType.StandardInstanced;
            entity.AddComponent(_renderComponent);
            _entityManager.AddEntity(entity);
        }

        private InstanceSettings GenerateTerrain(TerrainProfile profile)
        {
            _generator.SetProfile(profile);
            _generator.Generate();
            var units = _generator.GetTerrain();
            Random random = new Random();
            var settings = new InstanceSettings()
            {
                Instances = units.Select(x =>
                    new Instance()
                    {
                        Position = x.Position,
                        Colour = PaintTerrain(x) + new Vector3((float)random.NextDouble() * 0.025f)
                    }
                ).ToArray()
            };

            return settings;
        }

        private Vector3 PaintTerrain(TerrainUnit unit)
        {
            switch (RenderMode)
            {
                case TerrainRenderMode.Default:
                    return BiomeColour.Lookup(BiomePainter.Determine(unit.Height, Profile.SeaLevel, Profile.TideLevel,
                         unit.Rainfall, unit.Temperature));
                case TerrainRenderMode.HeightMap:
                    return new Vector3(unit.Height > Profile.SeaLevel ? unit.Height : 0);
                case TerrainRenderMode.Rainfall:
                    var rain = new Vector3(64, 86, 244) / 255.0f;
                    var height = new Vector3(unit.Height);
                    if (unit.Height < Profile.SeaLevel) return new Vector3(0);
                    return height +  new Vector3(unit.Rainfall * rain);
                case TerrainRenderMode.Temperature:
                    return new Vector3(unit.Temperature);
                default:
                    throw new ArgumentOutOfRangeException(nameof(unit));
            }
        }

        private void OnTerrainUpdate(TerrainUpdateEvent e)
        {
            var settings = GenerateTerrain(e.Profile);
            _renderComponent.UpdateInstanceSettings(settings);
        }
    }
}
