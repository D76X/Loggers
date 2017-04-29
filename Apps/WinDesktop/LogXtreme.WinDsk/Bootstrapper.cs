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

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings() {

            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<RegionAdapterStackPanel>());

            return mappings;

        }

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
