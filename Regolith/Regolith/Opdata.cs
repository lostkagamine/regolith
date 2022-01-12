using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Regolith
{
    public enum OpdataFlags
    {
        Immediate = 1 << 0,
        RegisterPlusOffset = 1 << 1
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
        public OpdataFlags Flags;

        public static Opdata Parse(byte od)
        {
            var res = new Opdata();

            res.Register = (RegisterIndex)((od & 0xF0) >> 4);
            res.Flags = (OpdataFlags)(od & 0x0F);

            return res;
        }
    }
}
