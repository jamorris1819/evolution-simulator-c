using Engine.Core.Managers;
using Engine.Grid;
using Engine.Terrain;
using Engine.Terrain.Events;
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
        private Dictionary<Hex, WorldHex> _hexes;

        public TerrainManager TerrainManager => _terrainManager;

        public Environment(EntityManager entityManager, IEventBus eventBus)
        {
            _entityManager = entityManager;
            _eventBus = eventBus;

            _terrainManager = new TerrainManager(
                _entityManager,
                new HexTerrainGenerator(_eventBus),_eventBus);
            _environmentBuilder = new EnvironmentBuilder(entityManager, eventBus);
            _hexes = new Dictionary<Hex, WorldHex>();
        }

        public void Initialise()
        {
            _terrainManager.Initialise();
            var hexes =_environmentBuilder.PopulateMap(_terrainManager.Units.Values.ToArray());

            for(int i = 0; i < hexes.Length; i++)
            {
                _hexes.Add(hexes[i].Terrain.Hex, hexes[i]);
            }
        }
    }
}
