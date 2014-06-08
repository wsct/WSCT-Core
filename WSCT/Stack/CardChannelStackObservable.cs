using System.Collections.Generic;
using WSCT.Core;

namespace WSCT.Stack
{
    public class CardChannelStackObservable : CardChannelObservable, ICardChannelStackObservable
    {
        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="stack"></param>
        public CardChannelStackObservable(ICardChannelStack stack)
            : base(stack)
        {
        }

        #endregion

        #region >> ICardChannelStack

        /// <inheritdoc />
        public List<ICardChannelLayer> Layers
        {
            get
            {
                var stack = (ICardChannelStack)channel;
                return stack.Layers;
            }
        }

        /// <inheritdoc />
        public void AddLayer(ICardChannelLayer layer)
        {
            var stack = (ICardChannelStack)channel;
            stack.AddLayer(layer);
        }

        /// <inheritdoc />
        public void ReleaseLayer(ICardChannelLayer layer)
        {
            var stack = (ICardChannelStack)channel;
            stack.ReleaseLayer(layer);
        }

        /// <inheritdoc />
        public ICardChannelLayer RequestLayer(ICardChannelLayer layer, SearchMode mode)
        {
            var stack = (ICardChannelStack)channel;
            return stack.RequestLayer(layer, mode);
        }

        #endregion
    }
}