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

        public byte[] Memory; //= new byte[MAX_POSSIBLE_MEMORY];

        public MemorySpace()
        {
            Memory = new byte[MAX_POSSIBLE_MEMORY];
            for (int i=0; i<Memory.Length; i++)
            {
                Memory[i] = 0;
            }
        }

        public void LoadROMFile(ROMFile rom, uint offset = 0x0_0000)
        {
            for (uint i=0; i<rom.Data.Length; i++)
            {
                var where = i + offset;
                var data = rom.Data[i];
                Memory[where] = data;
            }
        }

        public byte this[int index]
        {
            get
            {
                return Memory[index];
            }
            set
            {
                Memory[index] = value;
            }
        }

        public byte this[uint index]
        {
            get
            {
                return Memory[index];
            }
            set
            {
                Memory[index] = value;
            }
        }
    }
}
