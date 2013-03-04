using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Core;

namespace WSCT.Stack.Core
{

    /// <summary>
    /// Implements <see cref="CardChannel"/> as a <see cref="CardChannelLayer"/>
    /// </summary>
    /// <remarks>
    /// This layer is the terminal (top) layer by design.
    /// </remarks>
    public class CardChannelLayer : CardChannel, ICardChannelLayer
    {
        #region >> Fields

        ICardChannelStack _stack;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardChannelLayer()
            : base()
        {
        }

        /// <inheritdoc cref="CardChannel(ICardContext,String)"/>
        public CardChannelLayer(ICardContext context, String readerName)
            : base(context, readerName)
        {
        }

        #endregion

        #region >> ICardChannelLayer Membres

        /// <inheritdoc />
        public void setStack(ICardChannelStack stack)
        {
            _stack = stack;
        }

        #endregion
    }
}