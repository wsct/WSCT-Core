using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Xml.Serialization;

namespace WSCT.Stack
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("ContextStack")]
    public class CardContextStackDescription : Generic.GenericStackDescription<CardContextLayerDescription, ICardContextLayer>
    {
        #region >> Constructors

        /// <summary>
        /// 
        /// </summary>
        public CardContextStackDescription()
            : base()
        {
        }

        #endregion
    }
}
