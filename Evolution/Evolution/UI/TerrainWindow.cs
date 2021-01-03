using Engine.Terrain;
using Engine.Terrain.Data;
using Engine.Terrain.Events;
using Engine.Terrain.Noise;
using Engine.UI;
using Engine.UI.Popups.Events;
using Engine.UI.Popups.Properties;
using ImGuiNET;
using Newtonsoft.Json;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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

        private string[] _mapFilesOnDisk;
        private int _fileSelected;
        private bool _unsavedChanges;

        private NoiseConfiguration CurrentHeightNoise
        {
            get => _profile.HeightNoise[_heightNoiseSelected];
        }

        public TerrainWindow(TerrainManager manager, IEventBus eventBus) : base("Terrain Window")
        {
            NoiseTypes = new[] { "Value", "Value Fractal", "Perlin", "Perlin Fractal", "Simplex", "Simplex Fractal", "Cellular", "White Noise", "Cubic", "Cubic Fractal" };
            FractalTypes = new[] { "FBM", "Billow", "Rigid Multi" };
            RenderTypes = new[] { "Default", "Height", "Temperature", "Rainfall" };
            _manager = manager;
            _profile = _manager.Profile;
            _eventBus = eventBus;

            CalculateFiles();
        }

        private void CalculateFiles()
        {
            _mapFilesOnDisk = Directory.GetFiles(Directory.GetCurrentDirectory() + "/maps", "*.map").Select(x => x.Split('\\').Last().Split('.').First()).ToArray();
        }

        protected override void RenderWindow()
        {
            RenderGeneral();
            RenderHeightMap();
            RenderRainfallMap();
        }

        private void RenderGeneral()
        {
            if (ImGui.CollapsingHeader("General"))
            {
                RenderGeneral_LeftPane();

                ImGui.SameLine();
                bool change = RenderGeneral_RightPane();

                if (change)
                {
                    UpdateTerrain();

                    _unsavedChanges = true;
                }
            }
        }

        private void RenderGeneral_LeftPane()
        {
            ImGui.BeginChild("left pane general", new System.Numerics.Vector2(173, 300), true);

            for (int i = 0; i < _mapFilesOnDisk.Length; i++)
            {
                if (ImGui.Selectable(_mapFilesOnDisk[i], i == _fileSelected))
                {
                    _fileSelected = i;
                }
            }
            ImGui.EndChild();
        }

        private bool RenderGeneral_RightPane()
        {
            bool changeMade = false;
            ImGui.BeginGroup();
            ImGui.BeginChild("right pane general", new System.Numerics.Vector2(0, 400), true);

            ImGui.InputText("Map name", ref _profile.Name, 64u);
            ImGui.InputTextMultiline("Description", ref _profile.Description, 512u, new Vector2(0, 0));
            ImGui.InputText("Creator", ref _profile.Creator, 64u);



            ImGui.Separator();
            changeMade |= ImGui.Combo("Render Type", ref _manager.RenderModeInt, RenderTypes, RenderTypes.Length);
            changeMade |= ImGui.DragFloat("Sea level", ref _profile.SeaLevel, 0.01f, -1.0f, 1.0f);
            changeMade |= ImGui.DragFloat("Tide level", ref _profile.TideLevel, 0.01f, 0.0f, 0.5f);
            ImGui.Separator();
            changeMade |= ImGui.Checkbox("Island", ref _profile.Island);

            if (_profile.Island) {
                changeMade |= ImGui.DragInt("Edge seed", ref _profile.IslandSeed);
                changeMade |= ImGui.DragFloat("Edge scale ", ref _profile.IslandEdgeDistortion, 0.1f, 0.0f, 64.0f);
                changeMade |= ImGui.DragFloat("Drop off point", ref _profile.DropOffPoint, 0.01f, 0.0f, 1.0f);
                changeMade |= ImGui.DragFloat("Drop off steepness", ref _profile.DropOffSteepness, 0.01f, 0.0f, 8.0f);
            }

            if (ImGui.Button("Save"))
            {
                void SaveFile()
                {
                    string s = JsonConvert.SerializeObject(_profile);
                    File.WriteAllText(Directory.GetCurrentDirectory() + "/maps/" + _profile.Name + ".map", s);
                    _unsavedChanges = false;
                    CalculateFiles();
                }

                if (File.Exists(Directory.GetCurrentDirectory() + "/maps/" + _profile.Name + ".map"))
                {
                    _eventBus.Publish(new ConfirmationEvent()
                    {
                        Properties = new ConfirmationProperties()
                        {
                            OnOK = SaveFile,
                            OnClose = () => { },
                            Body = "A map with that name already exists.\n\nAre you sure you want to overwrite it?"
                        }
                    });
                }
                else
                {
                    SaveFile();
                }
            }

            ImGui.EndChild();
            ImGui.EndGroup();
            return changeMade;
        }

        private void RenderHeightMap()
        {
            if (ImGui.CollapsingHeader("Height map"))
            {
                RenderHeightMap_ManageButtons();

                RenderHeightMap_LeftPane();
                ImGui.SameLine();
                bool change = RenderHeightMap_RightPane();

                if (change)
                {
                    UpdateTerrain();

                    _unsavedChanges = true;
                }

            }
        }

        private void RenderHeightMap_ManageButtons()
        {

            if (ImGui.Button("Create"))
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

        private void RenderHeightMap_LeftPane()
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

        private bool RenderHeightMap_RightPane()
        {
            bool changeMade = false;
            ImGui.BeginGroup();
            ImGui.BeginChild("item view", new System.Numerics.Vector2(0, -ImGui.GetFrameHeightWithSpacing()), true);

            ImGui.Text($"Editing height map layer: {CurrentHeightNoise.Name}");
            ImGui.Separator();

            changeMade |= ImGui.Combo("Noise type", ref CurrentHeightNoise.Type, NoiseTypes, NoiseTypes.Length);
            changeMade |= ImGui.InputText("Layer name", ref CurrentHeightNoise.Name, 32u);
            changeMade |= ImGui.DragInt("Seed", ref CurrentHeightNoise.Seed);
            changeMade |= ImGui.DragFloat("Scale", ref CurrentHeightNoise.Scale);
            changeMade |= ImGui.SliderFloat("Frequency", ref CurrentHeightNoise.Frequency, 0.0f, 2.0f);

            ImGui.Separator();

            if (((NoiseType)CurrentHeightNoise.Type).ToString().ToLower().Contains("fractal"))
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

        private void RenderRainfallMap()
        {
            if (ImGui.CollapsingHeader("Rainfall map"))
            {
                bool change = RenderRainfallMap_Pane();

                if (change)
                {
                    UpdateTerrain();

                    _unsavedChanges = true;
                }

            }
        }

        private bool RenderRainfallMap_Pane()
        {
            bool changeMade = false;
            ImGui.BeginGroup();
            ImGui.BeginChild("rainfall item view", new Vector2(0, -ImGui.GetFrameHeightWithSpacing()), true);

            ImGui.Text($"Editing rainfall map layer");
            changeMade |= ImGui.Combo("Render Type", ref _manager.RenderModeInt, RenderTypes, RenderTypes.Length);
            ImGui.Separator();

            changeMade |= ImGui.Combo("Noise type", ref _profile.RainfallNoise.Type, NoiseTypes, NoiseTypes.Length);
            changeMade |= ImGui.InputText("Layer name", ref _profile.RainfallNoise.Name, 32u);
            changeMade |= ImGui.DragInt("Seed", ref _profile.RainfallNoise.Seed);
            changeMade |= ImGui.DragFloat("Scale", ref _profile.RainfallNoise.Scale);
            changeMade |= ImGui.SliderFloat("Frequency", ref _profile.RainfallNoise.Frequency, 0.0f, 2.0f);

            ImGui.Separator();

            if (((NoiseType)_profile.RainfallNoise.Type).ToString().ToLower().Contains("fractal"))
            {
                changeMade |= ImGui.Combo("Fractal type", ref _profile.RainfallNoise.FractalType, FractalTypes, FractalTypes.Length);
                changeMade |= ImGui.SliderInt("Octaves", ref _profile.RainfallNoise.Octaves, 1, 12);
                changeMade |= ImGui.DragFloat("Lacunarity", ref _profile.RainfallNoise.Lacunarity, 0.1f, 0.0f, 0.2f);
                changeMade |= ImGui.DragFloat("Gain", ref _profile.RainfallNoise.Gain, 0.1f, 0.0f, 0.2f);
                ImGui.Separator();
            }

            changeMade |= ImGui.SliderFloat2("Offset", ref _profile.RainfallNoise.Offset, -1000, 1000);
            changeMade |= ImGui.Checkbox("Inverse", ref _profile.RainfallNoise.Invert);
            changeMade |= ImGui.Checkbox("Mask", ref _profile.RainfallNoise.Mask);
            changeMade |= ImGui.Checkbox("Round", ref _profile.RainfallNoise.Round);

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
