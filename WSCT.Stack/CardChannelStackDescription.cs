using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace WSCT.Stack
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("ChannelStack")]
    public class CardChannelStackDescription : Generic.GenericStackDescription<CardChannelLayerDescription, ICardChannelLayer>
    {
        #region >> Constructors

        /// <summary>
        /// 
        /// </summary>
        public CardChannelStackDescription()
            : base()
        {
        }

        #endregion
    }
}
