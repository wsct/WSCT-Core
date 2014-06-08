using WSCT.Core.APDU;
using WSCT.ISO7816;
using WSCT.ISO7816.Commands;
using WSCT.Stack;
using WSCT.Wrapper;

namespace WSCT.Core.ConsoleTests
{
    internal class CardChannelLayer61 : ICardChannelLayer
    {
        #region >> Properties

        private ICardChannelStack stack;

        #endregion

        #region >> ICardChannelLayer Membres

        /// <inheritdoc />
        public void SetStack(ICardChannelStack stack)
        {
            this.stack = stack;
        }

        /// <inheritdoc />
        public string LayerId
        {
            get { return "61xx"; }
        }

        #endregion

        #region >> ICardChannel Membres

        public Protocol Protocol
        {
            get { return stack.RequestLayer(this, SearchMode.Next).Protocol; }
        }

        public string ReaderName
        {
            get { return stack.RequestLayer(this, SearchMode.Next).ReaderName; }
        }

        public void Attach(ICardContext context, string readerName)
        {
            stack.RequestLayer(this, SearchMode.Next).Attach(context, readerName);
        }

        public ErrorCode Connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            var ret = stack.RequestLayer(this, SearchMode.Next).Connect(shareMode, preferedProtocol);

            return ret;
        }

        public ErrorCode Disconnect(Disposition disposition)
        {
            var ret = stack.RequestLayer(this, SearchMode.Next).Disconnect(disposition);

            return ret;
        }

        public ErrorCode GetAttrib(Attrib attrib, ref byte[] buffer)
        {
            var ret = stack.RequestLayer(this, SearchMode.Next).GetAttrib(attrib, ref buffer);

            return ret;
        }

        public State GetStatus()
        {
            var state = stack.RequestLayer(this, SearchMode.Next).GetStatus();

            return state;
        }

        public ErrorCode Reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            var ret = stack.RequestLayer(this, SearchMode.Next).Reconnect(shareMode, preferedProtocol, initialization);

            return ret;
        }

        public ErrorCode Transmit(ICardCommand command, ICardResponse response)
        {
            ErrorCode ret;
            var cAPDU = (CommandAPDU)command;
            if ((cAPDU.Ins == 0xA4) && cAPDU.IsCc4)
            {
                var le = cAPDU.Le;
                cAPDU.HasLe = false;
                // As an example, direct use of the layer to transmit the command
                ret = stack.RequestLayer(this, SearchMode.Next).Transmit(command, response);
                var rAPDU = (ResponseAPDU)response;
                if ((ret == ErrorCode.Success) && (rAPDU.Sw1 == 0x61))
                {
                    if (le > rAPDU.Sw2)
                    {
                        le = rAPDU.Sw2;
                    }
                    // As an example, use of a CommandResponsePair object to manage the dialog
                    var crpGetResponse = new CommandResponsePair(new GetResponseCommand(le)) { RApdu = rAPDU };
                    ret = crpGetResponse.Transmit(stack.RequestLayer(this, SearchMode.Next));
                }
            }
            else
            {
                ret = stack.RequestLayer(this, SearchMode.Next).Transmit(command, response);
            }

            return ret;
        }

        #endregion
    }
}