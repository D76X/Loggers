using System;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using ModuleC.Views;
using ModuleC.Interfaces;
using ModuleC.ViewModels;

namespace LogXtreme.WinDsk.Modules.TestModules.ModuleC {
    public class TestModuleC : IModule {

        IUnityContainer container;

        public TestModuleC(
            IUnityContainer container) {

            this.container = container;
        }

        public void Initialize() {

            // register the views for the module
            this.container.RegisterType<IViewA, ViewA>();
            this.container.RegisterType<IViewAViewModel, ViewAViewModel>();

            this.container.RegisterType<IViewB,ViewB>();
            this.container.RegisterType<IViewBViewModel, ViewBViewModel>();
        }
    }
}
