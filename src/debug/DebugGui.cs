using ImGuiNET;
using SerpentEngine;

namespace CastleGame;
public class DebugGui : ImGuiDrawable
{
    public override void Draw()
    {
        if (ImGui.Begin("Debug"))
        {
            ImGui.Text("Hello, world!");
        }
    }
}
