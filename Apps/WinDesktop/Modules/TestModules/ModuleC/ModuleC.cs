using Microsoft.Practices.Unity;
using ModuleC.Interfaces;
using ModuleC.Views;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.Modules.TestModules.ModuleC {
    public class TestModuleC : IModule {

        private IUnityContainer container;
        private IRegionManager regionManager;


        public TestModuleC(
            IUnityContainer container, 
            IRegionManager regionManager) {

            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize() {

            // register the views for the module
            // rgister the view models for the module
            this.container.RegisterType<IViewA, ViewA>();
            this.container.RegisterType<IViewB,ViewB>();

            // VIEW DISCOVERY 

            // Use View Discovery to register the views with the region names                        
            // For example            
            // Below we instruct the global region manager that when a region of name RegionNames.RegionContent
            // is displayed an instance of ViewA must be injected it into it. Notice that this might also imply 
            // that a backing ViewAViewModel is also instantiated.              
            // this.regionManager.RegisterViewWithRegion(RegionNames.RegionContent, typeof(ViewA));

            // VIEW INJECTION
            
            // Use View Injection to have more control over how and when a view is created and displayed
            // var viewAviewModel = this.container.Resolve<IViewAViewModel>();
            
            // do something with the view model...
            //this.regionManager.Regions[RegionNames.RegionContent].Add(viewAviewModel.View);
            
            // OR FOR EVEN MORE CONTROL GRAB A REFERENCE TO THE REGION via IRegion
            
            // by holding on a IRegion reference all the methods defined on the IRegion interface become available
            // IRegion region = this.regionManager.Regions[RegionNames.RegionContent];
            // region.Add(viewAviewModel.View);
        }
    }
}
