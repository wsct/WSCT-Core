using System;
using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class BeforeDisconnectEventArgs : EventArgs
    {
        public Disposition Disposition;
    }
}