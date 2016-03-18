using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pavic
{
    class MemoryCalculator
    {
        public static long KiloBytes(int KiloBytes) => KiloBytes * 1024;
        public static long MegaBytes(int MegaBytes) => KiloBytes(MegaBytes) * 1024;
        public static long GigaBytes(int GigaBytes) => MegaBytes(GigaBytes) * 1024;
    }
}
