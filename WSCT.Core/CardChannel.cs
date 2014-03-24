using System;

namespace WSCT.Core
{
    /// <inheritdoc cref="CardChannelCore" />
    public class CardChannel : CardChannelObservable
    {
        #region >> Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardChannel()
            : base(new CardChannelCore())
        {
        }

        /// <inheritdoc cref="CardChannelCore(ICardContext,String)" />
        public CardChannel(ICardContext context, String readerName)
            : base(new CardChannelCore(context, readerName))
        {
        }

        #endregion
    }
}
