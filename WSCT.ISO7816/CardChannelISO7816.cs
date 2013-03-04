using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Core;

namespace WSCT.ISO7816
{
    /// <summary>
    /// 
    /// </summary>
    public class CardChannelISO7816 : ICardChannel
    {
        ICardChannel _cardChannel;
        ProtocolT _protocolT;

        #region >> Construtors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardChannel"></param>
        public CardChannelISO7816(ICardChannel cardChannel)
            : this(cardChannel, ProtocolT.T_0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardChannel"></param>
        /// <param name="protocolT"></param>
        public CardChannelISO7816(ICardChannel cardChannel, ProtocolT protocolT)
        {
            _cardChannel = cardChannel;
            _protocolT = protocolT;
        }

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        public ProtocolT protocolT
        {
            get { return _protocolT; }
            set { _protocolT = value; }
        }

        #endregion

        #region >> ICardChannel Membres

        /// <inheritdoc/>
        public Wrapper.Protocol protocol
        {
            get { return _cardChannel.protocol; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string readerName
        {
            get { return _cardChannel.readerName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void attach(ICardContext context, string readerName)
        {
            _cardChannel.attach(context, readerName);
        }

        /// <summary>
        /// 
        /// </summary>
        public Wrapper.ErrorCode connect(Wrapper.ShareMode shareMode, Wrapper.Protocol preferedProtocol)
        {
            return _cardChannel.connect(shareMode, preferedProtocol);
        }

        /// <summary>
        /// 
        /// </summary>
        public Wrapper.ErrorCode disconnect(Wrapper.Disposition disposition)
        {
            return _cardChannel.disconnect(disposition);
        }

        /// <summary>
        /// 
        /// </summary>
        public Wrapper.ErrorCode getAttrib(Wrapper.Attrib attrib, ref byte[] buffer)
        {
            return _cardChannel.getAttrib(attrib, ref buffer);
        }

        /// <summary>
        /// 
        /// </summary>
        public Wrapper.State getStatus()
        {
            return _cardChannel.getStatus();
        }

        /// <summary>
        /// 
        /// </summary>
        public Wrapper.ErrorCode reconnect(Wrapper.ShareMode shareMode, Wrapper.Protocol preferedProtocol, Wrapper.Disposition initialization)
        {
            return _cardChannel.reconnect(shareMode, preferedProtocol, initialization);
        }

        /// <summary>
        /// 
        /// </summary>
        public Wrapper.ErrorCode transmit(Core.APDU.ICardCommand command, Core.APDU.ICardResponse response)
        {
            Wrapper.ErrorCode errorCode = _cardChannel.transmit(command, response);
            return errorCode;
        }

        #endregion
    }
}