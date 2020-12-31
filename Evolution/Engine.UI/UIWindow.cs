using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Engine.UI
{
    public abstract class UIWindow
    {
        protected bool visible;
        private bool collapsed;

        public string Title { get; protected set; }
        public bool Visible { get => visible; set => visible = value; }
        public bool Collapsed { get => collapsed; set => collapsed = value; }
        public Point Size { get; set; }

        public UIWindow(string title)
        {
            Title = title;
            Visible = true;
            Size = new Point(300, 300);
        }

        public virtual void Render()
        {
            if (!Visible) return;
            if(!ImGui.Begin(Title, ref collapsed))
            {
                ImGui.End();
                return;
            }
            RenderWindow();
            ImGui.End();
        }

        protected abstract void RenderWindow();
    }
}
