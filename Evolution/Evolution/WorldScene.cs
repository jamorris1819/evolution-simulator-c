using Engine;
using Engine.Core;
using Engine.Core.Events;
using Engine.Core.Events.Input.Mouse;
using Engine.Render;
using Engine.Render.Data;
using Engine.Render.Data.Primitives;
using Engine.Terrain.Data;
using Engine.Terrain.Events;
using Engine.Terrain.Generator;
using Engine.Terrain.Noise;
using Engine.UI.Core;
using Evolution.UI;
using ImGuiNET;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution
{
    public class WorldScene : GameScene
    {
        Camera cam;
        ITerrainGenerator terrainGenerator;
        RenderComponent rc;
        double counter = 0;

        Entity terrainEntity;

        public WorldScene(Game game) : base(game)
        {
            terrainGenerator = new HexTerrainGenerator(Game.EventBus);
            game.UIManager.Windows.Add(new TerrainWindow("Terrain Generation", terrainGenerator.TerrainProfile, Game.EventBus));

            var settings = GenerateTerrain(terrainGenerator.TerrainProfile);

            terrainEntity = new Entity("Terrain");

            var shape = terrainGenerator.TerrainShape;


            rc = new RenderComponent(shape, settings);
            rc.Shader = Engine.Render.Shaders.Enums.ShaderType.StandardInstanced;
            terrainEntity.AddComponent(rc);
            EntityManager.AddEntity(terrainEntity);

            cam = new MouseCamera(1920, 1080, EventBus, Game.ShaderManager);

            EventBus.Subscribe<MouseDownEvent>((e) =>
            {
                var pos = cam.ScreenToWorld(e.Location);
                var hex = terrainGenerator.Layout.PixelToHex(pos).Round();
                Console.WriteLine($"{pos} -> {hex}");
            });

            EventBus.Subscribe<TerrainUpdateEvent>(x =>
            {
                var set = GenerateTerrain(x.Profile);
                rc.UpdateInstanceSettings(set);
            });
        }

        private InstanceSettings GenerateTerrain(TerrainProfile profile)
        {
            terrainGenerator.SetProfile(profile);
            terrainGenerator.Generate();
            var units = terrainGenerator.GetTerrain();
            var settings = new InstanceSettings()
            {
                Instances = units.Select(x =>


                    new Instance()
                    {
                        Position = x.Position,
                        Colour = new Vector3(x.Height)
                    }
                ).ToArray()
            };

            return settings;
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
