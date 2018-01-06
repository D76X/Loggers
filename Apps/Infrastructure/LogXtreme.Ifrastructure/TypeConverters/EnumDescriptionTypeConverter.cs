using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace LogXtreme.Ifrastructure.TypeConverters {

    /// <summary>
    /// Coverts the value of enumeration type to the string set by the 
    /// description attribute if one is found. If no description attribute
    /// is found for the enumerated value then the string literal of the 
    /// value is returned instead.
    /// Refs
    /// http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/
    /// https://msdn.microsoft.com/en-us/library/ayybcxe5.aspx
    /// </summary>
    public class EnumDescriptionTypeConverter : EnumConverter {

        public EnumDescriptionTypeConverter(Type type) : 
            base(type) { }

        public override object ConvertTo(
            ITypeDescriptorContext context, 
            CultureInfo culture, 
            object value, 
            Type destinationType) {

            if (destinationType == typeof(string)) {

                if (value != null) {

                    FieldInfo fi = value.GetType().GetField(value.ToString());

                    if (fi != null) {

                        var attributes = (DescriptionAttribute[])fi
                            .GetCustomAttributes(typeof(DescriptionAttribute), false);

                        return attributes.Length > 0 &&
                               !String.IsNullOrEmpty(attributes[0].Description) ?
                               attributes[0].Description :
                               value.ToString();
                    }                    
                }

                return string.Empty;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
