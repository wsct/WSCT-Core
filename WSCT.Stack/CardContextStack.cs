using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Wrapper;

namespace WSCT.Stack
{
    /// <summary>
    /// Implements <c>CardContextStackCore</c> as an <c>CardContextObservable</c> layerDescriptions
    /// </summary>
    public class CardContextStack : ICardContextStack
    {
        #region >> Attributes

        List<ICardContextLayer> _layers;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardContextStack()
            : base()
        {
            _layers = new List<ICardContextLayer>();
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// Returns index of a <see cref="ICardContextLayer"/> instance in the layerDescriptions
        /// </summary>
        /// <param name="layer">Layer instance to find</param>
        /// <returns>The index of the layer in the layerDescriptions</returns>
        int getIndex(ICardContextLayer layer)
        {
            for (int index = 0; index < _layers.Count; index++)
                if (_layers[index] == layer)
                    return index;
            throw new Exception("CardContextStack: layer not descriptionFound in the stack");
        }

        #endregion

        #region >> ICardContextStack Membres

        /// <inheritdoc />
        public List<ICardContextLayer> layers
        {
            get { return _layers; }
        }

        /// <inheritdoc />
        public void addLayer(ICardContextLayer layer)
        {
            _layers.Add(layer);
        }

        /// <inheritdoc />
        public void releaseLayer(ICardContextLayer layer)
        {
            _layers.Remove(layer);
        }

        /// <inheritdoc />
        public ICardContextLayer requestLayer(ICardContextLayer layer, SearchMode mode)
        {
            ICardContextLayer newLayer;
            int index;
            if (_layers.Count == 0)
                throw new Exception("CardContextStack.requestLayer(): no layers defined in the stack");
            switch (mode)
            {
                case SearchMode.bottom:
                    newLayer = _layers[_layers.Count - 1];
                    break;
                case SearchMode.next:
                    index = getIndex(layer);
                    if (index >= _layers.Count)
                        throw new Exception("CardContextStack.requestLayer(): Seek next failed");
                    newLayer = _layers[index + 1];
                    break;
                case SearchMode.previous:
                    index = getIndex(layer);
                    if (index <= 0)
                        throw new Exception("CardContextStack.requestLayer(): Seek previous failed");
                    newLayer = _layers[index - 1];
                    break;
                case SearchMode.top:
                    newLayer = _layers[0];
                    break;
                default:
                    throw new NotSupportedException(String.Format("CardContextStack.requestLayer(): Seek mode '{0}' unknown", mode));
            }
            return newLayer;
        }

        #endregion

        #region >> ICardContext Membres

        /// <inheritdoc />
        public IntPtr context
        {
            get { return requestLayer(null, SearchMode.top).context; }
        }

        /// <inheritdoc />
        public string[] groups
        {
            get { return requestLayer(null, SearchMode.top).groups; }
        }

        /// <inheritdoc />
        public int groupsCount
        {
            get { return requestLayer(null, SearchMode.top).groupsCount; }
        }

        /// <inheritdoc />
        public string[] readers
        {
            get { return requestLayer(null, SearchMode.top).readers; }
        }

        /// <inheritdoc />
        public int readersCount
        {
            get { return requestLayer(null, SearchMode.top).readersCount; }
        }

        /// <inheritdoc />
        public ErrorCode cancel()
        {
            return requestLayer(null, SearchMode.top).cancel();
        }

        /// <inheritdoc />
        public ErrorCode establish()
        {
            return requestLayer(null, SearchMode.top).establish();
        }

        /// <inheritdoc />
        public ErrorCode getStatusChange(uint timeout, AbstractReaderState[] readerStates)
        {
            return requestLayer(null, SearchMode.top).getStatusChange(timeout, readerStates);
        }

        /// <inheritdoc />
        public ErrorCode isValid()
        {
            return requestLayer(null, SearchMode.top).isValid();
        }

        /// <inheritdoc />
        public ErrorCode listReaders(string group)
        {
            return requestLayer(null, SearchMode.top).listReaders(group);
        }

        /// <inheritdoc />
        public ErrorCode listReaderGroups()
        {
            return requestLayer(null, SearchMode.top).listReaderGroups();
        }

        /// <inheritdoc />
        public ErrorCode release()
        {
            return requestLayer(null, SearchMode.top).release();
        }

        #endregion
    }
}
