using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WSCT.Helpers.Reflection
{
    /// <summary>
    /// Allow to load objects from an external assembly by using reflection.
    /// </summary>
    public class AssemblyLoader
    {
        #region >> Static members

        /// <summary>
        /// Create a new instance of a <typeparamref name="T"/> object .
        /// </summary>
        /// <typeparam name="T">Type of object to be created.</typeparam>
        /// <param name="assemblyFileName">Path and filename to the assembly (if <c>null</c> or <c>Empty</c>, <paramref name="typeName"/> is supposed to be accessible.</param>
        /// <param name="typeName">Type name of the instance to create from the assembly.</param>
        /// <returns>A new instance of the <c>T</c> object, or <c>null</c> if the <paramref name="typeName"/> class could not be found.</returns>
        public static T CreateInstance<T>(string assemblyFileName, string typeName)
        {
            var instance = default(T);
            Type type;
            if (String.IsNullOrEmpty(assemblyFileName))
            {
                type = Type.GetType(typeName);
            }
            else
            {
                if (assemblyFileName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                {
                    assemblyFileName = Path.GetFileNameWithoutExtension(assemblyFileName);
                }
                var assembly = Assembly.Load(new AssemblyName(assemblyFileName));
                type = assembly.GetType(typeName);
            }
            if (type != null)
            {
                instance = (T)Activator.CreateInstance(type);
            }
            return instance;
        }

        /// <summary>
        /// Retrieve a list of <c>Type</c>s from an external assembly, implementing a given <c>T</c> type (class or interface).
        /// </summary>
        /// <typeparam name="T">Type to be searched.</typeparam>
        /// <param name="assemblyFileName">Path and filename to the assembly.</param>
        /// <returns>A list of <c>Type</c> implementing <typeparamref name="T"/>.</returns>
        public static List<Type> GetTypesByInterface<T>(string assemblyFileName)
        {
            if (assemblyFileName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            {
                assemblyFileName = Path.GetFileNameWithoutExtension(assemblyFileName);
            }
            var assembly = Assembly.Load(new AssemblyName(assemblyFileName));
            var typeInfo = typeof(T).GetTypeInfo();

            return assembly.ExportedTypes
                .Where(t => t.GetTypeInfo().IsAssignableFrom(typeInfo))
                .ToList();
        }

        #endregion
    }
}