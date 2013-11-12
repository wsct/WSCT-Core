﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace WSCT.Helpers
{
    /// <summary>
    /// Helper class to manage serialization
    /// </summary>
    /// <typeparam name="T">Type of object to be used</typeparam>
    public class SerializedObject<T>
    {
        #region >> Static members

        /// <summary>
        /// Unserializes an object from an XML file
        /// </summary>
        /// <param name="xmlFileName">Name of the XML file</param>
        /// <returns>The deserialized object</returns>
        public static T loadFromXml(String xmlFileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T t;

            using (TextReader textReader = new StreamReader(xmlFileName))
            {
                t = (T)serializer.Deserialize(textReader);
                textReader.Close();
            }
            return t;
        }

        /// <summary>
        /// Serializes an object from an XML file
        /// </summary>
        /// <param name="t">Object to save</param>
        /// <param name="xmlFileName">Name of the XML file</param>
        public static void saveToXml(T t, String xmlFileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (TextWriter textWriter = new StreamWriter(xmlFileName))
            {
                serializer.Serialize(textWriter, t);
                textWriter.Close();
            }
        }

        #endregion
    }
}