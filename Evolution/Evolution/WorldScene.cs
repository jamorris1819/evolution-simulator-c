using Engine;
using Engine.Core.Events.Input.Mouse;
using Engine.Physics;
using Engine.Render;
using Engine.Render.Core.VAO.Instanced;
using Engine.Render.Events;
using Evolution.Environment.Life.Creatures;
using Evolution.Environment.Life.Creatures.Mouth;
using Evolution.Genetics;
using Evolution.Genetics.Utilities;
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
        DNA lastDNA;

        CreatureBuilder cb;

        List<Mouth> mouths = new List<Mouth>();

        public WorldScene(Game game) : base(game)
        {
            _environment = new Environment.Environment(EntityManager, EventBus);
            _environment.Initialise();

            game.UIManager.Windows.Add(new TerrainWindow(_environment.TerrainManager, Game.EventBus));
            game.UIManager.Windows.Add(new TestWindow(Game.EventBus));

            cam = new MouseCamera(1920, 1080, EventBus, Game.ShaderManager);

            EventBus.Publish(new CameraChangeEvent() { Camera = cam });
            Random random = new Random();

            dna = DNACreator.CreateDNA();

            cb = new CreatureBuilder(EntityManager, SystemManager.GetSystem<PhysicsSystem>().World);

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

            var aDNA = DNACreator.CreateDNA();
            var bDNA = aDNA.Mutate().Mutate().Mutate().Mutate().Mutate().Mutate().Mutate().Mutate().Mutate().Mutate().Mutate();

            var topRow = new DNA[100];
            var leftColumn = new DNA[100];

            for (int x = 0; x < 100; x++)
            {
                topRow[x] = aDNA;
                leftColumn[x] = bDNA;
                aDNA = aDNA.Mutate();
                bDNA = bDNA.Mutate();

                createCreature(x, 0, topRow[x]);
                createCreature(x, 3, topRow[x].Cross(leftColumn[x]));
                createCreature(x, 6, leftColumn[x]);
            }

            /*var bodyDef = new BodyDef();

            var fixtureDef = new PolygonDef();
            fixtureDef.SetAsBox(10, 1);


            var body = new PhysicsBody(bodyDef, fixtureDef);
            var floor = new Entity("floor");
            floor.AddComponent(new PositionComponent(0, -10));
            floor.AddComponent(new PhysicsComponent(body));

            EntityManager.AddEntity(floor);*/


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
            /*var entity = new Entity("creature");


            var shape = CreatureBodyFactoryBuilder.Get(Environment.Life.Creatures.Body.Enums.BodyType.SinglePart).CreateBody(dna).First();


            var rc = new RenderComponent(shape);
            //rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.StandardOutline);
            rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            entity.AddComponent(rc);
            entity.AddComponent(new PositionComponent(x, y * 3) {  });


            bool debuggable = y % 2 == 0;

            var body = new CirclePhysicsBody(0.2f, 10)
            {
                BodyType = tainicom.Aether.Physics2D.Dynamics.BodyType.Dynamic,
                LinearDrag = 1f,
                AngularDrag = 2f
            };

            body.Debug = debuggable;
            entity.AddComponent(new PhysicsComponent(body));

            var factory = MouthFactoryBuilder.GetFactory(Environment.Life.Creatures.Mouth.Enums.MouthType.Pincer);
            var mouth = factory.CreateMouth(dna);

            mouth.SetParent(entity);
            EntityManager.AddEntities(mouth.GetEntities());

            mouths.Add(mouth);

            EntityManager.AddEntity(entity);*/
            cb.BuildCreature(dna, new Vector2(x, y));
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

            foreach (Mouth mouth in mouths)
            {
                mouth.Update((float)counter);
            }

            /*clawEntity.GetComponent<PositionComponent>().Angle = 
            clawEntity2.GetComponent<PositionComponent>().Angle = -triangleWave((float)counter * speed) * (float)(Math.PI * 0.25);*/
        }
    }
}
