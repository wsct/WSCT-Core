using System;

namespace WSCT.Wrapper.PCSCLite32
{
    class ReaderState : AbstractReaderState
    {
        #region >> Properties

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

        public override byte[] atr
        {
            get
            {
                if ((scReaderState.atr != null) && (scReaderState.atr.Length > (uint)scReaderState.atrSize))
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
