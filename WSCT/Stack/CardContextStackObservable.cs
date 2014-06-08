using System.Collections.Generic;
using WSCT.Core;

namespace WSCT.Stack
{
    public class CardContextStackObservable : CardContextObservable, ICardContextStackObservable
    {
        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="stack"></param>
        public CardContextStackObservable(ICardContextStack stack)
            : base(stack)
        {
        }

        #endregion

        #region >> ICardContextStackObservable

        /// <inheritdoc />
        public List<ICardContextLayer> Layers
        {
            get
            {
                var stack = (ICardContextStack)context;
                return stack.Layers;
            }
        }

        /// <inheritdoc />
        public void AddLayer(ICardContextLayer layer)
        {
            var stack = (ICardContextStack)context;
            stack.AddLayer(layer);
        }

        /// <inheritdoc />
        public void ReleaseLayer(ICardContextLayer layer)
        {
            var stack = (ICardContextStack)context;
            stack.ReleaseLayer(layer);
        }

        /// <inheritdoc />
        public ICardContextLayer RequestLayer(ICardContextLayer layer, SearchMode mode)
        {
            var stack = (ICardContextStack)context;
            return stack.RequestLayer(layer, mode);
        }

        #endregion
    }
}