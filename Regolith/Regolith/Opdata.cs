using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Regolith
{
    public enum AddressMode
    {
        Immediate = 1,
        MemoryAtImmediate = 1 << 1,
        MemoryAtRegisterValue = 1 << 2
    }

    public enum RegisterIndex
    {
        RX = 0,
        RA = 1,
        RB = 2,
        RC = 3,
        RD = 4,
        RE = 5,
        RF = 6,
        NA = 7
    }

    public class Opdata
    {
        public RegisterIndex Register;
        public AddressMode Mode;

        public static Opdata Parse(byte od)
        {
            var res = new Opdata();

            res.Register = (RegisterIndex)((od & 0xF0) >> 4);
            res.Mode = (AddressMode)(od & 0x0F);

            return res;
        }
    }
}
