using Redbus.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
                system.OnUpdate(0.0166f);
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

        public T GetSystem<T>() where T : ISystem
            => (T)_systems.FirstOrDefault(x => x is T);
    }
}
