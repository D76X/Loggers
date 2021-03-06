﻿using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.Modules.Services;
using LogXtreme.WinDsk.Services;
using LogXtreme.WinDsk.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.AvalonDock;

namespace LogXtreme.WinDsk {

    /// <summary>
    /// Refs
    /// </summary>
    public class Bootstrapper: UnityBootstrapper {

        /// <summary>
        /// This method creates the default logger for Prism. 
        /// You may plug in your own logging framework here by just making sure that 
        /// it implements <see cref="ILoggerFacade"/>
        /// </summary>
        /// <returns></returns>
        protected override ILoggerFacade CreateLogger() {
            return base.CreateLogger();
        }

        /// <summary>
        /// Refs
        /// https://msdn.microsoft.com/en-us/library/gg405479(v=pandp.40).aspx
        /// http://prismlibrary.github.io/docs/wpf/Modules.html
        /// https://stackoverflow.com/questions/11921590/app-config-file-and-section-type
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog CreateModuleCatalog() {

            // Prism 6 for WPF no longer supports AggregateModuleCatalog. 
            // Prism 6 for WPF no longer supports XAML catalogs. 

            // In Prim 6 there are only two options left for module catalogs
            // 1-use a DirectoryModuleCatalog
            // return new DirectoryModuleCatalog() { ModulePath = Settings.ModulePath };
            // 2-use App.config 
            // https://stackoverflow.com/questions/11921590/app-config-file-and-section-type
            return new ConfigurationModuleCatalog();
        }

        /// <summary>
        /// Refs
        /// https://msdn.microsoft.com/en-us/library/gg405479(v=pandp.40).aspx
        /// http://prismlibrary.github.io/docs/wpf/Modules.html
        /// </summary>
        protected override void ConfigureModuleCatalog() {            

            base.ConfigureModuleCatalog();

            // add all the modules that must always be present in the application
            // the order in which the modules are added is the order in which they 
            // are loaded into memory.
            #region How to set up the module catalog with fix references to its modules
            //this.ModuleCatalog.AddModule(new ModuleInfo() {
            //    ModuleName = typeof(Modules.MainMenuModule).Name,
            //    ModuleType = typeof(Modules.MainMenuModule).AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.WhenAvailable,
            //});

            //this.ModuleCatalog.AddModule(new ModuleInfo() {
            //    ModuleName = typeof(Modules.ContentModule).Name,
            //    ModuleType = typeof(Modules.ContentModule).AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.WhenAvailable,
            //});

            //this.ModuleCatalog.AddModule(new ModuleInfo() {
            //    ModuleName = typeof(Modules.StatusBarModule).Name,
            //    ModuleType = typeof(Modules.StatusBarModule).AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.WhenAvailable,
            //});

            //this.ModuleCatalog.AddModule(new ModuleInfo() {
            //    ModuleName = typeof(Modules.DeviceTreeModule).Name,
            //    ModuleType = typeof(Modules.DeviceTreeModule).AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.WhenAvailable,
            //});

            //this.ModuleCatalog.AddModule(new ModuleInfo() {
            //    ModuleName = typeof(Modules.DataTreeModule).Name,
            //    ModuleType = typeof(Modules.DataTreeModule).AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.WhenAvailable,
            //});

            //this.ModuleCatalog.AddModule(new ModuleInfo() {
            //    ModuleName = typeof(Modules.DataGridModule).Name,
            //    ModuleType = typeof(Modules.DataGridModule).AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.WhenAvailable,
            //});
            #endregion
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
            Container.RegisterType<IRegionNavigationContentLoader, ScopedRegionNavigationContentLoader>(new ContainerControlledLifetimeManager());

            // register the application services
            RegisterTypeIfMissing(typeof(IShellService), typeof(ShellService), true);
            RegisterTypeIfMissing(typeof(IAvalonDockService), typeof(AvalonDockService), true);

            // register view and view models with their interfaces
            Container.RegisterType<IShellView, Shell>();
            Container.RegisterType<IShellViewModel, ShellViewModel>();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings() {

            // configure all the default RegionAdapters of the Prism library
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();

            // register all the custom RegionAdapters with Prism.
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<RegionAdapterStackPanel>());

            // region adapters for the AvalonDock controls
            mappings.RegisterMapping(
                typeof(DockingManager),
                new RegionAdapterDockingManager(
                    ServiceLocator.Current.GetInstance<RegionBehaviorFactory>(),
                    Container.Resolve<IAvalonDockService>()));

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
