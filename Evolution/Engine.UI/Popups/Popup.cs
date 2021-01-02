using Engine.UI.Popups.Properties;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Engine.UI.Popups
{
    public abstract class Popup<T> where T: PopupProperties
    {
        public string Name { get; protected set; }

        public bool Visible { get; set; }

        protected T Properties { get; set; }

        public Popup(string name)
        {
            Name = name;
        }

        public void Render()
        {
            if (!Visible) return;

            var center = new Vector2(ImGui.GetIO().DisplaySize.X * 0.5f, ImGui.GetIO().DisplaySize.Y * 0.5f);
            ImGui.SetNextWindowPos(center, ImGuiCond.Appearing, new Vector2(0.5f, 0.5f));

            bool open = true;
            ImGui.OpenPopup(Name);
            if (ImGui.BeginPopupModal(Name, ref open, ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoTitleBar))
            {
                RenderPopup();
            }
        }

        public void Show(T props)
        {
            Visible = true;
            Properties = props;
        }

        protected abstract void RenderPopup();
    }
}
