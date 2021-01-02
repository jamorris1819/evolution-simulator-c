using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Engine.UI.Windows
{
    public static class ConfirmationWindow
    {
        private static Action _okAction;
        private static Action _closeAction;
        private static string _description;

        public static void Show(Action ok, Action close, string desc)
        {
            ImGui.OpenPopup("Are you sure?");
            _okAction = ok;
            _closeAction = close;
            _description = desc;
        }

        public static void Show(Action ok, string desc) => Show(ok, () => { }, desc);

        public static void Render()
        {
            var center = new Vector2(ImGui.GetIO().DisplaySize.X * 0.5f, ImGui.GetIO().DisplaySize.Y * 0.5f);
            ImGui.SetNextWindowPos(center, ImGuiCond.Appearing, new Vector2(0.5f, 0.5f));

            bool open = true;
            if (ImGui.BeginPopupModal("Are you sure?", ref open, ImGuiWindowFlags.AlwaysAutoResize))
            {
                ImGui.Text(_description + "\n\n");
                ImGui.Separator();

                if (ImGui.Button("OK", new Vector2(120, 0))) { _okAction(); ImGui.CloseCurrentPopup(); }
                ImGui.SetItemDefaultFocus();
                ImGui.SameLine();
                if (ImGui.Button("Cancel", new Vector2(120, 0))) { _closeAction(); ImGui.CloseCurrentPopup(); }
                ImGui.EndPopup();
            }
        }
    }
}
