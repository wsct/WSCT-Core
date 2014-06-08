using WSCT.Stack;
using WSCT.Wrapper.Desktop.Core;

namespace WSCT.Wrapper.Desktop.Stack
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

        /// <inheritdoc />
        public string LayerId
        {
            get { return "PC/SC"; }
        }

        #endregion
    }
}