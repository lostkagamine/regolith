using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;
using Lithograph.Core;

namespace Lithograph.Windowing.Windows
{
    internal class ProcessorWindow : Window
    {
        public override void Render()
        {
            if (ImGui.Begin("Processor"))
            {
                ImGui.Text(string.Format(
                    "NA  = {0:X5}\n" +
                    "RX  = {1:X4}\n\n" +
                    "RA   RB   RC   RD   RE   RF\n" +
                    "{2:X4} {3:X4} {4:X4} {5:X4} {6:X4} {7:X4}\n\n" +
                    "RW  = {8:X8}\n" +
                    "RSP = {9:X8}",
                    
                    GlobalState.CPU.NA,
                    GlobalState.CPU.RX,
                    GlobalState.CPU.RA,
                    GlobalState.CPU.RB,
                    GlobalState.CPU.RC,
                    GlobalState.CPU.RD,
                    GlobalState.CPU.RE,
                    GlobalState.CPU.RF,
                    GlobalState.CPU.RW,
                    GlobalState.CPU.RSP));
            }
            ImGui.End();
        }
    }
}
