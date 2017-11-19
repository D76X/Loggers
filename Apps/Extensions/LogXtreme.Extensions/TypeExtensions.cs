
using System;

namespace LogXtreme.Extensions {

    public static class TypeExtensions {

        /// <summary>
        /// Test whether the type of a value is as a given type.
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
    }
}
