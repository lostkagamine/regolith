using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;

namespace Lithograph.Windowing.Windows
{
    public class MenuBar : Window
    {
        public override void Render()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("Lithograph"))
                {
                    if (ImGui.MenuItem("About Lithograph"))
                        WindowManager.Open<AboutWindow>();
                    ImGui.Separator();
                    if (ImGui.MenuItem("Quit"))
                        Environment.Exit(0);
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("File"))
                {
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("View"))
                {
                    if (ImGui.MenuItem("Processor"))
                        WindowManager.Open<ProcessorWindow>();
                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }
    }
}
