using System;
using WSCT.Core;
using WSCT.Core.APDU;

namespace WSCT.Wrapper.Desktop.Core
{
    /// <summary>
    /// Represents a basic object capable of managing access to a smartcard.
    /// </summary>
    public class CardChannelCore : ICardChannel
    {
        #region >> Fields

        private IntPtr _card;
        private ICardContext _context;

        private Protocol _protocol;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CardChannelCore()
        {
        }

        /// <summary>
        /// Constructor (<seealso cref="Attach"/>).
        /// </summary>
        /// <param name="context">Resource manager context to attach.</param>
        /// <param name="readerName">Name of the reader to use.</param>
        public CardChannelCore(ICardContext context, string readerName)
            : this()
        {
            Attach(context, readerName);
        }

        #endregion

        #region >> ICardChannel Members

        /// <inheritdoc />
        public Protocol Protocol
        {
            get { return _protocol; }
        }

        /// <inheritdoc />
        public string ReaderName { get; private set; }

        /// <inheritdoc />
        public virtual void Attach(ICardContext context, string readerName)
        {
            _context = context;
            ReaderName = readerName;
            _card = IntPtr.Zero;
            _protocol = Protocol.T0;
        }

        /// <inheritdoc />
        public virtual ErrorCode Connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            _protocol = preferedProtocol;
            var ret = Primitives.Api.SCardConnect(_context.Context, ReaderName, shareMode, preferedProtocol, ref _card, ref _protocol);
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode Disconnect(Disposition disposition)
        {
            var ret = Primitives.Api.SCardDisconnect(_card, disposition);
            _card = IntPtr.Zero;
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode GetAttrib(Attrib attrib, ref byte[] buffer)
        {
            var bufferSize = Primitives.Api.AutoAllocate;
            var ret = Primitives.Api.SCardGetAttrib(_card, (uint)attrib, ref buffer, ref bufferSize);
            return ret;
        }

        /// <inheritdoc />
        public virtual State GetStatus()
        {
            string readerName = null;
            var state = new State();
            var protocol = new Protocol();
            byte[] atr = null;
            Primitives.Api.SCardStatus(_card, ref readerName, ref state, ref protocol, ref atr);
            return state;
        }

        /// <inheritdoc />
        public virtual ErrorCode Reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            _protocol = preferedProtocol;
            var ret = Primitives.Api.SCardReconnect(_card, shareMode, preferedProtocol, initialization, ref _protocol);
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode Transmit(ICardCommand command, ICardResponse response)
        {
            var recvSize = Primitives.Api.AutoAllocate;
            byte[] recvBuffer = null;
            var ret = __transmit(command, ref recvBuffer, ref recvSize);
            response.Parse(recvBuffer, recvSize);
            return ret;
        }

        #endregion

        #region >> Members

        private ErrorCode __transmit(ICardCommand command, ref byte[] recvBuffer, ref UInt32 recvSize)
        {
            var sendPci = Primitives.Api.CreateIoRequestInstance(_protocol);
            var recvPci = Primitives.Api.CreateIoRequestInstance(_protocol);

            var ret = Primitives.Api.SCardTransmit(
                _card,
                ref sendPci,
                command.BinaryCommand,
                (UInt32)command.BinaryCommand.Length,
                ref recvPci,
                ref recvBuffer,
                ref recvSize
                );

            return ret;
        }

        #endregion
    }
}