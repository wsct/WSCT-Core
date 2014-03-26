using System;
using System.Collections.Generic;
using System.Text;

using WSCT;
using WSCT.Wrapper;
using WSCT.Core;
using WSCT.Core.APDU;
using WSCT.ISO7816;
using WSCT.ISO7816.Commands;
using WSCT.Stack;

namespace WSCT.Core.ConsoleTests
{
    class CardChannelLayer61xx : ICardChannelLayer, ICardChannelObservable
    {

        #region >> Properties

        ICardChannelStack _stack;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardChannelLayer61xx()
        {
        }

        #endregion

        #region >> ICardChannelLayer Membres

        /// <inheritdoc />
        public void SetStack(ICardChannelStack stack)
        {
            _stack = stack;
        }

        #endregion

        #region >> ICardChannel Membres

        public Protocol protocol
        {
            get { return _stack.RequestLayer(this, SearchMode.Next).protocol; }
        }

        public string readerName
        {
            get { return _stack.RequestLayer(this, SearchMode.Next).readerName; }
        }

        public void attach(ICardContext context, string readerName)
        {
            _stack.RequestLayer(this, SearchMode.Next).attach(context, readerName);
        }

        public ErrorCode connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            if (beforeConnectEvent != null) beforeConnectEvent(this, shareMode, preferedProtocol);

            ErrorCode ret = _stack.RequestLayer(this, SearchMode.Next).connect(shareMode, preferedProtocol);

            if (afterConnectEvent != null) afterConnectEvent(this, shareMode, preferedProtocol, ret);

            return ret;
        }

        public ErrorCode disconnect(Disposition disposition)
        {
            if (beforeDisconnectEvent != null) beforeDisconnectEvent(this, disposition);

            ErrorCode ret = _stack.RequestLayer(this, SearchMode.Next).disconnect(disposition);

            if (afterDisconnectEvent != null) afterDisconnectEvent(this, disposition, ret);

            return ret;
        }

        public ErrorCode getAttrib(Attrib attrib, ref byte[] buffer)
        {
            if (beforeGetAttribEvent != null) beforeGetAttribEvent(this, attrib, buffer);

            ErrorCode ret = _stack.RequestLayer(this, SearchMode.Next).getAttrib(attrib, ref buffer);

            if (afterGetAttribEvent != null) afterGetAttribEvent(this, attrib, buffer, ret);

            return ret;
        }

        public State getStatus()
        {
            if (beforeGetStatusEvent != null) beforeGetStatusEvent(this);

            State ret = _stack.RequestLayer(this, SearchMode.Next).getStatus();

            if (afterGetStatusEvent != null) afterGetStatusEvent(this, ret);

            return ret;
        }

        public ErrorCode reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            if (beforeReconnectEvent != null) beforeReconnectEvent(this, shareMode, preferedProtocol, initialization);

            ErrorCode ret = _stack.RequestLayer(this, SearchMode.Next).reconnect(shareMode, preferedProtocol, initialization);

            if (afterReconnectEvent != null) afterReconnectEvent(this, shareMode, preferedProtocol, initialization, ret);

            return ret;
        }

        public ErrorCode transmit(ICardCommand command, ICardResponse response)
        {
            if (beforeTransmitEvent != null) beforeTransmitEvent(this, command, response);

            ErrorCode ret;
            ISO7816.CommandAPDU cAPDU = (ISO7816.CommandAPDU)command;
            if ((cAPDU.ins == 0xA4) && cAPDU.isCC4)
            {
                UInt32 le = cAPDU.le;
                cAPDU.hasLe = false;
                // As an example, direct use of the layer to transmit the command
                ret = _stack.RequestLayer(this, SearchMode.Next).transmit(command, response);
                ResponseAPDU rAPDU = (ResponseAPDU)response;
                if ((ret == ErrorCode.Success) && (rAPDU.sw1 == 0x61))
                {
                    if (le > rAPDU.sw2)
                        le = rAPDU.sw2;
                    // As an example, use of a CommandResponsePair object to manage the dialog
                    CommandResponsePair crpGetResponse = new CommandResponsePair(new GetResponseCommand(le));
                    crpGetResponse.rAPDU = rAPDU;
                    ret = crpGetResponse.transmit(_stack.RequestLayer(this, SearchMode.Next));
                }
            }
            else
            {
                ret = _stack.RequestLayer(this, SearchMode.Next).transmit(command, response);
            }

            if (afterTransmitEvent != null) afterTransmitEvent(this, command, response, ret);

            return ret;
        }

        #endregion

        #region >> ICardChannelObservable Membres

        public event beforeConnect beforeConnectEvent;

        public event afterConnect afterConnectEvent;

        public event beforeDisconnect beforeDisconnectEvent;

        public event afterDisconnect afterDisconnectEvent;

        public event beforeGetAttrib beforeGetAttribEvent;

        public event afterGetAttrib afterGetAttribEvent;

        public event beforeGetStatus beforeGetStatusEvent;

        public event afterGetStatus afterGetStatusEvent;

        public event beforeReconnect beforeReconnectEvent;

        public event afterReconnect afterReconnectEvent;

        public event beforeTransmit beforeTransmitEvent;

        public event afterTransmit afterTransmitEvent;

        #endregion
    }
}