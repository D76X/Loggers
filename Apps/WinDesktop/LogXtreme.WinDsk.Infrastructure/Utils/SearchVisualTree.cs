using System.Windows;
using System.Windows.Media;

namespace LogXtreme.WinDsk.Infrastructure.Utils {
    static class SearchVisualTree {

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject {

            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) {
                return null;
            }

            var parent = parentObject as T;

            if (parent != null) {
                return parent;
            }

            return FindParent<T>(parentObject);
        }
    }
}
