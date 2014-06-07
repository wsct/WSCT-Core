using System;
using System.Collections.Generic;
using WSCT.Core;
using WSCT.Core.APDU;
using WSCT.Helpers.Linq;
using WSCT.Wrapper;

namespace WSCT.Stack
{
    /// <summary>
    ///     Reference implementation of <see cref="ICardChannelStack" />.
    /// </summary>
    public class CardChannelStack : ICardChannelStack
    {
        #region >> Attributes

        private readonly List<ICardChannelLayer> layers;

        #endregion

        #region >> Constructors

        /// <summary>
        ///     Initializes a new instance.
        /// </summary>
        public CardChannelStack()
        {
            layers = new List<ICardChannelLayer>();
        }

        #endregion

        #region >> ICardChannelStack Membres

        /// <inheritdoc />
        public List<ICardChannelLayer> Layers
        {
            get { return layers; }
        }

        /// <inheritdoc />
        public void AddLayer(ICardChannelLayer layer)
        {
            layers.Add(layer);
            layer.SetStack(this);
        }

        /// <inheritdoc />
        public void ReleaseLayer(ICardChannelLayer layer)
        {
            layers.Remove(layer);
        }

        /// <inheritdoc />
        public ICardChannelLayer RequestLayer(ICardChannelLayer layer, SearchMode mode)
        {
            if (layers.Count == 0)
            {
                throw new Exception("CardChannelStack.requestLayer(): no layers defined in the stack");
            }
            switch (mode)
            {
                case SearchMode.Bottom:
                    return layers[layers.Count - 1];
                case SearchMode.Next:
                    return layers.Following(l => l == layer);
                case SearchMode.Previous:
                    return layers.Preceding(l => l == layer);
                case SearchMode.Top:
                    return layers[0];
                default:
                    throw new NotSupportedException(String.Format("CardChannelStack.RequestLayer(): Seek mode '{0}' unknown", mode));
            }
        }

        #endregion

        #region >> ICardChannel Membres

        /// <inheritdoc />
        public Protocol Protocol
        {
            get { return RequestLayer(null, SearchMode.Top).Protocol; }
        }

        /// <inheritdoc />
        public string ReaderName
        {
            get { return RequestLayer(null, SearchMode.Top).ReaderName; }
        }

        /// <inheritdoc />
        public void Attach(ICardContext context, string readerName)
        {
            RequestLayer(null, SearchMode.Top).Attach(context, readerName);
        }

        /// <inheritdoc />
        public ErrorCode Connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            return RequestLayer(null, SearchMode.Top).Connect(shareMode, preferedProtocol);
        }

        /// <inheritdoc />
        public ErrorCode Disconnect(Disposition disposition)
        {
            return RequestLayer(null, SearchMode.Top).Disconnect(disposition);
        }

        /// <inheritdoc />
        public ErrorCode GetAttrib(Attrib attrib, ref byte[] buffer)
        {
            return RequestLayer(null, SearchMode.Top).GetAttrib(attrib, ref buffer);
        }

        /// <inheritdoc />
        public State GetStatus()
        {
            return RequestLayer(null, SearchMode.Top).GetStatus();
        }

        /// <inheritdoc />
        public ErrorCode Reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            return RequestLayer(null, SearchMode.Top).Reconnect(shareMode, preferedProtocol, initialization);
        }

        /// <inheritdoc />
        public ErrorCode Transmit(ICardCommand command, ICardResponse response)
        {
            return RequestLayer(null, SearchMode.Top).Transmit(command, response);
        }

        #endregion
    }
}