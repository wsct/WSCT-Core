using System;
using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class BeforeGetAttribEventArgs : EventArgs
    {
        public Attrib Attrib;

        public byte[] Buffer;
    }
}