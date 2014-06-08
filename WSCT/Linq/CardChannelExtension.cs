using WSCT.Core;

namespace WSCT.Linq
{
    /// <summary>
    /// Extension methods for <see cref="ICardChannel"/>.
    /// </summary>
    public static class CardChannelExtension
    {
        /// <summary>
        /// Encapsulates an <see cref="ICardChannel"/> and add <see cref="ICardChannelObservable"/> behaviour.
        /// </summary>
        /// <param name="channel">Card channel to encapsulate.</param>
        /// <returns></returns>
        public static ICardChannelObservable ToObservable(this ICardChannel channel)
        {
            var observable = channel as ICardChannelObservable;

            return observable ?? new CardChannelObservable(channel);
        }
    }
}
