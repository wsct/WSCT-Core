using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class AfterConnectEventArgs : AfterEventArgs
    {
        public ShareMode ShareMode;

        public Protocol PreferedProtocol;
    }
}