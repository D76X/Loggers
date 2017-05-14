using System;
using Prism.Mvvm;
using Prism.Regions;

namespace LogXtreme.WinDsk.Infrastructure {

    public class ViewModelBase : 
        BindableBase, 
        IViewModelBase, 
        INavigationAware, 
        IRegionMemberLifetime {

        /// <summary>
        /// When a Navigation Request is made to the RegionManager and a view becomes the 
        /// active view of a target region the view from which it has been navigated from
        /// becomes inactive and IRegionMemberLifetime.KeepAlive is invoked on it.
        /// When IRegionMemberLifetime.KeepAlive returns false the instance of the view 
        /// and view model are dereferenced and left to the GC. When KeepAlive => true
        /// the view and view model are kept in memory.
        /// </summary>
        public virtual bool KeepAlive => false;

        /// <summary>
        /// When a Navigation Request is made to the RegionManager the IsNavigationTarget
        /// of each view instance implementing INavigationAware that exists in the target 
        /// region will be checked. If there is a INavigationAware instance that returns 
        /// true that view is navigated to and beocomes the active view. If all the views 
        /// implementing INavigationAware in the target region retun false from their
        /// IsNavigationTarget a new instance of the requested type is resolved by the DI
        /// and becomes the acive view.
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns>TRUE to activate the view, FALSE otherwise</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) {
            // 
            return true;
        }

        /// <summary>
        /// When a Navigation Request is made to the RegionManager the OnNavigatedFrom
        /// occurs on the view model for the active view implementing INavigationAware. 
        /// In INavigationAware.OnNavigatedFrom state, data or teardown, etc. related 
        /// to the active view can be implemented to take place before navigation to 
        /// another view occurs.
        /// occours.
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) {
            // save data and state...
        }

        /// <summary>
        /// When a Navigation Request is made to the RegionManager and a view is the 
        /// selected target of the navigation OnNavigatedTo is invoked. OnNavigatedTo
        /// may be used to execute initialisation and/or use oarameter available on
        /// the NavigationContext.
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext) {
            // look at the NavigationContext do some initialisation...
        }
    }
}
