using Engine.Core;
using Engine.Core.Events.Input.Mouse;
using Engine.Core.Managers;
using Engine.Render.Core;
using Engine.Render.Core.Data.Primitives;
using Engine.Terrain.Data;
using Evolution.Environment.Food;
using Evolution.Environment.Life.Plants;
using Evolution.Environment.Life.Plants.Data;
using Evolution.Genetics;
using OpenTK.Mathematics;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Environment
{
    public class EnvironmentBuilder
    {
        private readonly EntityManager _entityManager;
        private readonly PlantManager _plantManager;
        private readonly FloraManager _floraManager;
        private readonly IEventBus _eventBus;
        private readonly Random _random = new Random();

        private List<List<Vector2>> _berryDistributions;

        public EnvironmentBuilder(EntityManager entityManager, IEventBus eventBus)
        {
            _entityManager = entityManager;
            _plantManager = new PlantManager(entityManager);
            _floraManager = new FloraManager();
            _floraManager.Initialise();
            _eventBus = eventBus;

            _berryDistributions = new List<List<Vector2>>();

            eventBus.Subscribe<MouseDownEvent>((e) =>
            {

                if (e.Button == OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Left)
                {
                    //_plantManager.Pop();
                }
            });

            for (int i = 0; i < 25; i++)
            {
                var points = UniformPoissonDiskSampler.SampleCircle(Vector2.Zero, 0.25f, 0.1f);
                _berryDistributions.Add(points);
            }
        }

        public WorldHex[] PopulateMap(TerrainUnit[] units)
        {
            _plantManager.Clear();
            var hexes = units.Select(PopulateUnit).ToArray();
            _plantManager.Refresh();
            return hexes;
        }

        private WorldHex PopulateUnit(TerrainUnit unit)
        {
            var worldHex = new WorldHex()
            {
                Terrain = unit,
                FoodSources = new List<FoodSource>(),
                Instances = new Dictionary<PlantDNA, List<PlantInstance>>()
            };

            if (!_floraManager.HasFlora(unit.Biome)) return worldHex;

            var growingSpots = GetGrowingSpots(unit, 0.85f).ToArray();


            for (int i = 0; i < growingSpots.Length; i++)
            {
                var plant = _floraManager.GetFlora(unit.Biome).GetRandomPlant();
                var instance = _plantManager.AddPlant(plant, growingSpots[i]);

                if (!worldHex.Instances.ContainsKey(plant)) worldHex.Instances.Add(plant, new List<PlantInstance>());
                worldHex.Instances[plant].Add(instance);
            }

            var foodBearing = worldHex.Instances.Keys.Where(x => x.Berries.HasValue).ToArray();

            var sources = foodBearing.SelectMany(x => CreateFoodSources(x, worldHex.Instances[x].Select(x => x.Position).ToArray()));
            worldHex.FoodSources = sources.ToList();

            return worldHex;
        }

        private FoodSource[] CreateFoodSources(PlantDNA dna, Vector2[] positions)
        {
            var sources = new FoodSource[positions.Length];
            var shape = Circle.Generate(0.04f * ((float)dna.Size / 30), 64);
            for(int i = 0; i < positions.Length; i++)
            {
                sources[i] = new FoodSource(positions[i], shape, GetPoints().Select(x => new FoodInstance(x, dna.Berries.Value)).ToList());
                sources[i].Initialise();
                _entityManager.AddEntity(sources[i].Entity);
            }

            return sources;
        }

        private IEnumerable<Vector2> GetGrowingSpots(TerrainUnit unit, float percentToPopulate)
        {
            var growingSpots = unit.GrowingPoints.OrderBy(x => _random.NextDouble());
            var count = growingSpots.Count();
            int skip = (int)(count * (1.0f - percentToPopulate));

            return growingSpots.Skip(skip);
        }

        private List<Vector2> GetPoints() => _berryDistributions[_random.Next(_berryDistributions.Count)].OrderBy(x => _random.NextDouble()).ToList();
    }
}
