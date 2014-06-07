using System;
using System.Collections.Generic;
using WSCT.Helpers.Linq;
using WSCT.Wrapper;

namespace WSCT.Stack
{
    /// <summary>
    ///     Reference implementation of <see cref="ICardContextStack" />.
    /// </summary>
    public class CardContextStack : ICardContextStack
    {
        #region >> Attributes

        private readonly List<ICardContextLayer> layers;

        #endregion

        #region >> Constructors

        /// <summary>
        ///     Initializes a new instance.
        /// </summary>
        public CardContextStack()
        {
            layers = new List<ICardContextLayer>();
        }

        #endregion

        #region >> ICardContextStack Membres

        /// <inheritdoc />
        public List<ICardContextLayer> Layers
        {
            get { return layers; }
        }

        /// <inheritdoc />
        public void AddLayer(ICardContextLayer layer)
        {
            layers.Add(layer);
        }

        /// <inheritdoc />
        public void ReleaseLayer(ICardContextLayer layer)
        {
            layers.Remove(layer);
        }

        /// <inheritdoc />
        public ICardContextLayer RequestLayer(ICardContextLayer layer, SearchMode mode)
        {
            if (layers.Count == 0)
            {
                throw new Exception("CardContextStack.requestLayer(): no layers defined in the stack");
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
                    throw new NotSupportedException(String.Format("CardContextStack.requestLayer(): Seek mode '{0}' unknown", mode));
            }
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