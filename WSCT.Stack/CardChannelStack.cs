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
            throw new Exception("CardChannelStack: layer not descriptionFound in the stack");
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
        public Protocol protocol
        {
            get { return RequestLayer(null, SearchMode.Top).protocol; }
        }

        /// <inheritdoc />
        public string readerName
        {
            get { return RequestLayer(null, SearchMode.Top).readerName; }
        }

        /// <inheritdoc />
        public void attach(ICardContext context, string readerName)
        {
            RequestLayer(null, SearchMode.Top).attach(context, readerName);
        }

        /// <inheritdoc />
        public ErrorCode connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            return RequestLayer(null, SearchMode.Top).connect(shareMode, preferedProtocol);
        }

        /// <inheritdoc />
        public ErrorCode disconnect(Disposition disposition)
        {
            return RequestLayer(null, SearchMode.Top).disconnect(disposition);
        }

        /// <inheritdoc />
        public ErrorCode getAttrib(Attrib attrib, ref byte[] buffer)
        {
            return RequestLayer(null, SearchMode.Top).getAttrib(attrib, ref buffer);
        }

        /// <inheritdoc />
        public State getStatus()
        {
            return RequestLayer(null, SearchMode.Top).getStatus();
        }

        /// <inheritdoc />
        public ErrorCode reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            return RequestLayer(null, SearchMode.Top).reconnect(shareMode, preferedProtocol, initialization);
        }

        /// <inheritdoc />
        public ErrorCode transmit(ICardCommand command, ICardResponse response)
        {
            return RequestLayer(null, SearchMode.Top).transmit(command, response);
        }

        #endregion
    }
}