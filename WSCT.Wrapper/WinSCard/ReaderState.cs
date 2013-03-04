using System;

namespace WSCT.Wrapper.WinSCard
{
    class ReaderState : AbstractReaderState
    {
        #region >> Properties

        /// <summary>
        /// Name of the reader being monitored.
        /// Set the value of this member to "\\\\?PnP?\\Notification" and the values of all other members to zero to be notified of the arrival of a new smart card reader.
        /// </summary>
        public override String readerName
        {
            get
            {
                return scReaderState.readerName;
            }
            set
            {
                scReaderState.readerName = value;
            }
        }

        /// <summary>
        /// Current state of the reader, as seen by the application.
        /// This field can take on any of EventState values, in combination, as a bitmask.
        /// </summary>
        public override EventState currentState
        {
            get
            {
                return (EventState)scReaderState.currentState;
            }
            set
            {
                scReaderState.currentState = (uint)value;
            }
        }

        /// <summary>
        /// Current state of the reader, as known by the smart card resource manager.
        /// This field can take on any of EventState values, in combination, as a bitmask.
        /// </summary>
        public override EventState eventState
        {
            get
            {
                return (EventState)scReaderState.eventState;
            }
            set
            {
                scReaderState.eventState = (uint)value;
            }
        }

        /// <summary>Number of bytes in the returned ATR.</summary>
        public override byte[] atr
        {
            get
            {
                if ((scReaderState.atr != null) && (scReaderState.atr.Length > scReaderState.atrSize))
                    Array.Resize<Byte>(ref scReaderState.atr, (int)scReaderState.atrSize);
                return scReaderState.atr;
            }
            set
            {
                scReaderState.atr = value;
            }
        }

        public SCARD_READERSTATE scReaderState;

        #endregion

        #region >> Constructors

        public ReaderState()
            : base()
        {
            scReaderState = new SCARD_READERSTATE();
        }

        public ReaderState(String readerName)
            : this()
        {
            this.readerName = readerName;
        }

        public ReaderState(String readerName, EventState currentState, EventState eventState)
            : this()
        {
            this.readerName = readerName;
            this.currentState = currentState;
            this.eventState = eventState;
        }

        #endregion
    }
}
