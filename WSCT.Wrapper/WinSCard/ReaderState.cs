using System;

namespace WSCT.Wrapper.WinSCard
{
    internal sealed class ReaderState : AbstractReaderState
    {
        #region >> Properties

        public ScardReaderState ScReaderState;

        /// <summary>
        /// Name of the reader being monitored.
        /// Set the value of this member to "\\\\?PnP?\\Notification" and the values of all other members to zero to be notified of the arrival of a new smart card reader.
        /// </summary>
        public override string ReaderName
        {
            get { return ScReaderState.readerName; }
            set { ScReaderState.readerName = value; }
        }

        /// <summary>
        /// Current state of the reader, as seen by the application.
        /// This field can take on any of EventState values, in combination, as a bitmask.
        /// </summary>
        public override EventState CurrentState
        {
            get { return (EventState)ScReaderState.currentState; }
            set { ScReaderState.currentState = (uint)value; }
        }

        /// <summary>
        /// Current state of the reader, as known by the smart card resource manager.
        /// This field can take on any of EventState values, in combination, as a bitmask.
        /// </summary>
        public override EventState EventState
        {
            get { return (EventState)ScReaderState.eventState; }
            set { ScReaderState.eventState = (uint)value; }
        }

        /// <summary>Number of bytes in the returned ATR.</summary>
        public override byte[] Atr
        {
            get
            {
                if ((ScReaderState.atr != null) && (ScReaderState.atr.Length > ScReaderState.atrSize))
                {
                    Array.Resize(ref ScReaderState.atr, (int)ScReaderState.atrSize);
                }
                return ScReaderState.atr;
            }
            set { ScReaderState.atr = value; }
        }

        #endregion

        #region >> Constructors

        public ReaderState()
        {
            ScReaderState = new ScardReaderState();
        }

        public ReaderState(string readerName)
            : this()
        {
            ReaderName = readerName;
        }

        public ReaderState(string readerName, EventState currentState, EventState eventState)
            : this()
        {
            ReaderName = readerName;
            CurrentState = currentState;
            EventState = eventState;
        }

        #endregion
    }
}