using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using Prism.Regions;
using System.Collections.Generic;
using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure.Utils {

    public static class PrismUtils {

        public static IRegionManager GetScopedRegionManager(DependencyObject dependencyObject) {

            var window = SearchVisualTree.FindParent<Window>(dependencyObject);

            if (window == null) { return null; }

            var shellView = window as IShellView;

            if (shellView == null) { return null; }

            var shellViewModel = shellView.ViewModel as IRegionManagerAware;

            if (shellViewModel == null) { return null; }

            return shellViewModel.RegionManager;
        }
    }
}
