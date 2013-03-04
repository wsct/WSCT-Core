using System;
using System.Collections.Generic;
using System.Text;

using WSCT.Wrapper;

namespace WSCT.Core
{
    /// <summary>
    /// Represents an enhanced <see cref="CardContextCore"/> allowing to observe activity by using delegates
    /// </summary>
    public class CardContext : CardContextObservable
    {
        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardContext()
            : base(new CardContextCore())
        {
        }

        #endregion
    }
}
