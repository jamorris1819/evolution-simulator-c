using DotnetNoise;
using Engine;
using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Events.Input.Mouse;
using Engine.Grid;
using Engine.Render;
using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.VAO.Instanced;
using Engine.Render.Events;
using Engine.Terrain;
using Engine.Terrain.Biomes;
using Engine.Terrain.Data;
using Engine.Terrain.Generator;
using Evolution.Life;
using Evolution.Life.DNA;
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
        private TerrainManager _terrainManager;

        Camera cam;
        double counter = 0;

        PooledInstanceVAO a;
        PooledInstance last;

        public WorldScene(Game game) : base(game)
        {
            _terrainManager = new TerrainManager(EntityManager, new HexTerrainGenerator(Game.EventBus), Game.EventBus);
            _terrainManager.Initialise();

            game.UIManager.Windows.Add(new TerrainWindow(_terrainManager, Game.EventBus));

            cam = new MouseCamera(1920, 1080, EventBus, Game.ShaderManager);

            EventBus.Publish(new CameraChangeEvent() { Camera = cam });

            /*a = new PooledInstanceVAO(Triangle.Generate(2, 1), 100);
            a.Initialise(Game.ShaderManager.All);
            a.Load();*/


            var points = _terrainManager.Units.Values.Where(x => x.Biome == Biome.TemperateGrassland)
                                           .Select(x => x.Position).ToArray();

           

            /*var items = UniformPoissonDiskSampler.SampleRectangle(new Vector2(-10, -10), new Vector2(10, 10), 1f);

            List<Vector2> growingPoints = new List<Vector2>();

            for(int i = 0; i < items.Count; i++)
            {
                var hex = _terrainManager.Layout.PixelToHex(items[i]).Round();
                if (!_terrainManager.Units.TryGetValue(hex, out TerrainUnit unit))
                {
                    if(hex.Length() < 10)
                        Console.WriteLine(hex.Length());
                    continue;
                }

                growingPoints.Add(items[i]);
            }*/

            var fertilePoints = _terrainManager.Units.Values.SelectMany(x => x.GrowingPoints).ToList();

            //Console.WriteLine(_terrainManager.Units.Values.Count(x => x.Biome == Biome.TemperateGrassland));

            /*var keep = items.Where(x =>
            {
                
                var hash = hex.GetHashCode();
                if (!_terrainManager.Units.ContainsKey(hash)) return false;
                return _terrainManager.Units[hash].Biome == Biome.TemperateGrassland;
            }).ToList();*/

            var leafData = new LeafData(new Vector2(0), new Vector2(0.5f));
            var dna = new PlantDNA(4, leafData);
            var plant = new Plant(dna);

            var plantShape = plant.GenerateShape();

            var ent = new Entity("plant");
            ent.AddComponent(new RenderComponent(plantShape));
            ent.AddComponent(new PositionComponent());
            EntityManager.AddEntity(ent);
            //var plant = Plant.Generate(fertilePoints);

                //plant.AddComponent(new PositionComponent(aridUnits[i] * 2));
                //EntityManager.AddEntity(plant);

             EventBus.Subscribe<MouseDownEvent>((e) =>
             {
                 var pos = cam.ScreenToWorld(e.Location);
                 var hex = _terrainManager.Layout.PixelToHex(pos).Round();
                 Console.WriteLine($"{pos} -> {hex}");
                 Console.WriteLine(e.Location);

                /* if (e.Button == OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Left)
                 {
                     last = a.CreateInstance(new Instance()
                     {
                         Colour = new Vector3(1, 0, 1),
                         Position = pos * 2
                     });
                     a.Reload();
                 }
                 else
                 {
                     last.Release();
                     a.Reload();
                 }*/
             });

            /* EventBus.Subscribe<TerrainUpdateEvent>(x =>
             {
                 var set = GenerateTerrain(x.Profile);
                 rc.UpdateInstanceSettings(set);
             });*/
        }

        public override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            cam.Update(0.01666);

            counter += e.Time;
            /*rc.UpdateInstanceSettings(new InstanceSettings()
            {
                Instances = terrainGenerator.GetTerrain().Select(x =>


                    new Instance()
                    {
                        Position = x.Position,
                        Colour = new Vector3((float)Math.Abs(Math.Cos(counter)))
                    }
                ).ToArray()
            });*/
        }
    }
}
