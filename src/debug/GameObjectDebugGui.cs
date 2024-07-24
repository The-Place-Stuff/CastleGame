using ImGuiNET;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame
{
    public class GameObjectDebugGui : DebugGui
    {
        public GameObject GameObject { get; set; }
        public GameObjectDebugGui(GameObject gameObject) : base()
        {
            GameObject = gameObject;
        }

        public override void Draw()
        {
            if (ImGui.Begin(GameObject.ToString())) 
            {
                ImGui.Text(GameObject.Position.ToString());
                ImGui.Text(GameObject.Components.ToString());

            }
        }
    }
}
