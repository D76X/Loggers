using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using LogXtreme.Extensions;

namespace LogXtreme.WinDsk.TestDataGrid.Converters {

    /// <summary>
    /// Refs
    /// https://stackoverflow.com/questions/20290842/converter-to-show-description-of-an-enum-and-convert-back-to-enum-value-on-sele
    /// https://app.pluralsight.com/player?course=wpf-productivity-playbook&author=brian-noyes&name=wpf-productivity-playbook-m5&clip=3&mode=live
    /// </summary>
    public class EnumToDisplayConverter : IValueConverter {

        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture) {

            if(value == null) { return DependencyProperty.UnsetValue; }

            return ((Enum)value).GetName();
        }

        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture) {

            return Enum.ToObject(targetType, value);
        }
    }
}
