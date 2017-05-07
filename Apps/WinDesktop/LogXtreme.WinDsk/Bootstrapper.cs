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
            return Container.Resolve<Shell>();            
        }

        protected override void InitializeShell() {

            base.InitializeShell();

            // the first shell created during the application lifetime is instantiated 
            // via the Bootstrapper.CreateShell() and not via our ShellService. In order
            // to get a IRegionManager reference to the RegionManager of the first shell
            // we use RegionManager.GetRegionManager                        
            var firstShellRegionManagerName = RegionManager.GetRegionManager(Shell);

            // Our shell's ViewModel implements IRegionManagerAware and can retain a reference 
            // to its region manager. We use RegionManagerAware.SetRegionManagerAware to do 
            // exaclty that.
            RegionManagerAware.SetRegionManagerAware(Shell, firstShellRegionManagerName);

            Application.Current.MainWindow = (Window)Shell;                            
            Application.Current.MainWindow.Show();            
        }        
    }
}
