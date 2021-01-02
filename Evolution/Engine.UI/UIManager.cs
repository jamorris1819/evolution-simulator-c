using Engine.UI.Core;
using Engine.UI.Popups;
using Engine.UI.Popups.Events;
using ImGuiNET;
using OpenTK.Windowing.Desktop;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Engine.UI
{
    public class UIManager
    {
        private GameWindow _gameWindow;
        private ImGuiController _controller;
        private IEventBus _eventBus;

        private ConfirmationPopup _confirmationPopup;

        public List<UIWindow> Windows { get; set; }

        public UIManager(GameWindow gameWindow, IEventBus eventBus)
        {
            _gameWindow = gameWindow;
            _eventBus = eventBus;
            Windows = new List<UIWindow>();
            _controller = new ImGuiController(gameWindow.Size.X, gameWindow.Size.Y);
            CreateStyle();

            _confirmationPopup = new ConfirmationPopup();

            _eventBus.Subscribe<ConfirmationEvent>(x => _confirmationPopup.Show(x.Properties));
        }

        public void Render()
        {
            _confirmationPopup.Render();
            for(int i = 0; i < Windows.Count; i++)
            {
                Windows[i].Render();
            }

            ImGui.ShowDemoWindow();
            _controller.Render();
        }

        public void Update(double dt)
        {
            _controller.Update(_gameWindow, (float)dt);
        }

        private void CreateStyle()
        {
            ImGui.GetIO().Fonts.AddFontFromFileTTF("Ruda-Bold.ttf", 15.0f);
            ImGui.GetStyle().FrameRounding = 4.0f;
            ImGui.GetStyle().GrabRounding = 4.0f;

            var colors = ImGui.GetStyle().Colors;
            colors[(int)ImGuiCol.Text] = new Vector4(0.95f, 0.96f, 0.98f, 1.00f);
            colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.36f, 0.42f, 0.47f, 1.00f);
            colors[(int)ImGuiCol.WindowBg] = new Vector4(0.11f, 0.15f, 0.17f, 1.00f);
            colors[(int)ImGuiCol.ChildBg] = new Vector4(0.15f, 0.18f, 0.22f, 1.00f);
            colors[(int)ImGuiCol.PopupBg] = new Vector4(0.08f, 0.08f, 0.08f, 0.94f);
            colors[(int)ImGuiCol.Border] = new Vector4(0.08f, 0.10f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            colors[(int)ImGuiCol.FrameBg] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
            colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.12f, 0.20f, 0.28f, 1.00f);
            colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.09f, 0.12f, 0.14f, 1.00f);
            colors[(int)ImGuiCol.TitleBg] = new Vector4(0.09f, 0.12f, 0.14f, 0.65f);
            colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.08f, 0.10f, 0.12f, 1.00f);
            colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.00f, 0.00f, 0.00f, 0.51f);
            colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.15f, 0.18f, 0.22f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.02f, 0.02f, 0.02f, 0.39f);
            colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.18f, 0.22f, 0.25f, 1.00f);
            colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.09f, 0.21f, 0.31f, 1.00f);
            colors[(int)ImGuiCol.CheckMark] = new Vector4(0.28f, 0.56f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.28f, 0.56f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.37f, 0.61f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.Button] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
            colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.28f, 0.56f, 1.00f, 1.00f);
            colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.06f, 0.53f, 0.98f, 1.00f);
            colors[(int)ImGuiCol.Header] = new Vector4(0.20f, 0.25f, 0.29f, 0.55f);
            colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.80f);
            colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
            colors[(int)ImGuiCol.Separator] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
            colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.10f, 0.40f, 0.75f, 0.78f);
            colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.10f, 0.40f, 0.75f, 1.00f);
            colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.26f, 0.59f, 0.98f, 0.25f);
            colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.67f);
            colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.95f);
            colors[(int)ImGuiCol.Tab] = new Vector4(0.11f, 0.15f, 0.17f, 1.00f);
            colors[(int)ImGuiCol.TabHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.80f);
            colors[(int)ImGuiCol.TabActive] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
            colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.11f, 0.15f, 0.17f, 1.00f);
            colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.11f, 0.15f, 0.17f, 1.00f);
            colors[(int)ImGuiCol.PlotLines] = new Vector4(0.61f, 0.61f, 0.61f, 1.00f);
            colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);
            colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.26f, 0.59f, 0.98f, 0.35f);
            colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.00f, 1.00f, 0.00f, 0.90f);
            colors[(int)ImGuiCol.NavHighlight] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
            colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.00f, 1.00f, 1.00f, 0.70f);
            colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.20f);
            colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.35f);
        }

        public void PressChar(char keyChar)
        {
            _controller.PressChar(keyChar);
        }

        public void MouseScroll(OpenTK.Mathematics.Vector2 offset)
        {
            _controller.MouseScroll(offset);
        }
    }
}
