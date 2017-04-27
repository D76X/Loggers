using LogXtreme.WinDsk.Infrastructure;
using Microsoft.Practices.Unity;
using ModuleB.Views;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleB {
    public class TestModuleB : IModule {

        private RegionManager regionManager;
        private IUnityContainer container;

        public TestModuleB(
            IUnityContainer unityContainer,
            RegionManager regionManager) {

            this.regionManager = regionManager;
            this.container = unityContainer;
        }

        public void Initialize() {

            // Register types

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            // In this test a view ViewA is injected into the Content Region of the Shell
            // However ViewA also holds a child view ViewB within itself 
            // The child view ViweB is injected into the local region RegionChild

            // a region name becomes a key for a region type in the instance of the RegionManager
            this.regionManager.RegisterViewWithRegion(RegionNamesModuleB.RegionChild, typeof(ViewB));

            IRegion contentRegion = this.regionManager.Regions[RegionNames.RegionContent];

            // resolve the view and inject it into its region then activate it 
            var view1 = this.container.Resolve<ViewA>();
            contentRegion.Add(view1);
            contentRegion.Activate(view1);

            // Cannot add the same view twice because any region name must be unique within a RegionManager instance
            // by adding an instance of the same view type to the same region manager the key is duplicated and exc
            // is thrown 

            // var view2 = this.container.Resolve<ViewA>();
            // contentRegion.Add(view2);
            // contentRegion.Activate(view2);
        }
    }
}
