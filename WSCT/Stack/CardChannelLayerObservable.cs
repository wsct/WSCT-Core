using WSCT.Core;

namespace WSCT.Stack
{
    /// <summary>
    /// Allows an existing <see cref="ICardChannel"/> instance to be observed by using delegates and wrapping it.
    /// </summary>
    public class CardChannelLayerObservable : CardChannelObservable, ICardChannelLayerObservable
    {
        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="channelLayer">Instance to wrap.</param>
        public CardChannelLayerObservable(ICardChannelLayer channelLayer)
            : base(channelLayer)
        {
        }

        #endregion

        #region >> ICardChannelLayerObservable

        /// <inheritdoc />
        public void SetStack(ICardChannelStack stack)
        {
            var layer = (ICardChannelLayer)channel;
            layer.SetStack(stack);
        }

        /// <inheritdoc />
        public string LayerId
        {
            get
            {
                var layer = (ICardChannelLayer)channel;
                return layer.LayerId;
            }
        }

        #endregion
    }
}