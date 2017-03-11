using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using WpfEmf.Interfaces;

/// <summary>
/// http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
/// </summary>
namespace CompositionWpfEx4 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
      
        private IEnumerable<ResourceDictionary> resourceDictionaries;
        private IEnumerable<WorkSpaceViewModel> viewModels;

        /// <summary>
        /// This will be set by MEF when the container is composed.
        /// </summary>
        [ImportMany]
        private IEnumerable<IBasePlugin> plugins;

        public IEnumerable<WorkSpaceViewModel> ViewModels => this.viewModels;

        /// <summary>
        /// Entry point of the application where all the MEF set up
        /// for the main parts of the application must happen.
        /// </summary>
        public App() {            

            var catalog = new AggregateCatalog(
                new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                new DirectoryCatalog("./modules"));

            var container = new CompositionContainer(catalog);

            container.ComposeParts(this);

            var rds = new List<ResourceDictionary>();
            var vms = new List<WorkSpaceViewModel>();

            var plugins = this.plugins.OrderBy(p => p.ViewModel.WorkSpaceName);

            foreach (var p in plugins) {

                rds.Add(p.View);
                vms.Add(p.ViewModel);

                // Take the View from the Plugin and Merge it with,
                // our Applications Resource Dictionary.
                Application.Current.Resources.MergedDictionaries.Add(p.View);
            }

            this.resourceDictionaries = rds;
            this.viewModels = vms;
        }
        /// <summary>
        /// Create the main window of the application in Application_Startup
        /// http://www.wpf-tutorial.com/wpf-application/working-with-app-xaml/
        /// 
        /// This allows to pass parameters to the constructor of the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Application_Startup(object sender, StartupEventArgs e) {

            MainWindow wnd = new MainWindow(this.viewModels);
            wnd.Title = "Main Window";
            wnd.Show();
        }
    }
}
