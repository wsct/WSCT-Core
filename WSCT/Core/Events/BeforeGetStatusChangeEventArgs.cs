using System;
using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class BeforeGetStatusChangeEventArgs : EventArgs
    {
        public uint TimeOut;

        public AbstractReaderState[] ReaderStates;
    }
}