using Redbus.Interfaces;
using System.Collections.Generic;

namespace Engine.Core.Managers
{
    public class EntityManager
    {
        private IEventBus _eventBus;
        private IList<Entity> _entities;

        public IEnumerable<Entity> Entities { get => _entities; }

        public EntityManager(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _entities = new List<Entity>();
        }

        public void AddEntity(Entity entity)
            => _entities.Add(entity);

        public void AddEntities(IEnumerable<Entity> entities)
        {
            foreach(var entity in entities)
            {
                AddEntity(entity);
            }
        }
    }
}
