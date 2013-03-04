using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Core;

namespace WSCT.Stack.Core
{

    /// <summary>
    /// Implements <see cref="CardContext"/> as a <see cref="CardContextLayer"/>
    /// </summary>
    /// <remarks>
    /// This layer is the terminal (top) layer by design.
    /// </remarks>
    public class CardContextLayer : CardContext, ICardContextLayer
    {
        #region >> Fields

        ICardContextStack _stack;

        #endregion

        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardContextLayer()
            : base()
        {
        }

        #endregion

        #region >> ICardContextLayer Membres

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack"></param>
        public void setStack(ICardContextStack stack)
        {
            _stack = stack;
        }

        #endregion
    }
}