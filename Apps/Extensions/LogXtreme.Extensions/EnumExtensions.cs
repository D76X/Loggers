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
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>() where T: Enum {

        }
    }
}
