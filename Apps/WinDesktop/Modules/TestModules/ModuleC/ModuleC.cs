using LogXtreme.WinDsk.Infrastructure;
using Microsoft.Practices.Unity;
using ModuleC.Interfaces;
using ModuleC.ViewModels;
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

            // THE VIEW MODELS FOR THE VIEWS MUST BE REGISTERED WITH THE CONTAINER
            this.container.RegisterType<IViewAViewModel, ViewAViewModel>();
            this.container.RegisterType<IViewBViewModel, ViewBViewModel>();

            // THIS REGISTRATION PATTERN FOR THE VIEWS IS NOT SUITABLE FOR NAVIGATION
            //this.container.RegisterType<IViewA, ViewA>();
            //this.container.RegisterType<IViewB,ViewB>();            

            // IF THE VIEW NAVIGATION PATTERN IN PRISM IS GOING TO BE USED THEN REGISTER THE VIEWS AS BELOW
            
            // EITHER
            //this.container.RegisterType<object, ViewA>("ViewA");
            //this.container.RegisterType<object, ViewB>("ViewB");

            // OR 
            this.container.RegisterType(typeof(object), typeof(ViewA), "ViewA");
            this.container.RegisterType(typeof(object), typeof(ViewB), "ViewB");

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

            // IF THIS IS THE MAIN MODULE OF THE APPLICATION AND IT CONTAINS A MAIN VIEW YOU MIGHT WANT TO SHOW IT 
            // BY DEFAULT            
            regionManager.RequestNavigate(RegionNames.RegionContent, "ViewA");
        }
    }
}
