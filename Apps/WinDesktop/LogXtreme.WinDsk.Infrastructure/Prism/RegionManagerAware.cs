using Prism.Regions;
using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary> 
    /// This class provides a set method to set the value of the IRegionManagerAware.RegionManager 
    /// to a given instance of IRegionManager. Implementations of IRegionManagerAware are generally
    /// view models which need to retain a reference to a scoped IRegionManager in order to navigate 
    /// properly from view to view within their scoped RegionManager.
    /// 
    /// </summary>
    public static class RegionManagerAware {

        /// <summary>
        /// Sets the IRegionManagerAware.RegionManager to an istance of IRegionManager
        /// </summary>
        /// <param name="item">Can be either a ViewModel implementing IRegionManagerAware or a View whose DataContext implements IRegionManagerAware</param>
        /// <param name="regionManager">Reference to a scope region manager</param>
        public static void SetRegionManagerAware(
            object item, 
            IRegionManager regionManager) {

            var rmAware = item as IRegionManagerAware;

            if (rmAware != null) {
                rmAware.RegionManager = regionManager;
            }

            var rmAwareFrameworkElement = item as FrameworkElement;

            if (rmAwareFrameworkElement != null) {

                var rmAwareDataContext = rmAwareFrameworkElement.DataContext as IRegionManagerAware;

                if (rmAwareDataContext != null) {
                    rmAwareDataContext.RegionManager = regionManager;
                }
            }
        }
    }
}
