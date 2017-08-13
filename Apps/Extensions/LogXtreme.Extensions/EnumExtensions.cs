using System;
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
    }
}
