using System;
using WSCT.Core.APDU;

namespace WSCT.Core.Events
{
    public class BeforeTransmitEventArgs : EventArgs
    {
        public ICardCommand Command;

        public ICardResponse Response;
    }
}