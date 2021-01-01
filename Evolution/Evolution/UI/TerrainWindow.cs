using Engine.Terrain;
using Engine.Terrain.Data;
using Engine.Terrain.Events;
using Engine.Terrain.Noise;
using Engine.UI;
using ImGuiNET;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DotnetNoise.FastNoise;

namespace Evolution.UI
{
    public class TerrainWindow : UIWindow
    {
        private int _heightNoiseSelected;
        private TerrainManager _manager;
        private TerrainProfile _profile;
        private IEventBus _eventBus;
        private bool _isolate;

        private static string[] NoiseTypes;
        private static string[] FractalTypes;
        private static string[] RenderTypes;

        private NoiseConfiguration CurrentHeightNoise
        {
            get => _profile.HeightNoise[_heightNoiseSelected];
        }

        public TerrainWindow(string name, TerrainManager manager, IEventBus eventBus) : base(name)
        {
            NoiseTypes = new[] { "Value", "Value Fractal", "Perlin", "Perlin Fractal", "Simplex", "Simplex Fractal", "Cellular", "White Noise", "Cubic", "Cubic Fractal" };
            FractalTypes = new[] { "FBM", "Billow", "Rigid Multi" };
            RenderTypes = new[] { "Default", "Height", "Temperature" };
            _manager = manager;
            _profile = _manager.Profile;
            _eventBus = eventBus;
        }

        protected override void RenderWindow()
        {
            if (ImGui.BeginTabBar("##Tabs"))
            {
                RenderHeightMapTab();
            }
        }

        private void RenderHeightMapTab()
        {
            if (ImGui.BeginTabItem("Height map"))
            {
                RenderHeightMapTab_ManageButtons();

                RenderHeightMapTab_LeftPane();
                ImGui.SameLine();
                bool change = RenderHeightMapTab_RightPane();

                if (change)
                {
                    UpdateTerrain();
                }

                ImGui.EndTabItem();
            }
        }

        private void RenderHeightMapTab_ManageButtons()
        {

            if(ImGui.Button("Create"))
            {
                var noise = new NoiseConfiguration("new");
                _profile.HeightNoise.Add(noise);
                _eventBus.Publish(new TerrainNoiseAddedEvent() { Noise = noise });
                UpdateTerrain();
            }
            ImGui.SameLine();
            ImGui.PushStyleColor(ImGuiCol.Button, ButtonColour.Red);
            ImGui.Button("Remove");
            ImGui.PopStyleColor();

            ImGui.SameLine();

            if (ImGui.Checkbox("Isolate selected layer", ref _isolate)) UpdateIsolatedLayer();
        }

        private void RenderHeightMapTab_LeftPane()
        {
            ImGui.BeginChild("left pane", new System.Numerics.Vector2(173, 0), true);

            for (int i = 0; i < _profile.HeightNoise.Count; i++)
            {
                if (ImGui.Selectable(_profile.HeightNoise[i].Name, i == _heightNoiseSelected))
                {
                    _heightNoiseSelected = i;
                    UpdateIsolatedLayer();
                }
            }
            ImGui.EndChild();
        }

        private bool RenderHeightMapTab_RightPane()
        {
            bool changeMade = false;
            ImGui.BeginGroup();
            ImGui.BeginChild("item view", new System.Numerics.Vector2(0, -ImGui.GetFrameHeightWithSpacing()), true);

            ImGui.Text("Global height map settings");
            ImGui.Separator();
            changeMade |= ImGui.Combo("Render Type", ref _manager.RenderModeInt, RenderTypes, RenderTypes.Length);
            changeMade |= ImGui.DragFloat("Sea level", ref _profile.SeaLevel, 0.01f, 0.0f, 1.0f);

            ImGui.Text($"Editing height map layer: {CurrentHeightNoise.Name}");
            ImGui.Separator();

            changeMade |= ImGui.Combo("Noise type", ref CurrentHeightNoise.Type, NoiseTypes, NoiseTypes.Length);
            changeMade |= ImGui.InputText("Layer name", ref CurrentHeightNoise.Name, 32u);
            changeMade |= ImGui.DragInt("Seed", ref CurrentHeightNoise.Seed);
            changeMade |= ImGui.DragFloat("Scale", ref CurrentHeightNoise.Scale);
            changeMade |= ImGui.SliderFloat("Frequency", ref CurrentHeightNoise.Frequency, 0.0f, 2.0f);

            ImGui.Separator();

            if(((NoiseType)CurrentHeightNoise.Type).ToString().ToLower().Contains("fractal"))
            {
                changeMade |= ImGui.Combo("Fractal type", ref CurrentHeightNoise.FractalType, FractalTypes, FractalTypes.Length);
                changeMade |= ImGui.SliderInt("Octaves", ref CurrentHeightNoise.Octaves, 1, 12);
                changeMade |= ImGui.DragFloat("Lacunarity", ref CurrentHeightNoise.Lacunarity, 0.1f, 0.0f, 0.2f);
                changeMade |= ImGui.DragFloat("Gain", ref CurrentHeightNoise.Gain, 0.1f, 0.0f, 0.2f);
                ImGui.Separator();
            }

            changeMade |= ImGui.SliderFloat2("Offset", ref CurrentHeightNoise.Offset, -1000, 1000);
            changeMade |= ImGui.Checkbox("Inverse", ref CurrentHeightNoise.Invert);
            changeMade |= ImGui.Checkbox("Mask", ref CurrentHeightNoise.Mask);
            changeMade |= ImGui.Checkbox("Round", ref CurrentHeightNoise.Round);

            ImGui.EndChild();
            ImGui.EndGroup();

            return changeMade;
        }

        private void RenderClimateTab()
        {
            
        }

        private void UpdateTerrain() => _eventBus.Publish(new TerrainUpdateEvent()
        {
            Profile = _profile
        });

        private void UpdateIsolatedLayer()
        {
            for (int i = 0; i < _profile.HeightNoise.Count; i++)
            {
                bool vis = !_isolate || i == _heightNoiseSelected;
                _profile.HeightNoise[i].Visible = vis;
            }

            UpdateTerrain();
        }
    }
}
