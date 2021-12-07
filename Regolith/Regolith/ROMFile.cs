using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Regolith
{
    public class ROMFile
    {
        // 16KB ROMs
        public static readonly long MAX_SIZE = 2^20;

        // ASCII 'ROM16'
        public static readonly byte[] Magic = new byte[5]
            { 0x52, 0x4F, 0x4D, 0x31, 0x36 };

        public byte[] Data = Array.Empty<byte>();

        public void LoadFile(string path)
        {
            var fl = File.OpenRead(path);
            fl.Seek(0, SeekOrigin.Begin);
            byte[] initial = new byte[Magic.Length];
            fl.Read(initial, 0, Magic.Length);
            if (initial != Magic)
            {
                throw new Exception("Bad ROM file! Mismatched magic.");
            }
            byte[] d = new byte[MAX_SIZE];
            var read = fl.Read(d, 0, (int)MAX_SIZE);
            Data = new byte[read];
            d.CopyTo(Data, 0);
        }
    }
}
