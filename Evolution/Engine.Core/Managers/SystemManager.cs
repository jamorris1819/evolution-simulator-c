using Redbus.Interfaces;
using System.Collections.Generic;

namespace Engine.Core.Managers
{
    public class SystemManager
    {
        private IEventBus _eventBus;
        private IList<ISystem> _systems;
        private EntityManager _entityManager;

        public SystemManager(EntityManager entityManager, IEventBus eventBus)
        {
            _entityManager = entityManager;
            _systems = new List<ISystem>();
            _eventBus = eventBus;
        }

        public void Update()
        {
            foreach (ISystem system in _systems) {
                foreach (Entity entity in _entityManager.Entities)
                {
                    system.OnUpdate(entity, 0.0166f);
                }
            }
        }

        public void Render()
        {
            foreach (ISystem system in _systems)
            {
                foreach (Entity entity in _entityManager.Entities)
                {
                    system.OnRender(entity);
                }
            }
        }

        public void AddSystem(ISystem system)
        {
            _systems.Add(system);
        }
    }
}
