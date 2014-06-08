using WSCT.Core;

namespace WSCT.Stack
{
    /// <summary>
    /// Interface for card channel layers, ie card channel that can be used in a card channel stack.
    /// </summary>
    /// <remarks>
    /// A given layer instance must be used only in one stack.
    /// </remarks>
    public interface ICardChannelLayer : ICardChannel
    {
        /// <summary>
        /// Allows to declare the layerDescriptions using this layer.
        /// </summary>
        /// <param name="stack">Stack containing this layer.</param>
        void SetStack(ICardChannelStack stack);

        /// <summary>
        /// Layer identifier (must be unique).
        /// </summary>
        string LayerId { get; }
    }
}