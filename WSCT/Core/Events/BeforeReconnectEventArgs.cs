using System;
using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class BeforeReconnectEventArgs : EventArgs
    {
        public ShareMode ShareMode;

        public Protocol PreferedProtocol;

        public Disposition Initialization;
    }
}