using System;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Abstract ReaderState structure to be implemented by PC/SC wrapper s
    /// </summary>
    public abstract class AbstractReaderState
    {
        /// <summary>
        /// Number of bytes in the returned ATR.
        /// </summary>
        public abstract byte[] atr { get; set; }

        /// <summary>
        /// Current state of the reader, as seen by the application.
        /// This field can take on any of EventState values, in combination, as a bitmask.
        /// </summary>
        public abstract EventState currentState { get; set; }

        /// <summary>
        /// Current state of the reader, as known by the smart card resource manager.
        /// This field can take on any of EventState values, in combination, as a bitmask.
        /// </summary>
        public abstract EventState eventState { get; set; }

        /// <summary>
        /// Name of the reader being monitored.
        /// Set the value of this member to "\\\\?PnP?\\Notification" and the values of all other members to zero to be notified of the arrival of a new smart card reader.
        /// </summary>
        public abstract string readerName { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder sEvent = new StringBuilder();
            if ((eventState & EventState.SCARD_STATE_CHANGED) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_CHANGED);
            if ((eventState & EventState.SCARD_STATE_EMPTY) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_EMPTY);
            if ((eventState & EventState.SCARD_STATE_EXCLUSIVE) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_EXCLUSIVE);
            if ((eventState & EventState.SCARD_STATE_IGNORE) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_IGNORE);
            if ((eventState & EventState.SCARD_STATE_INUSE) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_INUSE);
            if ((eventState & EventState.SCARD_STATE_MUTE) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_MUTE);
            if ((eventState & EventState.SCARD_STATE_PRESENT) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_PRESENT);
            if ((eventState & EventState.SCARD_STATE_UNAVAILABLE) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_UNAVAILABLE);
            if ((eventState & EventState.SCARD_STATE_UNKNOWN) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_UNKNOWN);
            if ((eventState & EventState.SCARD_STATE_UNPOWERED) != 0)
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.SCARD_STATE_UNPOWERED);
            return String.Format("Events:{0} on reader {1}", sEvent, readerName); ;
        }
    }
}
