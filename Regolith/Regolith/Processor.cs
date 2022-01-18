using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Regolith
{
    public enum CPUFlags
    {
        Zero = 1 << 0,
        Overflow = 1 << 1,
        
    }

    public class Processor
    {
        // Next Address
        // Address of the next instruction to execute.
        public uint NA;

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

        void Push(byte value)
        {
            Memory[--RSP] = value;
        }

        uint PopUint()
        {
            var val = 0u;
            val += Memory[++RSP];
            val += (uint)(Memory[++RSP] << 16);
            return val;
        }

        byte PopByte()
        {
            return Memory[++RSP];
        }

        uint GetValueForRindex(RegisterIndex idx)
        {
            switch (idx)
            {
                case RegisterIndex.RX:
                    return RX;
                case RegisterIndex.RA:
                    return RA;
                case RegisterIndex.RB:
                    return RB;
                case RegisterIndex.RC:
                    return RC;
                case RegisterIndex.RD:
                    return RD;
                case RegisterIndex.RE:
                    return RE;
                case RegisterIndex.RF:
                    return RF;
                case RegisterIndex.NA:
                    return NA;
            }
            return 0;
        }

        public void RunCycle()
        {
            unchecked
            {
                // all instructions are two bytes wide
                var opcode = Memory[NA];
                var odraw = Memory[NA + 1];
                var opdata = Opdata.Parse(odraw);
                var naOff = 0u;

                byte NextByte()
                {
                    return Memory[NA + 2 + (naOff++)];
                }

                ushort NextUshort()
                {
                    var r = (ushort)0;
                    r += (ushort)(NextByte() << 8);
                    r += NextByte();
                    return r;
                }

                ushort Data()
                {
                    switch (opdata.Mode)
                    {
                        case AddressMode.Immediate:
                            return NextUshort();
                        case AddressMode.MemoryAtImmediate:
                            return Memory[NextUshort()];
                        case AddressMode.MemoryAtRegisterValue:
                            return Memory[GetValueForRindex(opdata.Register)];
                    }
                    return 0;
                }

                void UpdateFlags()
                {
                    if (RX == 0)
                    {
                        Flags = (ushort)(Flags & (ushort)CPUFlags.Zero);
                    }
                }
                
                switch (opcode)
                {
                    case Opcodes.Nop:
                        break;
                    case Opcodes.Ret:
                        NA = PopUint();
                        return;
                    case Opcodes.Add:
                        RX += Data();
                        UpdateFlags();
                        break;
                    case Opcodes.TestEqual:
                        {
                            var regind = NextByte();
                            var rd = GetValueForRindex((RegisterIndex)regind);
                            var d = Data();
                            if (rd == d)
                            {
                                Flags = (ushort)(Flags & (ushort)CPUFlags.Zero);
                            }
                            else
                            {
                                Flags = (ushort)(Flags & ~(ushort)CPUFlags.Zero);
                            }
                        }
                        break;
                    case Opcodes.TestLess:
                        {
                            var regind = NextByte();
                            var rd = GetValueForRindex((RegisterIndex)regind);
                            var d = Data();
                            if (rd < d)
                            {
                                Flags = (ushort)(Flags & (ushort)CPUFlags.Zero);
                            }
                            else
                            {
                                Flags = (ushort)(Flags & ~(ushort)CPUFlags.Zero);
                            }
                        }
                        break;
                    case Opcodes.JumpAlways:
                        {
                            var d = Data();
                            NA = d;
                            return;
                        }
                    case Opcodes.JumpIfSet:
                        {
                            var d = Data();
                            if ((Flags & (ushort)CPUFlags.Zero) > 0)
                            {
                                NA = d;
                                return;
                            }
                            break;
                        }
                    case Opcodes.JumpIfNotSet:
                        {
                            var d = Data();
                            if ((Flags & (ushort)CPUFlags.Zero) == 0)
                            {
                                NA = d;
                                return;
                            }
                            break;
                        }
                }

                NA += 2 + naOff;
            }
        }
    }
}
