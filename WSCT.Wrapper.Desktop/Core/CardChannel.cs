using WSCT.Core;

namespace WSCT.Wrapper.Desktop.Core
{
    /// <inheritdoc cref="CardChannelCore" />
    public class CardChannel : CardChannelObservable
    {
        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CardChannel()
            : base(new CardChannelCore())
        {
        }

        /// <inheritdoc cref="CardChannelCore(ICardContext,string)" />
        public CardChannel(ICardContext context, string readerName)
            : base(new CardChannelCore(context, readerName))
        {
        }

        #endregion
    }
}