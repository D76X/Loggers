
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogXtreme.Extensions {

    public static class TypeExtensions {

        /// <summary>
        /// Tests whether the type of a value is as a given type.
        /// </summary>
        /// <param name="typeOne">The expected type</param>
        /// <param name="value">The value whose type is tested</param>
        /// <param name="resultType">The type of the tested value</param>
        /// <returns>true if the tested value as the type of the caller</returns>
        public static bool IsTypeOf(
            this Type typeOne,
            object value,
            out Type resultType) {

            resultType = value.GetType();
            return typeOne == resultType;
        }

        /// <summary>
        /// Refs
        /// http://www.codinghelmet.com/?path=howto/implement-icomparable-t
        /// </summary>
        /// <param name="type">the calling type</param>
        /// <returns></returns>
        public static bool IsIComparable(
            this Type type) => type.GetInterface(nameof(IComparable)) != null;

        /// <summary>
        /// Refs
        /// https://stackoverflow.com/questions/10900021/test-whether-an-object-implements-a-generic-interface-for-any-generic-type
        /// https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
        /// Refs
        /// http://www.codinghelmet.com/?path=howto/implement-icomparable-t
        /// </summary>
        /// <typeparam name="T">any type</typeparam>
        /// <param name="type">the calling type</param>
        /// <returns></returns>
        public static bool IsIComparable<T>(
            this Type type) => type.GetInterfaces()
                                   .Where(t => t.IsGenericType)
                                   .Select(t => t.GetGenericTypeDefinition())
                                   .Any(t => t.Equals(typeof(IComparable<>)));

        /// <summary>
        /// Refs
        /// https://stackoverflow.com/questions/10900021/test-whether-an-object-implements-a-generic-interface-for-any-generic-type
        /// </summary>
        /// <param name="type">The expected type</param>
        /// <returns></returns>
        public static bool IsGenericDictionary(
            this Type type) => type.GetInterfaces()
                                   .Where(t => t.IsGenericType)
                                   .Select(t => t.GetGenericTypeDefinition())
                                   .Any(t => t.Equals(typeof(IDictionary<,>)));


    }
}
