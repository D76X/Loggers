using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace LogXtreme.WinDsk.Infrastructure.Convertes {

    /// <summary>
    /// Refs
    /// http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/
    /// https://app.pluralsight.com/player?course=wpf-productivity-playbook&author=brian-noyes&name=wpf-productivity-playbook-m5&clip=3&mode=live
    /// </summary>
    public class BooleanToVisibilityConverter : 
        MarkupExtension,
        IValueConverter {

        /// <summary>
        /// true => Visible
        /// false => either FalseVisibility (default Collapsed) 
        /// </summary>
        public Visibility FalseVisibility { get; set; }

        public bool Negate { get; set; }

        public BooleanToVisibilityConverter() {
            this.FalseVisibility = Visibility.Collapsed;
        }

        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture) {

            bool bval;

            if (!bool.TryParse(value.ToString(), out bval)) { return value; }

            if (bval && !this.Negate || !bval && this.Negate) {
                return Visibility.Visible;
            }

            return this.FalseVisibility;
        }

        public object ConvertBack(
            object value, Type 
            targetType, 
            object parameter, 
            CultureInfo culture) {

            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementation of MarkupExtension.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
