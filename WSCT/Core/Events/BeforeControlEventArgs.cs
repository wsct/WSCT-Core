using System;

namespace WSCT.Core.Events;

public class BeforeControlEventArgs : EventArgs
{
    public uint ControlCode;

    public byte[] Command;
}