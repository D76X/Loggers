using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Modules.TestModules.ModuleD.Names;
using ModuleD.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Windows;

namespace ModuleD.ViewModels {
    public class ViewAViewModel : 
        ViewModelBase,
        IViewAViewModel,
        IRegionManagerAware,
        ICreateRegionManagerScope {        

        private bool isClosable = true;
        private bool isConfirmNavigationActive = false;

        public ViewAViewModel() {

            this.NavigateCommand = new DelegateCommand(Navigate);
        }
        
        public string Title => nameof(ViewAViewModel);

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

        public DelegateCommand NavigateCommand { get; private set; }        

        public override bool IsNavigationTarget(NavigationContext navigationContext) {

            var sender = navigationContext.Parameters[NavigationRequestParametersBase.KeyNavigationRequestedBy];
            // use the params here to decide whether to return true or false.
            // true to tell prism to reuse this view model and the corresponding view to satisfy the navigation request.
            // false to tell prism to satisfy the navigation request by creating a new view and view model. 

            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {

            var navigationRequestedBy = navigationContext.Parameters[NavigationRequestParametersBase.KeyNavigationRequestedBy];
            // do something with this information
        }

        public bool IsConfirmNavigationActive {
            get { return this.isConfirmNavigationActive; }
            set { SetProperty(ref this.isConfirmNavigationActive, value); }
        }

        public override void ConfirmNavigationRequest(
            NavigationContext navigationContext,
            Action<bool> continuationCallback) {

            var isNavigationConfirmed = false;

            if (this.IsConfirmNavigationActive) {

                MessageBoxResult messageResult = MessageBox.Show("Confirm navigation?", "Navigation", MessageBoxButton.YesNoCancel);

                if (messageResult == MessageBoxResult.Yes) {
                    // save or persist state or do something before Navigation accurs...
                    isNavigationConfirmed = true;
                }
                else {
                    isNavigationConfirmed = false;
                }
            }
            else {
                isNavigationConfirmed = true;
            }  

            isNavigationConfirmed = isNavigationConfirmed && this.IsClosable;

            continuationCallback(isNavigationConfirmed);
        }

        private void Navigate() {

            var parameters = new NavigationParameters();
            parameters.Add(NavigationRequestParametersBase.KeyNavigationRequestedBy, this);

            // do not check for null on RegionManager it must be set when a navigation 
            // request has been made to the VM
            this.RegionManager.RequestNavigate(
                RegionNames.RegionContent,
                ViewNamesModuleD.ViewB, 
                parameters);
        }        
    }
}
