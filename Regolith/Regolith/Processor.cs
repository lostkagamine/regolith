using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Regolith
{
    public class Processor
    {
        // Next Address
        // Address of the next instruction to execute.
        public ushort NA;

        // General registers
        public ushort RX;
        public ushort RA;
        public ushort RB;
        public ushort RC;
        public ushort RD;

        // Wide register
        // Accessed via RWL and RWH
        public uint RW;

        // Stack pointer
        public uint RSP;

        // Reserved registers
        // Used for internal stuff. User should not
        // be touching these otherwise the
        // CPU might go down.

        // Reserved Register: Interrupt Table
        public uint RRIT;



        // Flags
        public ushort Flags;
    }
}
