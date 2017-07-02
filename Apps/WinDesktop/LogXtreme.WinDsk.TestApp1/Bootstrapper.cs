using Microsoft.Practices.Unity;
using Prism.Unity;
using System;
using System.Windows;
using Prism.Modularity;
using Prism.Regions;
using System.Windows.Controls;
using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Modules.TestModules.ModuleA;
using LogXtreme.WinDsk.Modules.TestModules.ModuleB;
using LogXtreme.WinDsk.Modules.TestModules.ModuleC;
using LogXtreme.WinDsk.Interfaces;
using LogXtreme.WinDsk.Modules.TestModules.ModuleD;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;

namespace LogXtreme.WinDsk {
    public class Bootstrapper: UnityBootstrapper {        

        protected override void ConfigureModuleCatalog() {

            // Type testModule = typeof(TestModuleA);
            // Type testModule = typeof(TestModuleB);
            // Type testModule = typeof(TestModuleC);
            Type testModule = typeof(TestModuleD);

            ModuleCatalog.AddModule(new ModuleInfo() {
                ModuleName = testModule.Name,
                ModuleType = testModule.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });

            base.ConfigureModuleCatalog();
        }

        /// <summary>
        /// 
        /// Set up the container by registering the default Prism srvices with the container.
        /// Register your application services with the container (before loading the app modules).
        /// 
        /// Refs
        /// https://github.com/PrismLibrary/Prism/issues/155
        /// </summary>
        protected override void ConfigureContainer() {

            // the call to the base class method registers the core Prism services
            base.ConfigureContainer();

            // replace Prism services with custom services
            Container.RegisterType<IRegionNavigationContentLoader, ScopedRegionNavigationContentLoader>(new ContainerControlledLifetimeManager());

            // now register your application services

            // RegisterTypeIfMissing(typeof(IMyService), typeof(MyService), true);
            // this.Container.RegisterInstance<CallbackLogger>(this.callbackLogger);

            Container.RegisterType<IShellService, ShellService>(new ContainerControlledLifetimeManager());

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
    }
}
