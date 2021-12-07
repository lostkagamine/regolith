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
        public static readonly byte Nop = 0xee;
        public static readonly byte Ret = 0xef;

        // arith
        public static readonly byte Add = 0x00;
        public static readonly byte Sub = 0x01;
        public static readonly byte Mul = 0x02;
        public static readonly byte Div = 0x03;
        public static readonly byte Mod = 0x04;

        // branch/tests
        public static readonly byte TestEqual = 0x08;
        public static readonly byte TestLess = 0x09;
        // not-equal and greater than can be achieved using INZ
        // cmplt RA, RB
        // inz
        // jzs label
        public static readonly byte InvertZero = 0x10;
        public static readonly byte JumpAlways = 0x11;
        public static readonly byte JumpIfSet = 0x12;
        public static readonly byte JumpIfNotSet = 0x13;
    }
}
