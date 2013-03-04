using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Core;

namespace WSCT.Stack
{

    /// <summary>
    /// Interface for card channel layers, ie card channel that can be used in a card channel stack.
    /// See <see cref="ICardChannelStack"/>.
    /// </summary>
    /// <remarks>
    /// A given layer instance must be used only in one stack.
    /// </remarks>
    public interface ICardChannelLayer : ICardChannel
    {
        /// <summary>
        /// Allows to declare the layerDescriptions using this layer.
        /// </summary>
        /// <param name="stack"></param>
        void setStack(ICardChannelStack stack);
    }
}