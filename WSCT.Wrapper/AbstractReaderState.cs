using System;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Abstract ReaderState structure to be implemented by PC/SC wrapper.
    /// </summary>
    public abstract class AbstractReaderState
    {
        /// <summary>
        /// Number of bytes in the returned ATR.
        /// </summary>
        public abstract byte[] Atr { get; set; }

        /// <summary>
        /// Current state of the reader, as seen by the application.
        /// This field can take on any of EventState values, in combination, as a bitmask.
        /// </summary>
        public abstract EventState CurrentState { get; set; }

        /// <summary>
        /// Current state of the reader, as known by the smart card resource manager.
        /// This field can take on any of EventState values, in combination, as a bitmask.
        /// </summary>
        public abstract EventState EventState { get; set; }

        /// <summary>
        /// Name of the reader being monitored.
        /// Set the value of this member to "\\\\?PnP?\\Notification" and the values of all other members to zero to be notified of the arrival of a new smart card reader.
        /// </summary>
        public abstract string ReaderName { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var sEvent = new StringBuilder();
            if ((EventState & EventState.StateChanged) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.StateChanged);
            }
            if ((EventState & EventState.StateEmpty) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.StateEmpty);
            }
            if ((EventState & EventState.StateExclusive) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.StateExclusive);
            }
            if ((EventState & EventState.StateIgnore) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.StateIgnore);
            }
            if ((EventState & EventState.StateInuse) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.StateInuse);
            }
            if ((EventState & EventState.StateMute) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.StateMute);
            }
            if ((EventState & EventState.StatePresent) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.StatePresent);
            }
            if ((EventState & EventState.Unavailable) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.Unavailable);
            }
            if ((EventState & EventState.StateUnknown) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.StateUnknown);
            }
            if ((EventState & EventState.StateUnpowered) != 0)
            {
                sEvent.AppendFormat("{0}{1}", (sEvent.Length == 0 ? "" : " "), EventState.StateUnpowered);
            }
            return String.Format("Events:{0} on reader {1}", sEvent, ReaderName);
        }
    }
}
