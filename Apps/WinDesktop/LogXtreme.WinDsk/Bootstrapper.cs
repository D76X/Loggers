using Microsoft.Practices.Unity;
using Prism.Unity;
using System;
using System.Windows;
using LogXtreme.WinDsk.Modules.TestModules;
using Prism.Modularity;
using Prism.Regions;
using System.Windows.Controls;
using LogXtreme.WinDsk.Infrastructure;
using ModuleB;

namespace LogXtreme.WinDsk {
    public class Bootstrapper: UnityBootstrapper {        

        protected override void ConfigureModuleCatalog() {

            // Type testModule = typeof(TestModuleA);
            Type testModule = typeof(TestModuleB);

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
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings() {

            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<RegionAdapterStackPanel>());

            return mappings;

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
            Application.Current.MainWindow = (Window)Shell;                
            Application.Current.MainWindow.Show();
        }        
    }
}
