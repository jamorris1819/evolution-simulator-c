using DotnetNoise;
using Engine;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Events.Input.Mouse;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.VAO.Instanced;
using Engine.Render.Events;
using Engine.Terrain;
using Engine.Terrain.Biomes;
using Engine.Terrain.Generator;
using Evolution.Environment;
using Evolution.Environment.Life.Plants;
using Evolution.Genetics;
using Evolution.Genetics.Creature;
using Evolution.Genetics.Creature.Helper;
using Evolution.UI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution
{
    public class WorldScene : GameScene
    {
        private readonly Environment.Environment _environment;

        Camera cam;
        double counter = 0;

        PooledInstanceVAO a;
        PooledInstance last;

        DNA[] bottomRow;
        DNA[] leftRow;

        public WorldScene(Game game) : base(game)
        {
            _environment = new Environment.Environment(EntityManager, EventBus);
            _environment.Initialise();

            game.UIManager.Windows.Add(new TerrainWindow(_environment.TerrainManager, Game.EventBus));

            cam = new MouseCamera(1920, 1080, EventBus, Game.ShaderManager);

            EventBus.Publish(new CameraChangeEvent() { Camera = cam });
            Random random = new Random();


            var dnaA = DNAHelper.CreateDNA(new DNATemplate(new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()), random.Next(32, 64), random.Next(200)));
            var dnaB = DNAHelper.CreateDNA(new DNATemplate(new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()), random.Next(32, 64), random.Next(200)));


            bottomRow = new DNA[9];
            leftRow = new DNA[9];
            for (int i =0; i < 9; i++)
            {
                bottomRow[i] = dnaA.Mutate();
                leftRow[i] = dnaB.Mutate();

                createCreature(i, -1, bottomRow[i]);
                createCreature(-1, i, leftRow[i]);
            }

            for(int x = 0; x < 9; x++)
            {
                for(int y= 0; y < 9; y ++)
                {
                    createCreature(x, y, DNAHelper.Cross(bottomRow[x], leftRow[y]));
                }
            }


            EventBus.Subscribe<MouseDownEvent>((e) =>
             {
                 var pos = cam.ScreenToWorld(e.Location);
                 var hex = _environment.TerrainManager.Layout.PixelToHex(pos).Round();
                 Console.WriteLine($"{pos} -> {hex}");
                 Console.WriteLine(e.Location);
             });
        }

        private void createCreature(int x, int y, DNA dna)
        {
            var entity = new Entity("creature");

            Random random = new Random();
            FastNoise noise = new FastNoise();
            noise.Octaves = 5;
            noise.UsedNoiseType = FastNoise.NoiseType.PerlinFractal;

            int steps = dna.BodySteps.GetPhenotype().Data;
            float stepSize = (float)Math.PI / steps;

            List<Vector2> points = new List<Vector2>();
            for (int i = 0; i < steps; i++)
            {
                var point = new Vector2((float)Math.Sin(stepSize * i), (float)Math.Cos(stepSize * i));
                point.Normalize();
                var dist = Math.Abs(noise.GetNoise(i + dna.BodyOffset.GetPhenotype().Data, 0));
                point *= Math.Max(dist, 0.1f);
                points.Add(point);
            }

            //var highResPoints = Enumerable.Range(0, 64).Select(x => BezierCurve.CalculatePoint(points, x / 63f)).ToArray();
            
           

            //points = highResPoints.ToList();


            Vector2[] flippedPoints = new Vector2[points.Count];
            points.CopyTo(flippedPoints, 0);
            flippedPoints = flippedPoints.Reverse().Select(x => x * new Vector2(-1, 1)).ToArray();
            points.AddRange(flippedPoints);
            points = points.Select(x => x * new Vector2(1, 1.7f)).ToList();

            //BezierCurve.

            var shape = Polygon.Generate(points);
            shape = VertexHelper.SetColour(shape, Phenotype<Vector3>.GetFromGenotypes(dna.ColourR, dna.ColourG, dna.ColourB).Data);
            var rc = new RenderComponent(shape);
            rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.StandardOutline);
            rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            entity.AddComponent(rc);
            entity.AddComponent(new PositionComponent(x, y));
            EntityManager.AddEntity(entity);
        }

        public override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            cam.Update(0.01666);
        }
    }
}
