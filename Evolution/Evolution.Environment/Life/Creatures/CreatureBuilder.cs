using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Engine.Physics;
using Engine.Physics.Core;
using Engine.Render;
using Engine.Render.Core.Data;
using Evolution.Environment.Life.Creatures.Body.Physics;
using Evolution.Environment.Life.Creatures.Body.Visual;
using Evolution.Environment.Life.Creatures.Mouth.Factory;
using Evolution.Genetics;
using Evolution.Genetics.Creature;
using Evolution.Genetics.Creature.Modules;
using Evolution.Genetics.Creature.Modules.Body;
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
            
            var length = GetSegmentLength(bodies[0]);

            var mouth = CreateMouth(dna, length * 0.5f);
            mouth.MouthEntity.GetComponent<PositionComponent>().Position = GetMouthPos(bodies[0]);
            _entityManager.AddEntities(mouth.GetEntities());

            if (bodies.Length != physicBodies.Length) throw new Exception();

            var entities = new List<Entity>();

            for(int i = bodies.Length - 1; i >= 0; i--)
            {
                var body = bodies[i];
                var physBody = physicBodies[i];
                var entity = CreateBodyPart(position + new Vector2(0, i * -0.05f), body, physBody);
                if (i > 0 && i % 2 == 0) entity.AddComponent(new LegsComponent());
                entities.Add(entity);
            }            

            mouth.SetParent(entities[bodies.Length - 1]);
        }

        private Entity BuildBaseEntity(Vector2 position)
        {
            var entity = new Entity("creature");
            entity.AddComponent(new PositionComponent(position));
            _entityManager.AddEntity(entity);
            return entity;
        }

        private Entity CreateBodyPart(Vector2 position, in VertexArray va, PhysicsBody physBod)
        {
            var entity = new Entity("body part");

            entity.AddComponent(new PositionComponent(position));
            var rc = new RenderComponent(va);
            rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            rc.VertexArrayObject.Outlined = true;
            entity.AddComponent(rc);
            entity.AddComponent(new PhysicsComponent(physBod));

            // TODO: decide if parent should be set
            //entity.Parent = parent;
            _entityManager.AddEntity(entity);

            return entity;
        }

        private IEnumerable<VertexArray> CreateBodyParts(in DNA dna)
        {
            var bodyType = ((BodyModule)dna.GetModule(ModuleType.Body)).Type;
            return CreatureBodyFactoryBuilder.Get(bodyType).CreateBody(dna);
        }

        private IEnumerable<PhysicsBody> CreatePhysicsBodyParts(in DNA dna, Vector2 pos)
        {
            var bodyType = ((BodyModule)dna.GetModule(ModuleType.Body)).Type;
            return CreaturePhysicsBodyFactoryBuilder.Get(bodyType, _world).CreateBody(dna, pos);
        }

        private Mouth.Mouth CreateMouth(in DNA dna, float scale) => MouthFactoryBuilder.GetFactory(Mouth.Enums.MouthType.Pincer).CreateMouth(dna, scale);

        private Vector2 GetMouthPos(in VertexArray va)
        {
            var points = va.Vertices.Where(x => x.Position.X == 0).OrderByDescending(x => x.Position.Y);

            return points.First().Position;
        }

        private float GetSegmentLength(VertexArray va)
        {
            var points = va.Vertices.OrderByDescending(x => x.Position.Y);

            return points.First().Position.Y + Math.Abs(points.Last().Position.Y);
        }
    }
}
