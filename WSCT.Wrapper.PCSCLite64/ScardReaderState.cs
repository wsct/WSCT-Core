﻿using System;
using System.Runtime.InteropServices;

namespace WSCT.Wrapper.PCSCLite64
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct ScardReaderState
    {
        [MarshalAs(UnmanagedType.LPTStr)]
        public string readerName;

        public IntPtr userData;
        public UInt64 currentState;
        public UInt64 eventState;
        public UInt64 atrSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x21)]
        public byte[] atr;
    }
}