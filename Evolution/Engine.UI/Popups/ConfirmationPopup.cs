using Engine.UI.Popups.Properties;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Engine.UI.Popups
{
    public class ConfirmationPopup : Popup<ConfirmationProperties>
    {
        public ConfirmationPopup() : base("Are you sure?") { }

        protected override void RenderPopup()
        {
            ImGui.Text(Properties.Body);

            ImGui.Separator();

            if (ImGui.Button("OK", new Vector2(120, 0))) { Properties.OnOK?.Invoke(); Visible = false; ImGui.CloseCurrentPopup(); }
            ImGui.SetItemDefaultFocus();
            ImGui.SameLine();
            if (ImGui.Button("Cancel", new Vector2(120, 0))) { Properties.OnClose?.Invoke(); Visible = false; ImGui.CloseCurrentPopup(); }

            ImGui.EndPopup();
        }
    }
}
