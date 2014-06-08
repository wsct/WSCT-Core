using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class AfterReconnectEventArgs : AfterEventArgs
    {
        public ShareMode ShareMode;

        public Protocol PreferedProtocol;

        public Disposition Initialization;
    }
}