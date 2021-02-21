using Engine.Core;
using Engine.Render.Core;
using Engine.Render.Core.Data.Primitives;
using Evolution.Environment.Life.Creatures.Body.Visual;
using Evolution.Environment.Life.Creatures.Legs;
using Evolution.Genetics;
using Evolution.Genetics.Creature.Modules;
using Evolution.Genetics.Creature.Readers;
using Evolution.Genetics.Modules.Limbs;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolution.Environment.Life.Creatures.Limbs.Factory
{
    public class WalkingLimbFactory : LimbFactory
    {
        public override LimbType Type => LimbType.WalkingLeg;

        public override Limb CreateLimb(Entity parent, in DNA dna, bool leftSide)
        {
            var module = (WalkingLimbModule)dna.GetModule(ModuleType.WalkingLimbs); // todo: sort this

            var body = NoiseBodyGenerator.CreateBody(dna);

            var orderedY = body.Vertices.OrderBy(x => x.Position.Y);
            var height = Math.Abs(orderedY.First().Position.Y) + orderedY.Last().Position.Y;

            var legLength = DNAReader.ReadValueFloat(module.Length, DNAReader.LegLength) * height;
            var legDirection = DNAReader.ReadValueFloat(module.LegDirection, DNAReader.LegDirection) * (leftSide ? 1f : -1f);
            var legThickness = DNAReader.ReadValueFloat(module.LegThickness, DNAReader.LegThickness) * height * 0.7f * legLength;

            var baseOffset = leftSide
                ? body.Vertices.OrderBy(x => x.Position.X).First().Position
                : body.Vertices.OrderBy(x => x.Position.X).Last().Position;

            var colour = body.Vertices.First().Colour;
            baseOffset *= 0.75f;

            var legShape = Rectangle.Generate(legLength * 0.5f, legThickness);
            legShape = VertexHelper.Translate(legShape, new Vector2(legLength * 0.25f, 0));

            var ball = Circle.Generate(legThickness * 0.5f, 16);

            legShape = VertexHelper.Combine(ball, legShape);
            legShape = VertexHelper.SetColour(legShape, colour * 0.7f);


            var legModel = new LegModel(legShape, legShape, legLength, legDirection, baseOffset, legThickness);

            return new WalkingLimb(parent, EntityManager, legModel);
        }
    }
}
