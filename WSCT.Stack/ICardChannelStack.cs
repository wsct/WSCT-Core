using System.Collections.Generic;
using WSCT.Core;

namespace WSCT.Stack
{
    /// <summary>
    /// Interface for card channel stack, ie card channel that can be used in a card channel stack (<see cref="ICardChannelLayer"/>).
    /// </summary>
    public interface ICardChannelStack : ICardChannel
    {
        /// <summary>
        /// Accessor to the current ordered list of layers.
        /// </summary>
        List<ICardChannelLayer> Layers { get; }

        /// <summary>
        /// Adds given <paramref name="layer"/> instance into the stack.
        /// </summary>
        /// <param name="layer">Layer instance to add.</param>
        void AddLayer(ICardChannelLayer layer);

        /// <summary>
        /// Removes given <paramref name="layer"/> instance from the stack.
        /// </summary>
        /// <param name="layer">Layer instance to remove.</param>
        void ReleaseLayer(ICardChannelLayer layer);

        /// <summary>
        /// Requests a layer, relative to given <paramref name="layer"/>.
        /// </summary>
        /// <param name="layer">Layer instance used for <see cref="SearchMode.Previous"/> / <see cref="SearchMode.Next"/> mode.</param>
        /// <param name="mode">Seek mode.</param>
        /// <returns>The requested layer.</returns>
        ICardChannelLayer RequestLayer(ICardChannelLayer layer, SearchMode mode);
    }
}