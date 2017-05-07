using Microsoft.Practices.Unity;
using ModuleD.Interfaces;
using ModuleD.ViewModels;
using ModuleD.Views;
using Prism.Modularity;

namespace LogXtreme.WinDsk.Modules.TestModules.ModuleD {

    public class TestModuleD : IModule {

        private IUnityContainer container;

        public TestModuleD(
            IUnityContainer container) {

            this.container = container;
        }

        public void Initialize() {

            // do not use use view discovery to inject views in a TabControl
            // this.regionManager.RegisterViewWithRegion(RegionNamesModuleD.RegionTabview, typeof(ViewA));

            // register the ViewModels with the container
            // with the view first approach the view takes a reference on the interface of the corresponding
            // ViewModel in its constructor in the code behind and the DI resolves the ViewModel when the 
            // view is requested
            this.container.RegisterType<ITabviewViewModel, TabviewViewModel>();
            this.container.RegisterType<IViewAViewModel, ViewAViewModel>();
            this.container.RegisterType<IViewBViewModel, ViewBViewModel>();

            // resiter the view with the container so that it is possible to navigate to them
            this.container.RegisterType(typeof(object), typeof(TabView), "Tabview");
            this.container.RegisterType(typeof(object), typeof(ViewA), "ViewA");
            this.container.RegisterType(typeof(object), typeof(ViewB), "ViewB");
        }
    }
}
