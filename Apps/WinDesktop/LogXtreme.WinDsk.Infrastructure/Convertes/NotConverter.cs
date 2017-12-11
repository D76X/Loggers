﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace LogXtreme.WinDsk.Infrastructure.Convertes {
    public class NotConverter : IValueConverter {
        public object Convert(
            object value, 
            Type targetType, 
            object parameter,
            CultureInfo culture) {

            return (value is bool) ? !(bool)value : value;             
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
