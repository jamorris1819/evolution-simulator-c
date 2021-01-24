using Engine.Core.Managers;
using Engine.Terrain;
using Engine.Terrain.Generator;
using Evolution.Environment.Life.Plants;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Environment
{
    public class Environment
    {
        private EntityManager _entityManager;
        private TerrainManager _terrainManager;
        private EnvironmentBuilder _environmentBuilder;
        private IEventBus _eventBus;

        public TerrainManager TerrainManager => _terrainManager;

        public Environment(EntityManager entityManager, IEventBus eventBus)
        {
            _entityManager = entityManager;
            _eventBus = eventBus;

            _terrainManager = new TerrainManager(
                _entityManager,
                new HexTerrainGenerator(_eventBus),_eventBus);
            _environmentBuilder = new EnvironmentBuilder(entityManager, eventBus);
        }

        public void Initialise()
        {
            _terrainManager.Initialise();
            _environmentBuilder.PopulateMap(_terrainManager.Units.Values.ToArray());
        }
    }
}
