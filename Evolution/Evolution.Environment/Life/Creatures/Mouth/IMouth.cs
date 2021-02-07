using Engine.Core;
using Engine.Core.Components;
using Engine.Render;
using Evolution.Genetics.Creature;
using System.Collections.Generic;

namespace Evolution.Environment.Life.Creatures.Mouth
{
    public abstract class Mouth
    { 
        protected List<Entity> _entities = new List<Entity>();

        public Entity MouthEntity { get; protected set; }

        public abstract void Build(in DNA dna, float scale);
        public abstract void Update(float delta);
        public void SetParent(Entity entity) => MouthEntity.Parent = entity;

        public IEnumerable<Entity> GetEntities() => _entities;

        protected void CreateMouthEntity()
        {
            MouthEntity = new Entity("Mouth Entity");
            MouthEntity.AddComponent(new PositionComponent());
            _entities.Add(MouthEntity);
        }
    }
}
