using Microsoft.Practices.Unity;
using Prism.Unity;
using System;
using System.Windows;
using LogXtreme.WinDsk.Modules.TestModules;
using Prism.Modularity;
using Prism.Regions;
using System.Windows.Controls;
using LogXtreme.WinDsk.Infrastructure;

namespace LogXtreme.WinDsk {
    public class Bootstrapper: UnityBootstrapper {

        protected override DependencyObject CreateShell() {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;                
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog() {

            Type typeModuleA = typeof(TestModuleA);

            ModuleCatalog.AddModule(new ModuleInfo() {
                ModuleName = typeModuleA.Name,
                ModuleType = typeModuleA.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });

            base.ConfigureModuleCatalog();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings() {

            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<RegionAdapterStackPanel>());

            return mappings;

        }
    }
}
