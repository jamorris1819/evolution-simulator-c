using Engine;
using Engine.Core;
using Engine.Core.Events;
using Engine.Core.Events.Input.Mouse;
using Engine.Render;
using Engine.Render.Data;
using Engine.Render.Data.Primitives;
using Engine.Terrain;
using Engine.UI;
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
        ImGuiController ui;

        public WorldScene(Game game) : base(game)
        {
            ui = new ImGuiController(1920, 1080);

            terrainGenerator = new HexTerrainGenerator();
            terrainGenerator.Generate(25);
            var shape = terrainGenerator.TerrainShape;

            Entity e = new Entity("Terrain");
            Random random = new Random();

            var units = terrainGenerator.GetTerrain();

            var settings = new InstanceSettings()
            {
                Instances = units.Select(x =>
                

                    new Instance()
                    {
                        Position = x.Position,
                        Colour = x.Colour
                    }
                ).ToArray()
            };

            rc = new RenderComponent(shape, settings);
            rc.Shader = Engine.Render.Shaders.Enums.ShaderType.StandardInstanced;
            e.AddComponent(rc);
            EntityManager.AddEntity(e);

            cam = new MouseCamera(1920, 1080, EventBus, Game.ShaderManager);

            EventBus.Subscribe<MouseDownEvent>((e) =>
            {
                var pos = cam.ScreenToWorld(e.Location);
                var hex = terrainGenerator.Layout.PixelToHex(pos).Round();
                Console.WriteLine($"{pos} -> {hex}");
            });
            //EventBus.Subscribe<MouseDragEvent>((e) => Console.WriteLine($"Mouse drag {e.Button} {e.Location} {e.Delta}"));
        }

        public override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            test();
            ui.Render();
        }

        private void test()
        {
            ImGui.SetNextWindowSize(new System.Numerics.Vector2(400, 400));

            if (!ImGui.Begin("hello"))
            {
                ImGui.End();
                return;
            }

            ImGui.Button("press me");

            ImGui.End();
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            cam.Update(0.01666);
            ui.Update(null, (float)e.Time);
            if (counter > 15) return;
            counter += e.Time;
            rc.UpdateInstanceSettings(new InstanceSettings()
            {
                Instances = terrainGenerator.GetTerrain().Select(x =>


                    new Instance()
                    {
                        Position = x.Position,
                        Colour = new Vector3((float)Math.Abs(Math.Cos(counter)))
                    }
                ).ToArray()
            });
        }
    }
}
