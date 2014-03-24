using System;
using WSCT.Wrapper;
using WSCT.Core.APDU;

namespace WSCT.Core
{
    /// <summary>
    /// Represents a basic object capable of managing access to a smartcard.
    /// </summary>
    public class CardChannelCore : ICardChannel
    {
        #region >> Fields

        private ICardContext _context;

        private String _readerName;

        private IntPtr _card;

        private Protocol _protocol;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardChannelCore()
        {
        }

        /// <summary>
        /// Constructor (<seealso cref="attach"/>)
        /// </summary>
        /// <param name="context">Resource manager context to attach</param>
        /// <param name="readerName">Name of the reader to use</param>
        public CardChannelCore(ICardContext context, String readerName)
            : this()
        {
            attach(context, readerName);
        }

        #endregion

        #region >> ICardChannel Members

        /// <inheritdoc />
        public Protocol protocol
        {
            get
            {
                return _protocol;
            }
        }

        /// <inheritdoc />
        public String readerName
        {
            get
            {
                return _readerName;
            }
        }

        /// <inheritdoc />
        public virtual void attach(ICardContext context, String readerName)
        {
            _context = context;
            _readerName = readerName;
            _card = IntPtr.Zero;
            _protocol = Protocol.T0;
        }

        /// <inheritdoc />
        public virtual ErrorCode connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            _protocol = preferedProtocol;
            var ret = Primitives.Api.SCardConnect(_context.context, _readerName, shareMode, preferedProtocol, ref _card, ref _protocol);
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode disconnect(Disposition disposition)
        {
            var ret = Primitives.Api.SCardDisconnect(_card, disposition);
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode getAttrib(Attrib attrib, ref Byte[] buffer)
        {
            var bufferSize = Primitives.Api.AutoAllocate;
            var ret = Primitives.Api.SCardGetAttrib(_card, (uint)attrib, ref buffer, ref bufferSize);
            return ret;
        }

        /// <inheritdoc />
        public virtual State getStatus()
        {
            String readerName = null;
            var state = new State();
            var protocol = new Protocol();
            Byte[] atr = null;
            Primitives.Api.SCardStatus(_card, ref readerName, ref state, ref protocol, ref atr);
            return state;
        }

        /// <inheritdoc />
        public virtual ErrorCode reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            _protocol = preferedProtocol;
            var ret = Primitives.Api.SCardReconnect(_card, shareMode, preferedProtocol, initialization, ref _protocol);
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode transmit(ICardCommand command, ICardResponse response)
        {
            var recvSize = Primitives.Api.AutoAllocate;
            Byte[] recvBuffer = null;
            var ret = __transmit(command, ref recvBuffer, ref recvSize);
            response.parse(recvBuffer, recvSize);
            return ret;
        }

        #endregion

        #region >> Members

        ErrorCode __transmit(ICardCommand command, ref Byte[] recvBuffer, ref UInt32 recvSize)
        {
            var sendPci = Primitives.Api.CreateIoRequestInstance(_protocol);
            var recvPci = Primitives.Api.CreateIoRequestInstance(_protocol);

            var ret = Primitives.Api.SCardTransmit(
                _card,
                ref sendPci,
                command.binaryCommand,
                (UInt32)command.binaryCommand.Length,
                ref recvPci,
                ref recvBuffer,
                ref recvSize
                );

            return ret;
        }

        #endregion
    }
}