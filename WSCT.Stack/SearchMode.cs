using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Wrapper;
using WSCT.Core;
using WSCT.Core.APDU;

namespace WSCT.Stack
{

    /// <summary>
    /// Enumeration defining different modes of going through the layerDescriptions
    /// </summary>
    public enum SearchMode : byte
    {
        /// <summary>
        /// Seek next layer in the layerDescriptions
        /// </summary>
        next,
        /// <summary>
        /// Seek previous layer in the layerDescriptions
        /// </summary>
        previous,
        /// <summary>
        /// Seek top (first) layer in the layerDescriptions
        /// </summary>
        top,
        /// <summary>
        /// Seek bottom (last) layer in the layerDescriptions
        /// </summary>
        bottom
    }
}