using System;
using LogXtreme.WinDsk.Infrastructure;
using Prism.Commands;
using Prism.Regions;
using ModuleD.Interfaces;
using Microsoft.Practices.Unity;
using LogXtreme.WinDsk.Modules.TestModules.ModuleD.Names;
using ModuleD.Views;

namespace ModuleD.ViewModels {
    public class TabviewViewModel: 
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

        private void Navigate(string viewName) {

            IRegion tabViewRegion = this.RegionManager.Regions[RegionNamesModuleD.RegionTabview];
            var view = this.container.Resolve<ViewA>();
            tabViewRegion.Add(view);
            tabViewRegion.Activate(view);         
        }
    }
}
