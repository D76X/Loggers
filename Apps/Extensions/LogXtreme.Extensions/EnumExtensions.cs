using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LogXtreme.Extensions {

    public static class EnumExtensions {

        public static string GetName(this Enum enumValue) {

            return Enum.GetName(enumValue.GetType(), enumValue);
        }   
        
        public static string GetDescription(this Enum enumValue) {

            var enumValueName = enumValue.GetName();

            // the values of an Enum are defined as fields on its type
            var fieldInfo = enumValue.GetType().GetField(enumValueName);            

            var descriptionAttr = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).
                                            FirstOrDefault()
                                            as DescriptionAttribute;

            return descriptionAttr == null ? 
                   enumValueName :
                   descriptionAttr.Description;
        }

        /// <summary>
        /// Refs
        /// https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
        /// https://stackoverflow.com/questions/3816718/how-to-get-an-array-of-all-enum-values-in-c
        /// https://stackoverflow.com/questions/15388072/how-to-add-extension-methods-to-enums
        /// https://stackoverflow.com/questions/972307/can-you-loop-through-all-enum-values?rq=1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>() where T : struct, IConvertible {

            if(!typeof(T).IsEnum) {
                throw new ArgumentException($"{nameof(T)} must be an enumerated type");
            }

            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
