using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace WSCT.Stack.Generic
{
    /// <summary>
    /// Generic layer description (by its name, assembly and class name) for dynamic load.
    /// </summary>
    /// <typeparam name="TLayerDescription">Type of concrete layer description.</typeparam>
    /// <typeparam name="TILayer">Type of layer described.</typeparam>
    public class GenericStackDescription<TLayerDescription, TILayer>
        where TLayerDescription : GenericLayerDescription
    {
        #region >> Fields

        private List<TLayerDescription> _layerDescriptions;

        #endregion

        #region >> Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("layer")]
        public List<TLayerDescription> LayerDescriptions
        {
            get { return _layerDescriptions; }
            set { _layerDescriptions = value; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public GenericStackDescription()
        {
            _layerDescriptions = new List<TLayerDescription>();
        }

        #endregion

        #region >> Static methods

        /// <summary>
        /// Creates a new instance of the <typeparam name="TILayer"/> described by this <typeparam name="TLayerDescription"/>.
        /// </summary>
        /// <param name="layerDesc"></param>
        /// <returns>A new instance of <typeparam name="TILayer"/>.</returns>
        public static TILayer CreateInstance(TLayerDescription layerDesc)
        {
            var assembly = Assembly.LoadFrom(layerDesc.PathToDll + layerDesc.DllName);
            var type = assembly.GetType(layerDesc.ClassName);
            return (TILayer)Activator.CreateInstance(type);
        }

        #endregion

        #region >> Methods

        /// <summary>
        /// Adds a new <typeparam name="TLayerDescription"/>.
        /// </summary>
        /// <param name="layerDesc"></param>
        public void Add(TLayerDescription layerDesc)
        {
            _layerDescriptions.Add(layerDesc);
        }

        /// <summary>
        /// Create a new instance of <typeparamref name="TILayer"/> named <paramref name="layerName"/>.
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public TILayer CreateInstance(string layerName)
        {
            return CreateInstance(Get(layerName));
        }

        /// <summary>
        /// Checks if the <paramref name="layerName"/> is defined.
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public Boolean IsValid(string layerName)
        {
            var found = false;
            foreach (var contextLayer in _layerDescriptions)
            {
                if (contextLayer.Name == layerName)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        /// <summary>
        /// Get the <typeparamref name="TLayerDescription"/> instance which name is <paramref name="layerName"/>.
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns>The CardStackDescriptor instance or null if not find.</returns>
        private TLayerDescription Get(string layerName)
        {
            TLayerDescription descriptionFound = null;
            foreach (var layerDesc in _layerDescriptions)
            {
                if (layerDesc.Name == layerName && layerDesc.IsValid)
                {
                    descriptionFound = layerDesc;
                    break;
                }
            }
            return descriptionFound;
        }

        #endregion
    }
}