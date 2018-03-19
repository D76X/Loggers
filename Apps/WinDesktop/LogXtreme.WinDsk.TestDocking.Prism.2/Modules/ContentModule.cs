using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Unity;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using LogXtreme.WinDsk.TestDocking.Prism.ViewModels;
using LogXtreme.WinDsk.TestDocking.Prism.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.TestDocking.Prism.Modules {

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

            // Compose Views into the Shell when the module is initialized
            regionManager.RegisterViewWithRegion(RegionNames.RegionContent, typeof(ContentView));

            // Register the View for navigation with Prism as the content view is navigated to
            // when a second shell is created.
            this.container.RegisterTypeForNavigation<ContentView>();
        }
    }
}
