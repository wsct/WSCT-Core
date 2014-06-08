using System;
using WSCT.Wrapper;

namespace WSCT.Core.Events
{
    public class AfterGetStatusEventArgs : EventArgs
    {
        public State State;
    }
}