using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Modules.TestModules.ModuleD.Names;
using Microsoft.Practices.Unity;
using ModuleD.Interfaces;
using ModuleD.Navigation;
using Prism.Commands;
using Prism.Regions;
using System;

namespace ModuleD.ViewModels {
    public class TabviewViewModel :
        ViewModelBase,
        ITabviewViewModel,
        IRegionManagerAware {

        private readonly IUnityContainer container;

        public DelegateCommand<string> NavigateCommand { get; private set; }

        /// <summary>
        /// A reference to the region manager of the shell in which the view 
        /// of this view model in displayed.
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        public TabviewViewModel(IUnityContainer container) {
            this.container = container;
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {

            var navigationRequestedBy = navigationContext.Parameters[NavigationRequestParametersBase.KeyNavigationRequestedBy];
            // do something with this information
        }

        private void Navigate(string viewName) {

            var parameters = new NavigationParameters();
            parameters.Add(NavigationRequestParametersBase.KeyNavigationRequestedBy, this);

            this.RegionManager.RequestNavigate(
                RegionNamesModuleD.RegionTabview,
                new Uri(viewName, UriKind.Relative), 
                parameters);
        }       
    }
}
