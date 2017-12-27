﻿using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Modules.TestModules.ModuleD.Names;
using ModuleD.Interfaces;
using ModuleD.Navigation;
using Prism.Commands;
using Prism.Regions;
using System;

namespace ModuleD.ViewModels {

    public class ViewBViewModel : 
        ViewModelBase,
        IViewBViewModel,
        IRegionManagerAware,
        ICreateRegionManagerScope {        

        public DelegateCommand NavigateCommand { get; private set; }
        public IRegionManager RegionManager { get; set; }
        public string Title => nameof(ViewBViewModel);

        private bool isClosable = true;

        /// <summary>
        /// A reference to the region manager of the shell in which the view 
        /// of this view model in displayed.
        /// </summary>
        public ViewBViewModel() {       
            this.NavigateCommand = new DelegateCommand(Navigate);
        }

        public bool IsClosable {
            get { return this.isClosable; }
            set { SetProperty(ref this.isClosable, value); }
        }

        public bool CreateRegionManagerScope => true;

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
