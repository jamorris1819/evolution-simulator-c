using Engine.UI;
using Evolution.Environment.Life.Creatures.Mouth.ConstructionModels;
using ImGuiNET;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.UI
{
    public class TestWindow : UIWindow
    {
        private readonly IEventBus _eventBus;

        private float length = 1;
        private float curveHeight = 0.5f;
        private float baseDiameter = 0.15f;
        private float thickness = 0.2f;

        public TestWindow(IEventBus eventBus) : base("Test window")
        {
            _eventBus = eventBus;
        }

        protected override void RenderWindow()
        {
            bool changeMade = false;

            changeMade |= ImGui.DragFloat("Length", ref length, 0.01f, 0, 10);
            changeMade |= ImGui.DragFloat("Curve Height", ref curveHeight, 0.01f, 0, 10);
            changeMade |= ImGui.DragFloat("Base Diameter", ref baseDiameter, 0.01f, 0.1f, 0.5f);
            changeMade |= ImGui.DragFloat("Thickness", ref thickness, 0.01f, 0.1f, 1);

            if (changeMade)
            {
                _eventBus.Publish(new TestEvent() { Model = new PincerModel(length, curveHeight, baseDiameter, thickness) });
            }
        }
    }
}
