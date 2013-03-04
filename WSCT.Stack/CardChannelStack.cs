using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Wrapper;
using WSCT.Core.APDU;
using WSCT.Core;

namespace WSCT.Stack
{
    /// <summary>
    /// Represents a layerDescriptions of <see cref="ICardChannelLayer"/>s.
    /// </summary>
    public class CardChannelStack : ICardChannelStack
    {
        #region >> Attributes

        List<ICardChannelLayer> _layers;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardChannelStack()
        {
            _layers = new List<ICardChannelLayer>();
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// Returns index of a <see cref="ICardChannelLayer"/> instance in the layerDescriptions
        /// </summary>
        /// <param name="layer">Layer instance to find</param>
        /// <returns>The index of the layer in the layerDescriptions</returns>
        int getIndex(ICardChannelLayer layer)
        {
            for (int index = 0; index < _layers.Count; index++)
                if ((ICardChannelLayer)_layers[index] == (ICardChannelLayer)layer)
                    return index;
            throw new Exception("CardChannelStack: layer not descriptionFound in the stack");
        }

        #endregion

        #region >> ICardChannelStack Membres

        /// <inheritdoc />
        public List<ICardChannelLayer> layers
        {
            get { return _layers; }
        }

        /// <inheritdoc />
        public void addLayer(ICardChannelLayer layer)
        {
            _layers.Add((ICardChannelLayer)layer);
            layer.setStack(this);
        }

        /// <inheritdoc />
        public void releaseLayer(ICardChannelLayer layer)
        {
            _layers.Remove(layer);
        }

        /// <inheritdoc />
        public ICardChannelLayer requestLayer(ICardChannelLayer layer, SearchMode mode)
        {
            ICardChannelLayer newLayer;
            int index;
            if (_layers.Count == 0)
                throw new Exception("CardChannelStack.requestLayer(): no layers defined in the stack");
            switch (mode)
            {
                case SearchMode.bottom:
                    newLayer = _layers[_layers.Count - 1];
                    break;
                case SearchMode.next:
                    index = getIndex(layer);
                    if (index >= _layers.Count)
                        throw new Exception("CardChannelStack.requestLayer(): Seek next failed");
                    newLayer = _layers[index + 1];
                    break;
                case SearchMode.previous:
                    index = getIndex(layer);
                    if (index <= 0)
                        throw new Exception("CardChannelStack.requestLayer(): Seek previous failed");
                    newLayer = _layers[index - 1];
                    break;
                case SearchMode.top:
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
            get { return requestLayer(null, SearchMode.top).protocol; }
        }

        /// <inheritdoc />
        public string readerName
        {
            get { return requestLayer(null, SearchMode.top).readerName; }
        }

        /// <inheritdoc />
        public void attach(ICardContext context, string readerName)
        {
            requestLayer(null, SearchMode.top).attach(context, readerName);
        }

        /// <inheritdoc />
        public ErrorCode connect(ShareMode shareMode, Protocol preferedProtocol)
        {
            return requestLayer(null, SearchMode.top).connect(shareMode, preferedProtocol);
        }

        /// <inheritdoc />
        public ErrorCode disconnect(Disposition disposition)
        {
            return requestLayer(null, SearchMode.top).disconnect(disposition);
        }

        /// <inheritdoc />
        public ErrorCode getAttrib(Attrib attrib, ref byte[] buffer)
        {
            return requestLayer(null, SearchMode.top).getAttrib(attrib, ref buffer);
        }

        /// <inheritdoc />
        public State getStatus()
        {
            return requestLayer(null, SearchMode.top).getStatus();
        }

        /// <inheritdoc />
        public ErrorCode reconnect(ShareMode shareMode, Protocol preferedProtocol, Disposition initialization)
        {
            return requestLayer(null, SearchMode.top).reconnect(shareMode, preferedProtocol, initialization);
        }

        /// <inheritdoc />
        public ErrorCode transmit(ICardCommand command, ICardResponse response)
        {
            return requestLayer(null, SearchMode.top).transmit(command, response);
        }

        #endregion
    }
}