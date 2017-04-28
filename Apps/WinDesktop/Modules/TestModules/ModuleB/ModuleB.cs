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

            // a region name is a key for a region type in an instance of the RegionManager
            // in this case a RegionChild becomes a key for the default global region manager 
            this.regionManager.RegisterViewWithRegion(RegionNamesModuleB.RegionChild, typeof(ViewB));


            // IRegion targetRegion = this.regionManager.Regions[RegionNames.RegionContent];
            IRegion targetRegion = this.regionManager.Regions[RegionNames.RegionToolbar];

            string viewName = null;
            bool createRegionManagerScope = true;

            // resolve the view type ViewA and inject it into the content region then activate it
            // the ViewA host a child view of type ViewB which is resitered as a region on the 
            // global region manager.  
            var view1 = this.container.Resolve<ViewA>();
            targetRegion.Add(view1, viewName, createRegionManagerScope);
            targetRegion.Activate(view1);

            // Despite the fact that the host control for the content region is a StackPanel a second ViewA 
            // cannot be added to the content region because ViewA holds a ViewB instance as its child and 
            // ViewB is registered on the global region manager with the key "RegionChild". Therefore when 
            // it is attempted to add a second instance of ViewA this also causes the global region manager
            // to try to add a second "RegionChild" key which is not allowed and an exception is thrown.  

            // var view2 = this.container.Resolve<ViewA>();
            // contentRegion.Add(view2);
            // contentRegion.Activate(view2);

            var view2 = this.container.Resolve<ViewA>();
            targetRegion.Add(view2, viewName, createRegionManagerScope);
            targetRegion.Activate(view2);
        }
    }
}
