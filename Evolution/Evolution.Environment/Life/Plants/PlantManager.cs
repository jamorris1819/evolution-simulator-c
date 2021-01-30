using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Engine.Render;
using Engine.Render.Core.Data.Zoom;
using Engine.Render.Core.VAO.Instanced;
using Evolution.Environment.Life.Plants.Data;
using Evolution.Genetics;
using Evolution.Life;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Environment.Life.Plants
{
    /// <summary>
    /// A class responsible for the placement of plants into the environment.
    /// </summary>
    public class PlantManager
    {
        private EntityManager _entityManager;

        private Dictionary<PlantDNA, Entity> _entities;
        private Dictionary<PlantDNA, List<PlantInstance>> _instances;
        private Dictionary<PlantDNA, Plant> _plants;

        public PlantManager(EntityManager entityManager)
        {
            _entityManager = entityManager;
            _entities = new Dictionary<PlantDNA, Entity>();
            _instances = new Dictionary<PlantDNA, List<PlantInstance>>();
            _plants = new Dictionary<PlantDNA, Plant>();
        }

        /// <summary>
        /// Creates an instance of the given plant at the specified location.
        /// </summary>
        public PlantInstance AddPlant(PlantDNA dna, Vector2 pos)
        {
            if (!_entities.ContainsKey(dna))
            {
                InitialiseSpecies(dna);
            }

            var newInstance = new PlantInstance(Guid.NewGuid(), pos, dna.Colour);
            _instances[dna].Add(newInstance);

            return newInstance;
        }

        /// <summary>
        /// Refreshes the renderer of the specified plant.
        /// </summary>
        public void Refresh(PlantDNA dna)
        {
            Random random = new Random();
            var newInstanceSettings = new InstanceSettings()
            {
                Instances = _instances[dna].Select(x => new Instance()
                {
                    Colour = x.Colour + new Vector3((float)random.NextDouble() * 0.05f),
                    Position = x.Position
                }).ToArray()
            };

            _entities[dna].GetComponent<RenderComponent>().UpdateInstanceSettings(newInstanceSettings, true);
        }

        public void Clear()
        {
            _entities = new Dictionary<PlantDNA, Entity>();
            _instances = new Dictionary<PlantDNA, List<PlantInstance>>();
            _plants = new Dictionary<PlantDNA, Plant>();
            Refresh();
        }

        public void Refresh()
        {
            var keys = _plants.Keys.ToArray();
            for(int i = 0; i < keys.Length; i++)
            {
                Refresh(keys[i]);
            }
        }

        public void Pop()
        {
            _instances.Values.First().RemoveAt(0);

            Refresh(_instances.Keys.First());
        }

        /// <summary>
        /// Creates the required entity + render objects for the given DNA
        /// </summary>
        private void InitialiseSpecies(PlantDNA dna)
        {
            var plant = new Plant(dna);
            _plants.Add(dna, plant);

            var instanceSettings = new InstanceSettings()
            {
                Instances = new[]
                {
                    new Instance()
                    {
                        Colour = new Vector3(0),
                        Position = new Vector2()
                    }
                }
            };

            _instances[dna] = new List<PlantInstance>();

            var entity = new Entity("plant");
            entity.AddComponent(new PositionComponent());
            var shape = plant.GenerateShape();
            var renderComponent = new RenderComponent(shape, instanceSettings);
            renderComponent.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Outline);
            renderComponent.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.InstancedRotated);

            if (dna.MinHeight.HasValue && dna.MaxHeight.HasValue)
            {
                var profile = new ZoomProfile(dna.MinHeight.Value, dna.MaxHeight.Value, true,
                    new BezierZoomCurve(new Vector2(1, 0), new Vector2(0, 1)));
                renderComponent.ZoomProfile = profile;
            }

            entity.AddComponent(renderComponent);

            _entities[dna] = entity;
            _entityManager.AddEntity(_entities[dna]);
        }
    }
}
