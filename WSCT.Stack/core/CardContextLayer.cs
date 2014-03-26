using WSCT.Core;

namespace WSCT.Stack.Core
{
    /// <summary>
    /// Implements <see cref="CardContext"/> as a <see cref="CardContextLayer"/>.
    /// </summary>
    /// <remarks>
    /// This layer is the terminal (top) layer by design.
    /// </remarks>
    public class CardContextLayer : CardContext, ICardContextLayer
    {
        #region >> ICardContextLayer Membres

        /// <inheritdoc />
        public void SetStack(ICardContextStack stack)
        {
            // Nothing to do here.
        }

        #endregion
    }
}