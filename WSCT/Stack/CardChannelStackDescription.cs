using System.Xml.Serialization;
using WSCT.Stack.Generic;

namespace WSCT.Stack
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("ChannelStack")]
    public class CardChannelStackDescription : GenericStackDescription<CardChannelLayerDescription, ICardChannelLayer>
    {
    }
}