using System;
using System.Collections.Generic;
using WSCT.Core;
using WSCT.Core.APDU;
using WSCT.Wrapper;

namespace WSCT.Stack
{
    /// <summary>
    /// Reference implementation of <see cref="ICardChannelStack"/>.
    /// </summary>
    public class CardChannelStack : ICardChannelStack
    {
        #region >> Attributes

        private readonly List<ICardChannelLayer> _layers;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CardChannelStack()
        {
            _layers = new List<ICardChannelLayer>();
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// Returns index of a <see cref="ICardChannelLayer"/> instance in the stack.
        /// </summary>
        /// <param name="layer">Layer instance to find.</param>
        /// <returns>The index of the layer in the stack.</returns>
        private int GetIndex(ICardChannelLayer layer)
        {
            for (var index = 0; index < _layers.Count; index++)
            {
                if (_layers[index] == layer)
                {
                    return index;
                }
            }
            throw new Exception("CardChannelStack: layer not found in the stack");
        }

        #endregion

        #region >> ICardChannelStack Membres

        /// <inheritdoc />
        public List<ICardChannelLayer> Layers
        {
            get { return _layers; }
        }

        /// <inheritdoc />
        public void AddLayer(ICardChannelLayer layer)
        {
            _layers.Add(layer);
            layer.SetStack(this);
        }

        /// <inheritdoc />
        public void ReleaseLayer(ICardChannelLayer layer)
        {
            _layers.Remove(layer);
        }

        /// <inheritdoc />
        public ICardChannelLayer RequestLayer(ICardChannelLayer layer, SearchMode mode)
        {
            ICardChannelLayer newLayer;
            int index;
            if (_layers.Count == 0)
            {
                throw new Exception("CardChannelStack.requestLayer(): no layers defined in the stack");
            }
            switch (mode)
            {
                case SearchMode.Bottom:
                    newLayer = _layers[_layers.Count - 1];
                    break;
                case SearchMode.Next:
                    index = GetIndex(layer);
                    if (index >= _layers.Count)
                    {
                        throw new Exception("CardChannelStack.requestLayer(): Seek next failed");
                    }
                    newLayer = _layers[index + 1];
                    break;
                case SearchMode.Previous:
                    index = GetIndex(layer);
                    if (index <= 0)
                    {
                        throw new Exception("CardChannelStack.requestLayer(): Seek previous failed");
                    }
                    newLayer = _layers[index - 1];
                    break;
                case SearchMode.Top:
                    newLayer = _layers[0];
                    break;
                default:
                    throw new NotSupportedException(String.Format("CardChannelStack.requestLayer(): Seek mode '{0}' unknown", mode));
            }
            return newLayer;
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