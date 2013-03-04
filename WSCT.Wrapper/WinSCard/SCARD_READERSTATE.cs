using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.WinSCard
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct SCARD_READERSTATE
    {
        [MarshalAs(UnmanagedType.LPTStr)]
        public String readerName;
        public IntPtr userData;
        public UInt32 currentState;
        public UInt32 eventState;
        public UInt32 atrSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x24)]
        public byte[] atr;
    }
}