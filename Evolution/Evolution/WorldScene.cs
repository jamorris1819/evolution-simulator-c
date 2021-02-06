﻿using Box2DX.Dynamics;
using DotnetNoise;
using Engine;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Events.Input.Mouse;
using Engine.Physics;
using Engine.Physics.Core;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data;
using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.VAO.Instanced;
using Engine.Render.Events;
using Engine.Terrain;
using Engine.Terrain.Biomes;
using Engine.Terrain.Generator;
using Evolution.Environment;
using Evolution.Environment.Life.Creatures;
using Evolution.Environment.Life.Creatures.Mouth;
using Evolution.Environment.Life.Creatures.Mouth.Models;
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

        DNA dna;

        CreatureBodyBuilder creatureBuilder;

        Entity clawEntity;
        Entity clawEntity2;

        List<IMouth> mouths = new List<IMouth>();

        public WorldScene(Game game) : base(game)
        {
            _environment = new Environment.Environment(EntityManager, EventBus);
            _environment.Initialise();

            game.UIManager.Windows.Add(new TerrainWindow(_environment.TerrainManager, Game.EventBus));
            game.UIManager.Windows.Add(new TestWindow(Game.EventBus));

            creatureBuilder = new CreatureBodyBuilder();

            cam = new MouseCamera(1920, 1080, EventBus, Game.ShaderManager);

            EventBus.Publish(new CameraChangeEvent() { Camera = cam });
            Random random = new Random();


            var dnaA = DNAHelper.CreateDNA(new DNATemplate(new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()), random.Next(32, 64), 0));
            var dnaB = DNAHelper.CreateDNA(new DNATemplate(new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()), random.Next(32, 64), 50));

            dna = DNAHelper.CreateDNA(new DNATemplate(new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()), random.Next(32, 64), 500));

            /*bottomRow = new DNA[9];
            leftRow = new DNA[9];
            for (int i =0; i < 9; i++)
            {
                dnaA = dnaA.Copy();
                dnaB = dnaB.Copy();
                bottomRow[i] = dnaA;
                leftRow[i] = dnaB;

                createCreature(i, -1, bottomRow[i]);
                createCreature(-1, i, leftRow[i]);
            }*/

            for (int x = 0; x < 20; x++)
            {
                for(int y= 0; y < 20; y ++)
                {
                    createCreature(x, y, dna);
                    dna = DNAHelper.CreateDNA(new DNATemplate(new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()), random.Next(32, 64), random.Next(1000)));
                }
            }

            var bodyDef = new BodyDef();

            var fixtureDef = new PolygonDef();
            fixtureDef.SetAsBox(10, 1);


            var body = new PhysicsBody(bodyDef, fixtureDef);
            var floor = new Entity("floor");
            floor.AddComponent(new PositionComponent(0, -10));
            floor.AddComponent(new PhysicsComponent(body));

            EntityManager.AddEntity(floor);


            /*IEnumerable<Vector2> sinPoints(int count)
            {
                for(int i = 0; i < count + 1; i++)
                {
                    float delta = (i / (float)count) * (float)Math.PI;
                    yield return new Vector2(i, (float)Math.Sin(delta));
                }
            }

            IEnumerable<Vector2> bezierpoints(int count, Vector2 val)
            {
                BezierCurveQuadric a = new BezierCurveQuadric(new Vector2(0), new Vector2(1, 0), val);

                for (int i = 0; i < count + 1; i++)
                {
                    yield return a.CalculatePoint(i / (float)count);
                }
            }


            VertexArray createClaw()
            {
                float height = 0.4f;

                var width = 10;

                var topPoints = bezierpoints(width - 1, new Vector2(0.5f)).ToArray();
                var bottomPoints = bezierpoints(width - 1, new Vector2(0.8f, 0.3f)).Select(x => x - (new Vector2(0, 0.1f) * ((width - x.X) / (float)width))).ToArray();

                var vertices = new List<Vertex>();

                for (int i = 0; i < width - 1; i++)
                {
                    var t1v1 = new Vertex(topPoints[i]);
                    var t1v2 = new Vertex(bottomPoints[i]);
                    var t1v3 = new Vertex(topPoints[i + 1]);

                    vertices.Add(t1v1);
                    vertices.Add(t1v2);
                    vertices.Add(t1v3);


                    var t2v1 = new Vertex(topPoints[i + 1]);
                    var t2v2 = new Vertex(bottomPoints[i]);
                    var t2v3 = new Vertex(bottomPoints[i + 1]);

                    vertices.Add(t2v1);
                    vertices.Add(t2v2);
                    vertices.Add(t2v3);
                }

                var va = new VertexArray(vertices.ToArray(), Enumerable.Range(0, vertices.Count).Select(x => (ushort)x).ToArray());

                return va;
            }*/

            /*var pm = new PincerModel(1, 0.5f, 0.15f, 0.2f);

            clawEntity = new Entity("claw");
            clawEntity.AddComponent(new PositionComponent(-10, 0));
            clawEntity.AddComponent(new RenderComponent(pm.GenerateShape(16)));
            clawEntity.GetComponent<RenderComponent>().Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);

            EntityManager.AddEntity(clawEntity);

            clawEntity2 = new Entity("claw2");
            clawEntity2.AddComponent(new PositionComponent(-10, 0));
            clawEntity2.AddComponent(new RenderComponent(pm.GenerateShape(16)));
            clawEntity2.GetComponent<RenderComponent>().Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);

            EntityManager.AddEntity(clawEntity2);

            EventBus.Subscribe<TestEvent>(x =>
            {
                clawEntity.RemoveComponent<RenderComponent>();
                clawEntity.AddComponent(new RenderComponent(x.Model.GenerateShape(16)));
                clawEntity.GetComponent<RenderComponent>().Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);

                clawEntity2.RemoveComponent<RenderComponent>();
                var shape = VertexHelper.Multiply(x.Model.GenerateShape(16), new Vector2(1, -1));
                clawEntity2.AddComponent(new RenderComponent(shape));
                clawEntity2.GetComponent<RenderComponent>().Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            });
            */



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

            var shape = creatureBuilder.CreateBody(dna);
            var rc = new RenderComponent(shape);
            //rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.StandardOutline);
            rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            entity.AddComponent(rc);
            entity.AddComponent(new PositionComponent(x, y * 3) {  });

            var bodyDef = new BodyDef()
            {
                LinearDamping = 3f,
                AngularDamping = 3f,
                Position = new Box2DX.Common.Vec2(x * 4, y * 3 * 4),
                IsBullet = true
            };

            var fixtureDef = (new PhysicsBodyBuilder()).CreateBody(dna);
            bool debuggable = y % 2 == 0;

            var body = new PhysicsBody(bodyDef, fixtureDef);
            body.Debug = debuggable;
            entity.AddComponent(new PhysicsComponent(body));

            var mouth = new PincerMouth(EntityManager);
            mouth.Build(dna);
            mouth.SetParent(entity);

            mouths.Add(mouth);

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
            counter += 0.01666f;

            float triangleWave(float x)
            {
                return (float)Math.Abs(Math.Asin(Math.Cos(x)) * 0.63661828367f);
            }

            float speed = 2.5f;

            foreach (IMouth mouth in mouths)
            {
                mouth.Update((float)counter);
            }

            /*clawEntity.GetComponent<PositionComponent>().Angle = 
            clawEntity2.GetComponent<PositionComponent>().Angle = -triangleWave((float)counter * speed) * (float)(Math.PI * 0.25);*/
        }
    }
}
