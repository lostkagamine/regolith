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
        public ushort RE;
        public ushort RF;

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


        public MemorySpace Memory;

        public Processor(MemorySpace mem)
        {
            Memory = mem;
        }

        public void Reset()
        {
            NA = 0x0_0100;
            RX = 0x0000;
            RA = 0x0000;
            RB = 0x0000;
            RC = 0x0000;
            RD = 0x0000;
            RE = 0x0000;
            RF = 0x0000;
            RW = 0x0000_0000;

            RSP = 0x6000;

            RRIT = 0x0000;
        }

        void Push(uint value)
        {
            Memory[--RSP] = (byte)((value & 0xFFFF_0000) >> 16);
            Memory[--RSP] = (byte)((value & 0x0000_FFFF));
        }

        uint PopUint()
        {
            var val = 0u;
            val += Memory[++RSP];
            val += (uint)(Memory[++RSP] << 16);
            return val;
        }

        public void RunCycle()
        {
            // all instructions are two bytes wide
            var opcode = Memory[NA];
            var opdata = Memory[NA + 1];

            switch (opcode)
            {
                case Opcodes.Nop:
                    break;
                case Opcodes.Ret:
                    // TODO
                    break;
            }

            NA += 2;
        }
    }
}
