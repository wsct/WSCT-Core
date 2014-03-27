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
        public IntPtr Context
        {
            get { return RequestLayer(null, SearchMode.Top).Context; }
        }

        /// <inheritdoc />
        public string[] Groups
        {
            get { return RequestLayer(null, SearchMode.Top).Groups; }
        }

        /// <inheritdoc />
        public int GroupsCount
        {
            get { return RequestLayer(null, SearchMode.Top).GroupsCount; }
        }

        /// <inheritdoc />
        public string[] Readers
        {
            get { return RequestLayer(null, SearchMode.Top).Readers; }
        }

        /// <inheritdoc />
        public int ReadersCount
        {
            get { return RequestLayer(null, SearchMode.Top).ReadersCount; }
        }

        /// <inheritdoc />
        public ErrorCode Cancel()
        {
            return RequestLayer(null, SearchMode.Top).Cancel();
        }

        /// <inheritdoc />
        public ErrorCode Establish()
        {
            return RequestLayer(null, SearchMode.Top).Establish();
        }

        /// <inheritdoc />
        public ErrorCode GetStatusChange(uint timeout, AbstractReaderState[] readerStates)
        {
            return RequestLayer(null, SearchMode.Top).GetStatusChange(timeout, readerStates);
        }

        /// <inheritdoc />
        public ErrorCode IsValid()
        {
            return RequestLayer(null, SearchMode.Top).IsValid();
        }

        /// <inheritdoc />
        public ErrorCode ListReaders(string group)
        {
            return RequestLayer(null, SearchMode.Top).ListReaders(group);
        }

        /// <inheritdoc />
        public ErrorCode ListReaderGroups()
        {
            return RequestLayer(null, SearchMode.Top).ListReaderGroups();
        }

        /// <inheritdoc />
        public ErrorCode Release()
        {
            return RequestLayer(null, SearchMode.Top).Release();
        }

        #endregion
    }
}