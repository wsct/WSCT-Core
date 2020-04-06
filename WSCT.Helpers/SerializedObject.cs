using System.IO;
using System.Xml.Serialization;

namespace WSCT.Helpers
{
    /// <summary>
    /// Helper class to manage serialization.
    /// </summary>
    /// <typeparam name="T">Type of object to be used.</typeparam>
    public class SerializedObject<T>
    {
        #region >> Static members

        /// <summary>
        /// Unserializes an object from an XML file.
        /// </summary>
        /// <param name="xmlFileName">Name of the XML file.</param>
        /// <returns>The deserialized object.</returns>
        public static T LoadFromXml(string xmlFileName)
        {
            var serializer = new XmlSerializer(typeof(T));
            T t;

            using (TextReader textReader = new StreamReader(File.Open(xmlFileName, FileMode.Open)))
            {
                t = (T)serializer.Deserialize(textReader);
            }
            return t;
        }

        /// <summary>
        /// Serializes an object from an XML file.
        /// </summary>
        /// <param name="t">Object to save.</param>
        /// <param name="xmlFileName">Name of the XML file.</param>
        public static void SaveToXml(T t, string xmlFileName)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (TextWriter textWriter = new StreamWriter(File.Open(xmlFileName, FileMode.OpenOrCreate)))
            {
                serializer.Serialize(textWriter, t);
            }
        }

        #endregion
    }
}