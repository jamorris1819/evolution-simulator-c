using Engine.Core.Events.Input.Mouse;
using Engine.Core.Managers;
using Engine.Terrain.Data;
using Evolution.Environment.Life.Plants;
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
        private readonly PlantManager _plantManager;
        private readonly FloraManager _floraManager;
        private readonly IEventBus _eventBus;
        private readonly Random _random = new Random();

        public EnvironmentBuilder(EntityManager entityManager, IEventBus eventBus)
        {
            _plantManager = new PlantManager(entityManager);
            _floraManager = new FloraManager();
            _floraManager.Initialise();
            _eventBus = eventBus;

            eventBus.Subscribe<MouseDownEvent>((e) =>
            {

                if (e.Button == OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Left)
                {
                    //_plantManager.Pop();
                }
            });
        }



        public void PopulateMap(TerrainUnit[] units)
        {
            for(int i = 0; i < units.Length; i++)
            {
                PopulateUnit(units[i]);
            }

            _plantManager.Refresh();
        }

        private void PopulateUnit(TerrainUnit unit)
        {
            if (!_floraManager.HasFlora(unit.Biome)) return;

            var growingSpots = GetGrowingSpots(unit, 0.85f).ToArray();

            for (int i = 0; i < growingSpots.Length; i++)
            {
                var plant = _floraManager.GetFlora(unit.Biome).GetRandomPlant();
                _plantManager.AddPlant(plant, growingSpots[i]);
            }
        }

        private IEnumerable<Vector2> GetGrowingSpots(TerrainUnit unit, float percentToPopulate)
        {
            var growingSpots = unit.GrowingPoints.OrderBy(x => _random.NextDouble());
            var count = growingSpots.Count();
            int skip = (int)(count * (1.0f - percentToPopulate));

            return growingSpots.Skip(skip);
        }
    }
}
