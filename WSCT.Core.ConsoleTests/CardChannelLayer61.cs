using WSCT.Core.APDU;
using WSCT.ISO7816;
using WSCT.ISO7816.Commands;
using WSCT.Stack;
using WSCT.Wrapper;

namespace WSCT.Core.ConsoleTests
{
    internal class CardChannelLayer61 : ICardChannelLayer, ICardChannelObservable
    {
        #region >> Properties

        private ICardChannelStack _stack;

        #endregion

        #region >> ICardChannelLayer Membres

        /// <inheritdoc />
        public void SetStack(ICardChannelStack stack)
        {
            _stack = stack;
        }

        #endregion

        #region >> ICardChannel Membres

        public Protocol Protocol
        {
            get { return _stack.RequestLayer(this, SearchMode.Next).Protocol; }
        }

        public string ReaderName
        {
            get { return _stack.RequestLayer(this, SearchMode.Next).ReaderName; }
        }

        public void Attach(ICardContext context, string readerName)
        {
            _stack.RequestLayer(this, SearchMode.Next).Attach(context, readerName);
        }

        public ErrorCode Connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            if (BeforeConnectEvent != null)
            {
                BeforeConnectEvent(this, shareMode, preferedProtocol);
            }

            var ret = _stack.RequestLayer(this, SearchMode.Next).Connect(shareMode, preferedProtocol);

            if (AfterConnectEvent != null)
            {
                AfterConnectEvent(this, shareMode, preferedProtocol, ret);
            }

            return ret;
        }

        public ErrorCode Disconnect(Disposition disposition)
        {
            if (BeforeDisconnectEvent != null)
            {
                BeforeDisconnectEvent(this, disposition);
            }

            var ret = _stack.RequestLayer(this, SearchMode.Next).Disconnect(disposition);

            if (AfterDisconnectEvent != null)
            {
                AfterDisconnectEvent(this, disposition, ret);
            }

            return ret;
        }

        public ErrorCode GetAttrib(Attrib attrib, ref byte[] buffer)
        {
            if (BeforeGetAttribEvent != null)
            {
                BeforeGetAttribEvent(this, attrib, buffer);
            }

            var ret = _stack.RequestLayer(this, SearchMode.Next).GetAttrib(attrib, ref buffer);

            if (AfterGetAttribEvent != null)
            {
                AfterGetAttribEvent(this, attrib, buffer, ret);
            }

            return ret;
        }

        public State GetStatus()
        {
            if (BeforeGetStatusEvent != null)
            {
                BeforeGetStatusEvent(this);
            }

            var ret = _stack.RequestLayer(this, SearchMode.Next).GetStatus();

            if (AfterGetStatusEvent != null)
            {
                AfterGetStatusEvent(this, ret);
            }

            return ret;
        }

        public ErrorCode Reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            if (BeforeReconnectEvent != null)
            {
                BeforeReconnectEvent(this, shareMode, preferedProtocol, initialization);
            }

            var ret = _stack.RequestLayer(this, SearchMode.Next).Reconnect(shareMode, preferedProtocol, initialization);

            if (AfterReconnectEvent != null)
            {
                AfterReconnectEvent(this, shareMode, preferedProtocol, initialization, ret);
            }

            return ret;
        }

        public ErrorCode Transmit(ICardCommand command, ICardResponse response)
        {
            if (BeforeTransmitEvent != null)
            {
                BeforeTransmitEvent(this, command, response);
            }

            ErrorCode ret;
            var cAPDU = (CommandAPDU)command;
            if ((cAPDU.Ins == 0xA4) && cAPDU.IsCc4)
            {
                var le = cAPDU.Le;
                cAPDU.HasLe = false;
                // As an example, direct use of the layer to transmit the command
                ret = _stack.RequestLayer(this, SearchMode.Next).Transmit(command, response);
                var rAPDU = (ResponseAPDU)response;
                if ((ret == ErrorCode.Success) && (rAPDU.Sw1 == 0x61))
                {
                    if (le > rAPDU.Sw2)
                    {
                        le = rAPDU.Sw2;
                    }
                    // As an example, use of a CommandResponsePair object to manage the dialog
                    var crpGetResponse = new CommandResponsePair(new GetResponseCommand(le)) { RApdu = rAPDU };
                    ret = crpGetResponse.Transmit(_stack.RequestLayer(this, SearchMode.Next));
                }
            }
            else
            {
                ret = _stack.RequestLayer(this, SearchMode.Next).Transmit(command, response);
            }

            if (AfterTransmitEvent != null)
            {
                AfterTransmitEvent(this, command, response, ret);
            }

            return ret;
        }

        #endregion

        #region >> ICardChannelObservable Membres

        public event BeforeConnect BeforeConnectEvent;

        public event AfterConnect AfterConnectEvent;

        public event BeforeDisconnect BeforeDisconnectEvent;

        public event AfterDisconnect AfterDisconnectEvent;

        public event BeforeGetAttrib BeforeGetAttribEvent;

        public event AfterGetAttrib AfterGetAttribEvent;

        public event BeforeGetStatus BeforeGetStatusEvent;

        public event AfterGetStatus AfterGetStatusEvent;

        public event BeforeReconnect BeforeReconnectEvent;

        public event AfterReconnect AfterReconnectEvent;

        public event BeforeTransmit BeforeTransmitEvent;

        public event AfterTransmit AfterTransmitEvent;

        #endregion
    }
}