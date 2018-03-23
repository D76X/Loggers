using System;
using Prism;
using Prism.Mvvm;
using Prism.Regions;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    /// <summary>
    /// The Base View Model.
    /// 
    /// IConfirmNavigationRequest inherits from INavigationAware.
    /// 
    /// </summary>
    public class ViewModelBase :
        BindableBase,
        IActiveAware,
        IViewModelBase,
        IConfirmNavigationRequest,
        IRegionMemberLifetime {

        private bool isActive;

        /// <summary>
        /// When a Navigation Request is made to the RegionManager and a view becomes the 
        /// active view of a target region the view from which it has been navigated from
        /// becomes inactive and IRegionMemberLifetime.KeepAlive is invoked on it.
        /// When IRegionMemberLifetime.KeepAlive returns false the instance of the view 
        /// and view model are dereferenced and left to the GC. When KeepAlive => true
        /// the view and view model are kept in memory.
        /// </summary>
        public virtual bool KeepAlive => true;

        /// <summary>
        /// IActiveAware.IsActive
        /// Used by Prism to determine whether the view corresponding to this 
        /// view model is the active view in the region it is hosted by. 
        /// Override to implements csutom logic on the view model when the 
        /// state of the view changes from active to non active or viceversa. 
        /// 
        /// Refs
        /// http://prismlibrary.github.io/docs/wpf/Navigation.html
        /// http://briannoyesblog.azurewebsites.net/2009/12/08/detecting-the-active-view-in-a-prism-app/
        /// </summary>
        public virtual bool IsActive {
            get => this.isActive;
            set {

                // do something here

                if (SetProperty<bool>(ref isActive, value)) {

                    this.IsActiveChanged?.Invoke(this, EventArgs.Empty);

                    // or do something here

                    // for example 
                    // unload /load data
                    // store/retrieve state
                    // pack/unpack data
                    // etc.
                }
            }
        }

        /// <summary>
        /// Refs
        /// http://prismlibrary.github.io/docs/wpf/Navigation.html
        /// http://briannoyesblog.azurewebsites.net/2009/12/08/detecting-the-active-view-in-a-prism-app/

        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary> 
        /// IConfirmNavigationRequest inherits from INavigationAware.
        /// 
        /// When a Navigation Request is made to the RegionManager the view and then view 
        /// model of the current active view are checked to determined whether they implement 
        /// the interface IConfirmNavigationRequest. If this is the case the implementation 
        /// of ConfirmNavigationRequest is invoked to confirm to the RegionManager whether 
        /// the navigation should continue or not.
        /// 
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <param name="continuationCallback"></param>
        public virtual void ConfirmNavigationRequest(
            NavigationContext navigationContext,
            Action<bool> continuationCallback) {
            // check NavigationContext...
            // check state and data...
            continuationCallback(true);
        }

        /// <summary>
        /// 
        /// INavigationAware.IsNavigationTarget
        /// 
        /// When a Navigation Request is made to the RegionManager the IsNavigationTarget
        /// of each view instance implementing INavigationAware that exists in the target 
        /// region will be checked. If there is a INavigationAware instance that returns 
        /// true that view is navigated to and becomes the active view. If all the views 
        /// implementing INavigationAware in the target region retun false from their
        /// IsNavigationTarget a new instance of the requested type is resolved by the DI
        /// and becomes the acive view.
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns>TRUE to activate the view, FALSE otherwise</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) {
            // check the NavigationContext for paramters and implement custom logic
            return true;
        }

        /// <summary>
        /// 
        /// INavigationAware.OnNavigatedFrom
        /// 
        /// When a Navigation Request is made to the RegionManager the OnNavigatedFrom
        /// occurs on the view model for the active view implementing INavigationAware. 
        /// In INavigationAware.OnNavigatedFrom state, data or teardown, etc. related 
        /// to the active view can be implemented to take place before navigation to 
        /// another view occurs.
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) {
            // save data and state...
        }

        /// <summary>
        /// 
        /// INavigationAware.OnNavigatedTo
        /// 
        /// When a Navigation Request is made to the RegionManager and a view is the 
        /// selected target of the navigation OnNavigatedTo is invoked. OnNavigatedTo
        /// may be used to execute initialisation and/or use parameter available on
        /// the NavigationContext.
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext) {
            // look at the NavigationContext do some initialisation...
        }
    }
}
