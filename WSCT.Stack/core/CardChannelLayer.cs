using System;
using WSCT.Core;

namespace WSCT.Stack.Core
{
    /// <summary>
    /// Implements <see cref="CardChannel"/> as a <see cref="CardChannelLayer"/>.
    /// </summary>
    /// <remarks>
    /// This layer is the terminal (top) layer by design.
    /// </remarks>
    public class CardChannelLayer : CardChannel, ICardChannelLayer
    {
        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CardChannelLayer()
        {
        }

        /// <inheritdoc cref="CardChannel(ICardContext,string)"/>
        public CardChannelLayer(ICardContext context, String readerName)
            : base(context, readerName)
        {
        }

        #endregion

        #region >> ICardChannelLayer Membres

        /// <inheritdoc />
        public void SetStack(ICardChannelStack stack)
        {
            // Nothing to do here.
        }

        #endregion
    }
}