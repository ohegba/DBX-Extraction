using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace dbXtract
{
    class CString
    {
        String stringO;

        public static String ReadNullTerminatedString(BinaryReader b)
        {
            StringBuilder s = new StringBuilder();
            
            Char c = '!';
            Byte bb;

            do
            {
                s.Append((bb = b.ReadByte()));
            }
            while (bb != 0x00);

            return s.ToString();
        }

    }
}
