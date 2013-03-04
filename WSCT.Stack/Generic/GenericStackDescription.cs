using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace WSCT.Stack.Generic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TLayerDescription"></typeparam>
    /// <typeparam name="TILayer"></typeparam>
    public class GenericStackDescription<TLayerDescription, TILayer>
        where TLayerDescription : GenericLayerDescription
    {
        #region >> Fields

        List<TLayerDescription> _layerDescriptions;

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("layer")]
        public List<TLayerDescription> layerDescriptions
        {
            get { return _layerDescriptions; }
            set { _layerDescriptions = value; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// 
        /// </summary>
        public GenericStackDescription()
        {
            _layerDescriptions = new List<TLayerDescription>();
        }

        #endregion

        #region >> Static methods

        /// <summary>
        /// Create a new instance of the layerDescriptions described by <c>CardStackDescriptor</c>
        /// </summary>
        /// <param name="layerDesc"></param>
        /// <returns>A new instance of the layerDesc</returns>
        public static TILayer createInstance(TLayerDescription layerDesc)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(layerDesc.pathToDll + layerDesc.dllName);
            Type type = assembly.GetType(layerDesc.className);
            return (TILayer)Activator.CreateInstance(type);
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layerDesc"></param>
        public void add(TLayerDescription layerDesc)
        {
            _layerDescriptions.Add(layerDesc);
        }

        /// <summary>
        /// Create a new instance of the layerDesc named <c>cardStackNamed</c>
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public TILayer createInstance(String layerName)
        {
            return GenericStackDescription<TLayerDescription, TILayer>.createInstance(get(layerName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public Boolean isValid(String layerName)
        {
            Boolean found = false;
            foreach (TLayerDescription contextLayer in _layerDescriptions)
                if (contextLayer.name == layerName)
                {
                    found = true;
                    break;
                }
            return found;
        }

        /// <summary>
        /// Get the <c>CardStackDescriptor</c> instance which name is <c>layerName</c>
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns>The CardStackDescriptor instance or null if not find</returns>
        TLayerDescription get(String layerName)
        {
            TLayerDescription descriptionFound = null;
            foreach (TLayerDescription layerDesc in _layerDescriptions)
                if (layerDesc.name == layerName && layerDesc.isValid)
                {
                    descriptionFound = layerDesc;
                    break;
                }
            return descriptionFound;
        }

        #endregion
    }
}
