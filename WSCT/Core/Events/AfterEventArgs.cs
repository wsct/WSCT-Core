using System;
using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class AfterEventArgs : EventArgs
    {
        public ErrorCode ReturnValue;
    }
}