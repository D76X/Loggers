﻿using System;
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
        IViewModelBase, 
        IConfirmNavigationRequest, 
        IRegionMemberLifetime {

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
        /// true that view is navigated to and beocomes the active view. If all the views 
        /// implementing INavigationAware in the target region retun false from their
        /// IsNavigationTarget a new instance of the requested type is resolved by the DI
        /// and becomes the acive view.
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns>TRUE to activate the view, FALSE otherwise</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) {
            // check the NavigationContext
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
        /// occours.
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) {
            // save data and state...
        }

        /// <summary>
        /// 
        /// INavigationAware.OnNavigatedtO
        /// 
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