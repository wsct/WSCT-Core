using System;
using System.Collections.Generic;

using WSCT.Core;

namespace WSCT.Stack
{

    /// <summary>
    /// Interface for card channel layerDescriptions, ie card channel that can be used in a card channel layerDescriptions.
    /// See <see cref="ICardChannelStack"/>.
    /// </summary>
    public interface ICardChannelStack : ICardChannel
    {
        /// <summary>
        /// Accessor to the current ordered list of layers
        /// </summary>
        List<ICardChannelLayer> layers
        {
            get;
        }

        /// <summary>
        /// Adds given <paramref name="layer"/> instance into the layerDescriptions.
        /// </summary>
        /// <param name="layer">Layer instance to add</param>
        void addLayer(ICardChannelLayer layer);

        /// <summary>
        /// Removes given <paramref name="layer"/> instance from the layerDescriptions.
        /// </summary>
        /// <param name="layer">Layer instance to remove</param>
        void releaseLayer(ICardChannelLayer layer);

        /// <summary>
        /// Requests a layer, relative to given <paramref name="layer"/>.
        /// </summary>
        /// <param name="layer">Layer instance used for <see cref="SearchMode.previous"/> / <see cref="SearchMode.next"/> mode</param>
        /// <param name="mode">Seek mode</param>
        /// <returns>The requested layer</returns>
        ICardChannelLayer requestLayer(ICardChannelLayer layer, SearchMode mode);
    }
}
