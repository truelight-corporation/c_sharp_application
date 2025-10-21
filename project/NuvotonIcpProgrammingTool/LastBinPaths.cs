using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuvotonIcpTool
{
    public class LastBinPaths
    {
        public KeyValuePair<string, string> APROMPath { get; set; }
        public KeyValuePair<string, string> LDROMPath { get; set; }
        public string DataFlashPath { get; set; }
        public bool SecurityLockState { get; set; }
    }
}
