using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuvotonIcpTool
{
    public class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern uint GetShortPathName(
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)]
        string lpszLongPath,
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)]
        System.Text.StringBuilder lpszShortPath,
            uint cchBuffer);
    }
}
