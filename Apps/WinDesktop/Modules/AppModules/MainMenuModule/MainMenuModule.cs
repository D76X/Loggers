﻿using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.MainMenuModule.Interfaces;
using LogXtreme.WinDsk.MainMenuModule.ViewModels;
using LogXtreme.WinDsk.MainMenuModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.Modules {

    [Module(ModuleName = nameof(MainMenuModule))]
    public class MainMenuModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public MainMenuModule(
            IUnityContainer container,
            RegionManager regionManager
            ) {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize() {

            // Register types
            this.container.RegisterType<IMainMenuViewModel, MainMenuViewModel>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            regionManager.RegisterViewWithRegion(RegionNames.RegionMainMenu, typeof(MainMenuView));
        }
    }
}
