using System;

namespace WSCT.Wrapper.MacOSX
{
    /// <summary>
    /// <see cref="AbstractReaderState"/> implementation for MaxOSX OS.
    /// </summary>
    sealed class ReaderState : AbstractReaderState
    {
        #region >> Properties

        /// <inheritdoc />
        public override String ReaderName
        {
            get
            {
                return ScReaderState.readerName;
            }
            set
            {
                ScReaderState.readerName = value;
            }
        }

        /// <inheritdoc />
        public override EventState CurrentState
        {
            get
            {
                return (EventState)ScReaderState.currentState;
            }
            set
            {
                ScReaderState.currentState = (uint)value;
            }
        }

        /// <inheritdoc />
        public override EventState EventState
        {
            get
            {
                return (EventState)ScReaderState.eventState;
            }
            set
            {
                ScReaderState.eventState = (uint)value;
            }
        }

        /// <inheritdoc />
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
            set
            {
                ScReaderState.atr = value;
            }
        }

        public ScardReaderState ScReaderState;

        #endregion

        #region >> Constructors

        public ReaderState()
        {
            ScReaderState = new ScardReaderState();
        }

        public ReaderState(String readerName)
            : this()
        {
            ReaderName = readerName;
        }

        public ReaderState(String readerName, EventState currentState, EventState eventState)
            : this()
        {
            ReaderName = readerName;
            CurrentState = currentState;
            EventState = eventState;
        }

        #endregion
    }
}
