using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using WpfEmf.Interfaces;

namespace CompositionWpfEx4 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        //[ImportMany("ResourceDictionaries", typeof(ResourceDictionary))]
        private IEnumerable<ResourceDictionary> _resourceDictionaries;

        private IEnumerable<WorkSpaceViewModel> _viewModels;

        [ImportMany]
        private IEnumerable<IBasePlugin> _plugins;


        public App() {

            var catalog = new AggregateCatalog(
                new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                new DirectoryCatalog("./modules"));

            var container = new CompositionContainer(catalog);

            container.ComposeParts(this);

            var x = this._plugins;
        }
    }
}
