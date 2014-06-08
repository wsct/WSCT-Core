using WSCT.Core.APDU;

namespace WSCT.Core.Events
{
    public class AfterTransmitEventArgs : AfterEventArgs
    {
        public ICardCommand Command;

        public ICardResponse Response;
    }
}