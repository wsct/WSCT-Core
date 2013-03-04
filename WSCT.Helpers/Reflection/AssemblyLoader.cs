using System;
using System.Collections.Generic;
using System.Reflection;

namespace WSCT.Helpers.Reflection
{
    /// <summary>
    /// Allow to load objects from an external assembly by using reflection
    /// </summary>
    public class AssemblyLoader
    {
        #region >> Static members

        /// <summary>
        /// Create a new instance of a <c>T</c> object 
        /// </summary>
        /// <typeparam name="T">Type of object to be created</typeparam>
        /// <param name="assemblyFileName">Path and filename to the assembly (if <c>null</c> or <c>Empty</c>, <paramref name="typeName"/> is supposed to be accessible</param>
        /// <param name="typeName">Type name of the instance to create from the assembly</param>
        /// <returns>A new instance of the <c>T</c> object, or <c>null</c> if the <paramref name="typeName"/> class could not be found</returns>
        public static T createInstance<T>(String assemblyFileName, String typeName)
        {
            T instance = default(T);
            Type type;
            if (String.IsNullOrEmpty(assemblyFileName))
            {
                type = Type.GetType(typeName);
            }
            else
            {
                Assembly assembly = Assembly.LoadFrom(assemblyFileName);
                type = assembly.GetType(typeName);
            }
            if (type != null)
            {
                instance = (T)Activator.CreateInstance(type);
            }
            return instance;
        }

        /// <summary>
        /// Retrieve a list of <c>Type</c>s from an external assembly, implementing a given <c>T</c> type (class or interface)
        /// </summary>
        /// <typeparam name="T">Type to be searched</typeparam>
        /// <param name="assemblyFileName">Path and filename to the assembly</param>
        /// <returns>A list of <c>Type</c> implementing <c>T</c></returns>
        public static List<Type> getTypesByInterface<T>(String assemblyFileName)
        {
            List<Type> result = new List<Type>();

            Assembly assembly = Assembly.LoadFrom(assemblyFileName);
            Type[] types = assembly.GetExportedTypes();

            TypeFilter typeFilter = new TypeFilter(
                delegate(Type typeFiltered, Object filterCriteria)
                {
                    return typeFiltered.Equals(filterCriteria);
                }
            );

            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].FindInterfaces(typeFilter, typeof(T)).Length > 0)
                {
                    result.Add(types[i]);
                }
            }

            return result;
        }

        #endregion
    }
}
