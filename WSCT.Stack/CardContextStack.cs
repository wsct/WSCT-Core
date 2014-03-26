using System;
using System.Collections.Generic;
using WSCT.Wrapper;

namespace WSCT.Stack
{
    /// <summary>
    /// Reference implementation of <see cref="ICardContextStack"/>.
    /// </summary>
    public class CardContextStack : ICardContextStack
    {
        #region >> Attributes

        private readonly List<ICardContextLayer> _layers;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CardContextStack()
        {
            _layers = new List<ICardContextLayer>();
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// Returns index of a <see cref="ICardContextLayer"/> instance in the stack.
        /// </summary>
        /// <param name="layer">Layer instance to find.</param>
        /// <returns>The index of the layer in the stack.</returns>
        private int GetIndex(ICardContextLayer layer)
        {
            for (var index = 0; index < _layers.Count; index++)
            {
                if (_layers[index] == layer)
                {
                    return index;
                }
            }
            throw new Exception("CardContextStack: layer not descriptionFound in the stack");
        }

        #endregion

        #region >> ICardContextStack Membres

        /// <inheritdoc />
        public List<ICardContextLayer> Layers
        {
            get { return _layers; }
        }

        /// <inheritdoc />
        public void AddLayer(ICardContextLayer layer)
        {
            _layers.Add(layer);
        }

        /// <inheritdoc />
        public void ReleaseLayer(ICardContextLayer layer)
        {
            _layers.Remove(layer);
        }

        /// <inheritdoc />
        public ICardContextLayer RequestLayer(ICardContextLayer layer, SearchMode mode)
        {
            ICardContextLayer newLayer;
            int index;
            if (_layers.Count == 0)
            {
                throw new Exception("CardContextStack.requestLayer(): no layers defined in the stack");
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
                        throw new Exception("CardContextStack.requestLayer(): Seek next failed");
                    }
                    newLayer = _layers[index + 1];
                    break;
                case SearchMode.Previous:
                    index = GetIndex(layer);
                    if (index <= 0)
                    {
                        throw new Exception("CardContextStack.requestLayer(): Seek previous failed");
                    }
                    newLayer = _layers[index - 1];
                    break;
                case SearchMode.Top:
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
            get { return RequestLayer(null, SearchMode.Top).context; }
        }

        /// <inheritdoc />
        public string[] groups
        {
            get { return RequestLayer(null, SearchMode.Top).groups; }
        }

        /// <inheritdoc />
        public int groupsCount
        {
            get { return RequestLayer(null, SearchMode.Top).groupsCount; }
        }

        /// <inheritdoc />
        public string[] readers
        {
            get { return RequestLayer(null, SearchMode.Top).readers; }
        }

        /// <inheritdoc />
        public int readersCount
        {
            get { return RequestLayer(null, SearchMode.Top).readersCount; }
        }

        /// <inheritdoc />
        public ErrorCode cancel()
        {
            return RequestLayer(null, SearchMode.Top).cancel();
        }

        /// <inheritdoc />
        public ErrorCode establish()
        {
            return RequestLayer(null, SearchMode.Top).establish();
        }

        /// <inheritdoc />
        public ErrorCode getStatusChange(uint timeout, AbstractReaderState[] readerStates)
        {
            return RequestLayer(null, SearchMode.Top).getStatusChange(timeout, readerStates);
        }

        /// <inheritdoc />
        public ErrorCode isValid()
        {
            return RequestLayer(null, SearchMode.Top).isValid();
        }

        /// <inheritdoc />
        public ErrorCode listReaders(string group)
        {
            return RequestLayer(null, SearchMode.Top).listReaders(group);
        }

        /// <inheritdoc />
        public ErrorCode listReaderGroups()
        {
            return RequestLayer(null, SearchMode.Top).listReaderGroups();
        }

        /// <inheritdoc />
        public ErrorCode release()
        {
            return RequestLayer(null, SearchMode.Top).release();
        }

        #endregion
    }
}