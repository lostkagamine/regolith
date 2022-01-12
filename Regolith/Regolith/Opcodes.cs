using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Regolith
{
    public static class Opcodes
    {
        // misc
        public const byte Nop = 0xee;
        public const byte Ret = 0xef;

        // arith
        public const byte Add = 0x00;
        public const byte Sub = 0x01;
        public const byte Mul = 0x02;
        public const byte Div = 0x03;
        public const byte Mod = 0x04;

        // branch/tests
        public const byte TestEqual = 0x08;
        public const byte TestLess = 0x09;
        // not-equal and greater than can be achieved using JNS
        public const byte JumpAlways = 0x11;
        public const byte JumpIfSet = 0x12;
        public const byte JumpIfNotSet = 0x13;

        public const byte Input = 0x20;
        public const byte Output = 0x21;

        public const byte Push = 0x22;
        public const byte Pop = 0x23;
    }
}
