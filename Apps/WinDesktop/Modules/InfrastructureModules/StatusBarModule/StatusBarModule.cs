using LogXtreme.WinDsk.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using StatusBarModule.Interfaces;
using StatusBarModule.ViewModels;
using StatusBarModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusBarModule {

    [Module(ModuleName = nameof(StatusBarModule))]
    public class StatusBarModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public StatusBarModule(
            IUnityContainer container,
            RegionManager regionManager) {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize() {

            // Register types
            this.container.RegisterType<IStatusBarViewModel, StatusBarViewModel>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            regionManager.RegisterViewWithRegion(RegionNames.RegionStatusBar, typeof(StatusBarView));
        }
    }
}
