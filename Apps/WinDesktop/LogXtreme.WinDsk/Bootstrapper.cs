using LogXtreme.WinDsk.Infrastructure.Prism;
using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using System.Windows.Controls;
using Prism.Logging;
using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Services;
using LogXtreme.WinDsk.Interfaces;
using LogXtreme.WinDsk.ViewModels;

namespace LogXtreme.WinDsk {

    public class Bootstrapper: UnityBootstrapper {

        protected override ILoggerFacade CreateLogger() {
            return base.CreateLogger();
        }

        protected override void ConfigureModuleCatalog() {            

            base.ConfigureModuleCatalog();
        }

        /// <summary>
        /// 
        /// Set up the container by registering the default Prism services with the container.
        /// Register your application services with the container (before loading the app modules).
        /// 
        /// Refs
        /// https://github.com/PrismLibrary/Prism/issues/155
        /// </summary>
        protected override void ConfigureContainer() {

            // the call to the base class method registers the core Prism services
            base.ConfigureContainer();

            // replace Prism services with custom services
            //...

            // register the application services
            RegisterTypeIfMissing(typeof(IShellService), typeof(ShellService), true);

            // register view and view models with their interfaces
            Container.RegisterType<IShellView, Shell>();
            Container.RegisterType<IShellViewModel, ShellViewModel>();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings() {

            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<RegionAdapterStackPanel>());

            return mappings;

        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors() {

            IRegionBehaviorFactory behaviors = base.ConfigureDefaultRegionBehaviors();
            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            return behaviors;

        }

        protected override void RegisterFrameworkExceptionTypes() {
            base.RegisterFrameworkExceptionTypes();
        }

        /// <summary>
        /// Use the container to resolve the shell.
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell() {

            // use the shell service to create an instance of the shell so that it
            // can properly retain a reference to its scoped region manager. In the
            // case of the first shell created by the application the region manager
            // to register with the shell must be the global region manager.
            var shellService = Container.Resolve<IShellService>();
            var globalRegionManager = Container.Resolve<IRegionManager>();
            return shellService.CreateShell(globalRegionManager);
        }

        protected override void InitializeShell() {

            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
            var shellService = Container.Resolve<IShellService>();
            shellService.ShowShell(Shell);
        }

        protected override void InitializeModules() {
            base.InitializeModules();
        }
    }
}
