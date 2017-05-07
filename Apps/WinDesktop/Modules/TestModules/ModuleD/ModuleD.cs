using Microsoft.Practices.Unity;
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
            this.container.RegisterType<IViewAViewModel, ViewAViewModel>();

            // resiter the view with the container so that it is possible to navigate to them
            this.container.RegisterType(typeof(object), typeof(TabView), "Tabview");
            this.container.RegisterType(typeof(object), typeof(ViewA), "ViewA");
            this.container.RegisterType(typeof(object), typeof(ViewB), "ViewB");
        }
    }
}
