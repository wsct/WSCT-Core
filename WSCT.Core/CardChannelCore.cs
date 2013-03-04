using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Permissions;

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

        private Core.ICardContext _context;

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
        public CardChannelCore(Core.ICardContext context, String readerName)
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
        public virtual void attach(Core.ICardContext context, String readerName)
        {
            this._context = context;
            this._readerName = readerName;
            this._card = IntPtr.Zero;
            this._protocol = Protocol.SCARD_PROTOCOL_T0;
        }

        /// <inheritdoc />
        public virtual ErrorCode connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            this._protocol = preferedProtocol;
            ErrorCode ret = Primitives.api.SCardConnect(this._context.context, this._readerName, shareMode, preferedProtocol, ref this._card, ref this._protocol);
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode disconnect(Disposition disposition)
        {
            ErrorCode ret = Primitives.api.SCardDisconnect(_card, disposition);
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode getAttrib(Attrib attrib, ref Byte[] buffer)
        {
            UInt32 bufferSize = Primitives.api.SCARD_AUTOALLOCATE;
            ErrorCode ret = Primitives.api.SCardGetAttrib(this._card, (uint)attrib, ref buffer, ref bufferSize);
            return ret;
        }

        /// <inheritdoc />
        public virtual State getStatus()
        {
            String readerName = null;
            State state = new State();
            Protocol protocol = new Protocol();
            Byte[] atr = null;
            Primitives.api.SCardStatus(this._card, ref readerName, ref state, ref protocol, ref atr);
            return state;
        }

        /// <inheritdoc />
        public virtual ErrorCode reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            this._protocol = preferedProtocol;
            ErrorCode ret = Primitives.api.SCardReconnect(_card, shareMode, preferedProtocol, initialization, ref this._protocol);
            return ret;
        }

        /// <inheritdoc />
        public virtual ErrorCode transmit(ICardCommand command, ICardResponse response)
        {
            UInt32 recvSize = Primitives.api.SCARD_AUTOALLOCATE;
            Byte[] recvBuffer = null;
            ErrorCode ret = __transmit(command, ref recvBuffer, ref recvSize);
            response.parse(recvBuffer, recvSize);
            return ret;
        }

        #endregion

        #region >> Members

        ErrorCode __transmit(ICardCommand command, ref Byte[] recvBuffer, ref UInt32 recvSize)
        {
            AbstractIoRequest sendPci = Primitives.api.createIoRequestInstance(this._protocol);
            AbstractIoRequest recvPci = Primitives.api.createIoRequestInstance(this._protocol);

            ErrorCode ret = Primitives.api.SCardTransmit(
                this._card,
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