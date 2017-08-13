using System;

namespace LogXtreme.Extensions {

    public static class EnumExtensions {

        public static string GetName(this Enum enumValue) {

            return Enum.GetName(enumValue.GetType(), enumValue);
        }        
    }
}
