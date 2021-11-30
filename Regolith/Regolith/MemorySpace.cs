using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Regolith
{
    public class MemorySpace
    {
        public static readonly long MAX_POSSIBLE_MEMORY = 2 ^ 20;

        public byte[] Memory = new byte[MAX_POSSIBLE_MEMORY];
    }
}
