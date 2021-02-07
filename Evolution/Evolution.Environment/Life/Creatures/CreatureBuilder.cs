using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Engine.Physics;
using Engine.Physics.Core;
using Engine.Render;
using Engine.Render.Core.Data;
using Evolution.Environment.Life.Creatures.Body.Physics;
using Evolution.Environment.Life.Creatures.Body.Visual;
using Evolution.Genetics.Creature;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using tainicom.Aether.Physics2D.Dynamics;

namespace Evolution.Environment.Life.Creatures
{
    public class CreatureBuilder
    {
        private readonly EntityManager _entityManager;
        private readonly World _world;

        public CreatureBuilder(EntityManager entityManager, World world)
        {
            _entityManager = entityManager;
            _world = world;
        }

        public void BuildCreature(in DNA dna, Vector2 position)
        {
            var baseEntity = BuildBaseEntity(position);

            var bodies = CreateBodyParts(dna).ToArray();
            var physicBodies = CreatePhysicsBodyParts(dna, position).ToArray();

            if (bodies.Length != physicBodies.Length) throw new Exception();

            for(int i = 0; i < bodies.Length; i++)
            {
                var body = bodies[i];
                var physBody = physicBodies[i];
                CreateBodyPart(baseEntity, body, physBody);
            }
        }

        private Entity BuildBaseEntity(Vector2 position)
        {
            var entity = new Entity("creature");
            entity.AddComponent(new PositionComponent(position));
            _entityManager.AddEntity(entity);
            return entity;
        }

        private Entity CreateBodyPart(Entity parent, in VertexArray va, PhysicsBody physBod)
        {
            var entity = new Entity("body part");

            entity.AddComponent(new PositionComponent(physBod.Position));
            var rc = new RenderComponent(va);
            rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            entity.AddComponent(rc);
            physBod.Debug = true;
            entity.AddComponent(new PhysicsComponent(physBod));

            //entity.Parent = parent;
            _entityManager.AddEntity(entity);

            return entity;
        }

        private IEnumerable<VertexArray> CreateBodyParts(in DNA dna) => CreatureBodyFactoryBuilder.Get(Body.Enums.BodyType.SinglePart).CreateBody(dna);

        private IEnumerable<PhysicsBody> CreatePhysicsBodyParts(in DNA dna, Vector2 pos)
            => CreaturePhysicsBodyFactoryBuilder.Get(Body.Enums.BodyType.SinglePart, _world).CreateBody(dna, pos);
    }
}
