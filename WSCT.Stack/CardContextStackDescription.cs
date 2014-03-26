using System.Xml.Serialization;
using WSCT.Stack.Generic;

namespace WSCT.Stack
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("ContextStack")]
    public class CardContextStackDescription : GenericStackDescription<CardContextLayerDescription, ICardContextLayer>
    {
    }
}