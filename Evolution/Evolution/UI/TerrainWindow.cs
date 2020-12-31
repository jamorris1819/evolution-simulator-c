using Engine.Terrain;
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
        private List<NoiseConfiguration> _heightNoise;
        private IEventBus _eventBus;

        private static string[] NoiseTypes;
        private static string[] FractalTypes;

        private NoiseConfiguration CurrentNoise
        {
            get => _heightNoise[_heightNoiseSelected];
        }

        public TerrainWindow(string name, IEnumerable<NoiseConfiguration> configs, IEventBus eventBus) : base(name)
        {
            NoiseTypes = new[] { "Value", "Value Fractal", "Perlin", "Perlin Fractal", "Simplex", "Simplex Fractal", "Cellular", "White Noise", "Cubic", "Cubic Fractal" };
            FractalTypes = new[] { "FBM", "Billow", "Rigid Multi" };
            _heightNoise = configs.ToList();
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
                    _eventBus.Publish(new TerrainUpdateEvent());
                }

                ImGui.EndTabItem();
            }
        }

        private void RenderHeightMapTab_ManageButtons()
        {

            if(ImGui.Button("Create"))
            {
                _heightNoise.Add(new NoiseConfiguration("new"));
            }
            ImGui.SameLine();
            ImGui.PushStyleColor(ImGuiCol.Button, ButtonColour.Red);
            ImGui.Button("Remove");
            ImGui.PopStyleColor();
        }

        private void RenderHeightMapTab_LeftPane()
        {
            ImGui.BeginChild("left pane", new System.Numerics.Vector2(173, 0), true);

            

            for (int i = 0; i < _heightNoise.Count; i++)
            {
                if (ImGui.Selectable(_heightNoise[i].Name, i == _heightNoiseSelected))
                    _heightNoiseSelected = i;
            }
            ImGui.EndChild();
        }

        private bool RenderHeightMapTab_RightPane()
        {
            bool changeMade = false;
            ImGui.BeginGroup();
            ImGui.BeginChild("item view", new System.Numerics.Vector2(0, -ImGui.GetFrameHeightWithSpacing()), true);
            
            ImGui.Text($"Editing height map layer: {CurrentNoise.Name}");
            ImGui.Separator();

            changeMade |= ImGui.Combo("Noise type", ref CurrentNoise.Type, NoiseTypes, NoiseTypes.Length);
            changeMade |= ImGui.InputText("Layer name", ref CurrentNoise.Name, 32u);
            changeMade |= ImGui.DragInt("Seed", ref CurrentNoise.Seed);
            changeMade |= ImGui.DragFloat("Scale", ref CurrentNoise.Scale);
            changeMade |= ImGui.SliderFloat("Frequency", ref CurrentNoise.Frequency, 0.0f, 2.0f);

            ImGui.Separator();

            if(((NoiseType)CurrentNoise.Type).ToString().ToLower().Contains("fractal"))
            {
                changeMade |= ImGui.Combo("Fractal type", ref CurrentNoise.FractalType, FractalTypes, FractalTypes.Length);
                changeMade |= ImGui.SliderInt("Octaves", ref CurrentNoise.Octaves, 1, 12);
                changeMade |= ImGui.DragFloat("Lacunarity", ref CurrentNoise.Lacunarity, 0.1f, 0.0f, 0.2f);
                changeMade |= ImGui.DragFloat("Gain", ref CurrentNoise.Gain, 0.1f, 0.0f, 0.2f);
                ImGui.Separator();
            }

            changeMade |= ImGui.SliderFloat2("Offset", ref CurrentNoise.Offset, -1000, 1000);
            changeMade |= ImGui.Checkbox("Inverse", ref CurrentNoise.Invert);
            changeMade |= ImGui.Checkbox("Mask", ref CurrentNoise.Mask);

            ImGui.EndChild();
            ImGui.EndGroup();

            return changeMade;
        }

        private void RenderClimateTab()
        {
            
        }
    }
}
