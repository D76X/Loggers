using ContentModule.Interfaces;
using ContentModule.ViewModels;
using ContentModule.Views;
using LogXtreme.WinDsk.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace ContentModule {

    [Module(ModuleName = nameof(ContentModule))]
    public class ContentModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public ContentModule(
            IUnityContainer container,
            RegionManager regionManager) {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize() {

            // Register types
            this.container.RegisterType<IContentViewModel, ContentViewModel>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            regionManager.RegisterViewWithRegion(RegionNames.RegionContent, typeof(ContentView));
        }
    }
}
