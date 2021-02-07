using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data;
using Evolution.Environment.Life.Creatures.Mouth.ConstructionModels;
using Evolution.Genetics.Creature;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Environment.Life.Creatures.Mouth
{
    public class PincerMouth : Mouth
    {
        private List<PositionComponent> _positions;

        public PincerMouth()
        {
            _positions = new List<PositionComponent>();
        }

        public override void Build(in DNA dna)
        {
            CreateMouthEntity();
            CreatePincerEntities(dna);
        }

        public override void Update(float counter)
        {
            float speed = 5.5f;

            _positions[0].Angle = triangleWave((float)counter * speed) * (float)(Math.PI * 0.25);
            _positions[1].Angle = -triangleWave((float)counter * speed) * (float)(Math.PI * 0.25);
        }

        private void CreatePincerEntities(in DNA dna)
        {
            Random random = new Random();
            var pincer = new PincerModel(1, (float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            var va1 = pincer.GenerateShape(16);

            var colour = Phenotype<Vector3>.GetFromGenotypes(dna.ColourR, dna.ColourG, dna.ColourB);

            va1 = VertexHelper.SetColour(va1, colour.Data * 1.5f);

            /*var curve = _creatureBodyBuilder.CreateThoraxCurve(dna);
            var creatureHeight = curve.First().Y + (float)Math.Abs(curve.Last().Y);
            var mouthSize = creatureHeight / 2.0f;

            va1 = VertexHelper.Scale(va1, mouthSize);*/

            var border = VertexHelper.Scale(va1, 1.1f);
            border = VertexHelper.SetColour(border, new Vector3(0));

            va1 = VertexHelper.Combine(border, va1);



            va1 = VertexHelper.Rotate(va1, (float)Math.PI * 0.5f);

            var va2 = VertexHelper.Multiply(va1, new Vector2(-1, 1));

            CreatePincerEntity(va1);
            CreatePincerEntity(va2);
        }

        private void CreatePincerEntity(VertexArray va)
        {
            var entity = new Entity("claw2");
            entity.AddComponent(new PositionComponent(0, 0));
            entity.AddComponent(new RenderComponent(va));
            entity.GetComponent<RenderComponent>().Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            entity.Parent = MouthEntity;
            _positions.Add(entity.GetComponent<PositionComponent>());
            _entities.Add(entity);
        }

        private static float triangleWave(float x)
        {
            return (float)Math.Abs(Math.Asin(Math.Cos(x)) * 0.63661828367f);
        }
    }
}
