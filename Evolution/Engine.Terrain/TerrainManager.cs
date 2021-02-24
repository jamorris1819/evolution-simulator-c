using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Engine.Grid;
using Engine.Render;
using Engine.Render.Core.Shaders.Enums;
using Engine.Render.Core.VAO.Instanced;
using Engine.Terrain.Data;
using Engine.Terrain.Events;
using Engine.Terrain.Generator;
using Engine.Terrain.Painters;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Dictionary<Hex, TerrainUnit> Units { get; private set; }

        public Layout Layout => _generator.Layout;

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
            _renderComponent.Outlined = false;
            _renderComponent.Shaders.Add(ShaderType.StandardInstanced);
            //_renderComponent.Shaders.Add(ShaderType.StandardInstancedLoop);
            entity.AddComponent(_renderComponent);
            entity.AddComponent(new TransformComponent());
            _entityManager.AddEntity(entity);
        }

        private InstanceSettings GenerateTerrain(TerrainProfile profile)
        {
            _generator.SetProfile(profile);
            _generator.Generate();
            Units = new Dictionary<Hex, TerrainUnit>();
            var units = _generator.GetTerrain(); //.Select(x => new KeyValuePair<int, TerrainUnit>(x.GetHashCode(), x));
            for(int i = 0; i < units.Count; i++)
            {
                Units.Add(units[i].Hex, units[i]);
            }

            Random random = new Random();
            var settings = new InstanceSettings()
            {
                Instances = units.Select(x =>
                    new Instance()
                    {
                        Position = x.Position,
                        Colour = PaintTerrain(x) + new Vector3(Math.Max(x.Height * 0.25f, 0)) + new Vector3((float)random.NextDouble() * 0.025f)
                    }
                ).ToArray()
            };

            return settings;
        }

        private Vector3 PaintTerrain(TerrainUnit unit)
        {
            ITerrainPainter painter = TerrainPainterFactory.GetPainter(RenderMode);
            return painter.GetColour(unit);
        }

        private void OnTerrainUpdate(TerrainUpdateEvent e)
        {
            var settings = GenerateTerrain(e.Profile);
            _renderComponent.UpdateInstanceSettings(settings, false);
        }
    }
}
