using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Modules.TestModules.ModuleD.Names;
using ModuleD.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System;

namespace ModuleD.ViewModels {

    /// <summary>
    /// View Model that can hold a reference to a scoped region manager if one is created 
    /// for the corresponding view. The View Model tells the implementation of the interface
    /// IRegionNavigationContentLoader that a scoped region manager must be created for the 
    /// corresponding view when a request navigation happens by returning true fpr the prop
    /// CreateRegionManagerScope. The implementation of IRegionNavigationContentLoader then 
    /// sets the scoped region manager on the view and/or the view model when it is found 
    /// that they are implentations of IRegionManagerAware.
    /// </summary>
    public class ViewBViewModel : 
        ViewModelBase,
        IViewBViewModel,
        IRegionManagerAware,
        ICreateRegionManagerScope {   
        
        private bool isClosable = true;

        /// <summary>
        /// A reference to the region manager of the shell in which the view 
        /// of this view model in displayed.
        /// </summary>
        public ViewBViewModel() {       
            this.NavigateCommand = new DelegateCommand(Navigate);
        }        

        public string Title => nameof(ViewBViewModel);

        public bool IsClosable {
            get { return this.isClosable; }
            set { SetProperty(ref this.isClosable, value); }
        }

        /// <summary>
        /// Property defined on supported interface IRegionManagerAware.
        /// This is necessary to support displaying the view in TabItems.
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        /// <summary>
        /// Indicates to the implemenation of IRegionNavigationContentLoader
        /// whether a scope region must be created when a view for this VM
        /// is instantiated as a result of a navigation request.
        /// This is necessary to support displaying the view in TabItems.
        /// </summary>
        public bool CreateRegionManagerScope => true;

        public override bool IsNavigationTarget(NavigationContext navigationContext) {

            var sender = navigationContext.Parameters[NavigationRequestParametersBase.KeyNavigationRequestedBy];
            // use the params here to decide whether to return true or false.
            // true to tell prism to reuse this view model and the corresponding view to satisfy the navigation request.
            // false to tell prism to satisfy the navigation request by creating a new view and view model. 

            return false;
        }

        public DelegateCommand NavigateCommand { get; private set; }

        public override void OnNavigatedTo(NavigationContext navigationContext) {

            var navigationRequestedBy = navigationContext.Parameters[NavigationRequestParametersBase.KeyNavigationRequestedBy];
            // do something with this information
        }

        public override void ConfirmNavigationRequest(
            NavigationContext navigationContext,
            Action<bool> continuationCallback) {

            continuationCallback(this.IsClosable);
        }

        private void Navigate() {

            var parameters = new NavigationParameters();
            parameters.Add(NavigationRequestParametersBase.KeyNavigationRequestedBy, this);

            this.RegionManager.RequestNavigate(
                RegionNames.RegionContent,
                ViewNamesModuleD.ViewA,
                parameters);
        }        
    }
}
