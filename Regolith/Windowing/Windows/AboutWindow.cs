using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Windowing.Windows
{
    public class AboutWindow : Window
    {
        public override void Render()
        {
            if (ImGui.Begin("About Lithograph", ref Open, ImGuiWindowFlags.AlwaysAutoResize))
            {
                ImGui.Text("Lithograph - interactive simulator and");
                ImGui.Text("visual debugger for Regolith16/Monolith");
                ImGui.NewLine();
                ImGui.Text("Created by Rin, 2021.");
                ImGui.NewLine();
                ImGui.Text("Regolith16 is a 16-bit microprocessor architecture.");
                ImGui.Text("Monolith is a computer system built on Regolith16.");
                ImGui.NewLine();
                ImGui.Text($"Running on dotnet {Environment.Version}.");
                ImGui.End();
            }
        }
    }
}
