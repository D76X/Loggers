using Microsoft.Practices.ServiceLocation;
using Prism.Common;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary>
    /// Implementation of <see cref="IRegionNavigationContentLoader"/> that relies on a <see cref="IServiceLocator"/>
    /// to create new views when necessary.
    /// 
    /// This is a modified implementation from 
    /// https://github.com/PrismLibrary/Prism/blob/master/Source/Wpf/Prism.Wpf/Regions/RegionNavigationContentLoader.cs
    /// 
    /// The original purpose of this class is to implement IRegionNavigationContentLoader. When a region is navigated 
    /// to this implementatiuon loads the content into it after examining the provided navigation context. This class 
    /// alters only slightly the original in order to allow Child Navigation within the TabControl when it is used as 
    /// a region.
    /// 
    /// The problem is described here 
    /// https://app.pluralsight.com/player?course=prism-mastering-tabcontrol&author=brian-lagunas&name=prism-mastering-tabcontrol-m4&clip=3&mode=live
    /// 
    /// The solution to the problem is described here.
    /// https://app.pluralsight.com/player?course=prism-mastering-tabcontrol&author=brian-lagunas&name=prism-mastering-tabcontrol-m4&clip=4&mode=live
    /// 
    /// This implementation add supports for scoped regions to the original implementation. 
    /// A view model may implement the custom interface <see cref="ICreateRegionManagerScope"/> and indicate to Prism 
    /// whether it needs to create a scoped region when it is diplayed. If a scoped region is requested one is created
    /// when the view is created and then the view and the view model are tested for IRegionManagerAware so that they
    /// may retain a reference to the newly created scoped region. 
    /// 
    /// Why is this important?
    /// 
    /// The are two problems at play when a TabControl is used as a region in Prism.
    /// 
    /// Problem 1 - avoiding collisions on named region on a the same region manager.        
    /// 
    /// The names of the regions added to a region manager must be unique. If an attempt to add a named region to a 
    /// region manager in which such name already exists is made Prism crashes with an exception.
    /// 
    /// This is crucial when working with the TabControl as a Prism region. In such case any time a new tab is added 
    /// to the TabControl we want a new scoped region to be created for the view that is displayed in 
    /// each tab individual tab. This is necassary when the view in a tab of the tab control defines its own regions.
    /// Lets assume Tab 1 displays ViewA/ViewModelA and that ViewA declares a control that is made into a Prism named 
    /// region using the attached property RegionManager.RegionName="SomeChildRegionName" then when a new tab for the 
    /// same ViewA/ViewModelA is added to the TabControl an additional named child region "SomeChildRegionName" should
    /// also be added to the RegionManager of the TabControl, but this is not possible as the names of the region in 
    /// a region manager must be unique.
    ///    
    /// One solution to this specific problem is to make sure that each tab in a TabControl region has its own region 
    /// manager so that when a new instance of a tab with the same  ViewA/ViewModelA is created the "SomeChildRegionName"
    /// is registered with the scoped region manager of the tab and not of the TabControl. In order to make sure that
    /// each TabItem in a TabControl displays a view with a scoped region we need to do something at the point when a
    /// new view is added to the TabControl into one of its TabItems. This happens in the implementation of the method
    /// IRegionNavigationContentLoader.LoadContent below. Here it is checked for ICreateRegionManagerScope and if the
    /// view or view model implements it a decision can be made whether or not the new view needs a scoped region. 
    /// All the view models of views that should be displayed in a TabControl should support ICreateRegionManagerScope
    /// and request a scoped region especially if they have child regions defined in them and multiple instances of the 
    /// same view should be diplayed in the same TabControl.
    /// 
    /// Problems 2 - navigation from a child view within a tab of a tab control.
    /// 
    /// We need the the view or/and the view model of the view of a tab item in TabControl control to retain a reference
    /// to the scoped region manager of the tab item in order to properly issue navigation requests within it. The solution 
    /// to this problem is the custom Prism infrastructure based on IRegionManagerAware and RegionManagerAwareBehavior to
    /// which it is referred to for all the details.
    /// 
    /// </summary>
    public class ScopedRegionNavigationContentLoader : IRegionNavigationContentLoader {

        private readonly IServiceLocator serviceLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedRegionNavigationContentLoader"/> class with a service locator.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        public ScopedRegionNavigationContentLoader(IServiceLocator serviceLocator) {
            this.serviceLocator = serviceLocator;
        }

        /// <summary>
        /// Gets the view to which the navigation request represented by <paramref name="navigationContext"/> applies.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="navigationContext">The context representing the navigation request.</param>
        /// <returns>
        /// The view to be the target of the navigation request.
        /// </returns>
        /// <remarks>
        /// If none of the views in the region can be the target of the navigation request, a new view
        /// is created and added to the region.
        /// </remarks>
        /// <exception cref="ArgumentException">when a new view cannot be created for the navigation request.</exception>
        public object LoadContent(IRegion region, NavigationContext navigationContext) {
            if (region == null)
                throw new ArgumentNullException(nameof(region));

            if (navigationContext == null)
                throw new ArgumentNullException(nameof(navigationContext));

            string candidateTargetContract = this.GetContractFromNavigationContext(navigationContext);

            var candidates = this.GetCandidatesFromRegion(region, candidateTargetContract);

            var acceptingCandidates =
                candidates.Where(
                    v => {
                        var navigationAware = v as INavigationAware;
                        if (navigationAware != null && !navigationAware.IsNavigationTarget(navigationContext)) {
                            return false;
                        }

                        var frameworkElement = v as FrameworkElement;
                        if (frameworkElement == null) {
                            return true;
                        }

                        navigationAware = frameworkElement.DataContext as INavigationAware;
                        return navigationAware == null || navigationAware.IsNavigationTarget(navigationContext);
                    });


            var view = acceptingCandidates.FirstOrDefault();

            if (view != null) {
                return view;
            }

            view = this.CreateNewRegionItem(candidateTargetContract);

            // the global region manager or a scoped region manager depending on the third param 
            var scopedRegionManager = region.Add(view, null, this.CreateRegionManagerScope(view));

            // sets the view's RegionManager to what has been created when the view was added to 
            // the region
            RegionManagerAware.SetRegionManagerAware(view, scopedRegionManager);

            return view;
        }

        /// <summary>
        /// Tests whether the object needs a scoped region. 
        /// </summary>
        /// <param name="view">The object on which we need to know whether a scope region must be created</param>
        /// <returns>True when the object is a view or a view model supporting ICreateRegionManagerScope</returns>
        private bool CreateRegionManagerScope(object view) {

            bool createRegionManagerScope = false;

            var viewHasScopeRegion = view as ICreateRegionManagerScope;

            if (viewHasScopeRegion != null) {

                createRegionManagerScope = viewHasScopeRegion.CreateRegionManagerScope;
            }

            // if it is a view grab it VM (DataContext) and do the same as above
            var frameworkElement = view as FrameworkElement;

            if (frameworkElement != null) {

                var hasScopeRegionDataContext = frameworkElement.DataContext as ICreateRegionManagerScope;

                if (hasScopeRegionDataContext != null) {

                    createRegionManagerScope = hasScopeRegionDataContext.CreateRegionManagerScope;
                }
            }

            return createRegionManagerScope;
        }

        /// <summary>
        /// Provides a new item for the region based on the supplied candidate target contract name.
        /// </summary>
        /// <param name="candidateTargetContract">The target contract to build.</param>
        /// <returns>An instance of an item to put into the <see cref="IRegion"/>.</returns>
        protected virtual object CreateNewRegionItem(string candidateTargetContract) {
            object newRegionItem;
            try {
                newRegionItem = this.serviceLocator.GetInstance<object>(candidateTargetContract);
            }
            catch (ActivationException e) {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, @"Cannot create navigation target", candidateTargetContract),
                    e);
            }
            return newRegionItem;
        }

        /// <summary>
        /// Returns the candidate TargetContract based on the <see cref="NavigationContext"/>.
        /// </summary>
        /// <param name="navigationContext">The navigation contract.</param>
        /// <returns>The candidate contract to seek within the <see cref="IRegion"/> and to use, if not found, when resolving from the container.</returns>
        protected virtual string GetContractFromNavigationContext(NavigationContext navigationContext) {
            if (navigationContext == null) throw new ArgumentNullException(nameof(navigationContext));

            var candidateTargetContract = UriParsingHelper.GetAbsolutePath(navigationContext.Uri);
            candidateTargetContract = candidateTargetContract.TrimStart('/');
            return candidateTargetContract;
        }

        /// <summary>
        /// Returns the set of candidates that may satisfiy this navigation request.
        /// </summary>
        /// <param name="region">The region containing items that may satisfy the navigation request.</param>
        /// <param name="candidateNavigationContract">The candidate navigation target as determined by <see cref="GetContractFromNavigationContext"/></param>
        /// <returns>An enumerable of candidate objects from the <see cref="IRegion"/></returns>
        protected virtual IEnumerable<object> GetCandidatesFromRegion(IRegion region, string candidateNavigationContract) {
            if (region == null)
                throw new ArgumentNullException(nameof(region));

            return region.Views.Where(v =>
                string.Equals(v.GetType().Name, candidateNavigationContract, StringComparison.Ordinal) ||
                string.Equals(v.GetType().FullName, candidateNavigationContract, StringComparison.Ordinal));
        }
    }
}

