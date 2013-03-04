using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Core;

namespace WSCT.Stack
{

    /// <summary>
    /// Interface for card context layers, ie card context that can be used in a card context stack.
    /// See <see cref="ICardContextStack"/>.
    /// </summary>
    /// <remarks>
    /// A given layer instance must be used only in one stack.
    /// </remarks>
    public interface ICardContextLayer : ICardContext
    {
        /// <summary>
        /// Allows to declare the stack using this layer.
        /// </summary>
        /// <param name="stack"></param>
        void setStack(ICardContextStack stack);
    }
}