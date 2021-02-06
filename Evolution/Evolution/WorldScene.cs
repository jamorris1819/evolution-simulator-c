using Box2DX.Dynamics;
using DotnetNoise;
using Engine;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Events.Input.Mouse;
using Engine.Physics;
using Engine.Physics.Core;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.VAO.Instanced;
using Engine.Render.Events;
using Engine.Terrain;
using Engine.Terrain.Biomes;
using Engine.Terrain.Generator;
using Evolution.Environment;
using Evolution.Environment.Life.Creatures;
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

        public WorldScene(Game game) : base(game)
        {
            _environment = new Environment.Environment(EntityManager, EventBus);
            _environment.Initialise();

            game.UIManager.Windows.Add(new TerrainWindow(_environment.TerrainManager, Game.EventBus));

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
                LinearDamping = 1f,
                AngularDamping = 1f,
                Position = new Box2DX.Common.Vec2(x * 10, y * 3 * 10),
                IsBullet = true
            };

            var fixtureDef = (new PhysicsBodyBuilder()).CreateBody(dna);
            bool debuggable = y % 2 == 0;

            var body = new PhysicsBody(bodyDef, fixtureDef);
            body.Debug = debuggable;
            entity.AddComponent(new PhysicsComponent(body));

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
