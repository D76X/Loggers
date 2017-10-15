using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LogXtreme.WinDsk.Infrastructure.Convertes {

    public class BoolToHiddenVisibilityConverter : IValueConverter {

        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture) {

            if (!(value is bool)) { return value; }

            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture) {

            throw new NotImplementedException();
        }
    }
}
