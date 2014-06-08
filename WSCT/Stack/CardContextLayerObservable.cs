using WSCT.Core;

namespace WSCT.Stack
{
    /// <summary>
    /// Allows an existing <see cref="ICardContextLayer"/> instance to be observed by using delegates and wrapping it.
    /// </summary>
    public class CardContextLayerObservable : CardContextObservable, ICardContextLayerObservable
    {
        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="contextLayer"><b>ICardContext</b> instance to wrap.</param>
        public CardContextLayerObservable(ICardContextLayer contextLayer)
            : base(contextLayer)
        {
        }

        #endregion

        #region >> ICardContextLayerObservable

        /// <inheritdoc />
        public void SetStack(ICardContextStack stack)
        {
            var layer = (ICardContextLayer)context;
            layer.SetStack(stack);
        }

        /// <inheritdoc />
        public string LayerId
        {
            get
            {
                var layer = (ICardContextLayer)context;
                return layer.LayerId;
            }
        }

        #endregion
    }
}