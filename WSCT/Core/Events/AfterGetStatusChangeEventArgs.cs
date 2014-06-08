using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class AfterGetStatusChangeEventArgs : AfterEstablishEventArgs
    {
        public uint TimeOut;

        public AbstractReaderState[] ReaderStates;
    }
}