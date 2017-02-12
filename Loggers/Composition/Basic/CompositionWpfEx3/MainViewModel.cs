using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfMef.Interfaces;

namespace CompositionWpfEx3 {
    public class MainViewModel {

        [ImportMany]
        public IEnumerable<IModule> Modules { get; set; }

        public MainViewModel() {

            var catalog = new AggregateCatalog(
            new AssemblyCatalog(Assembly.GetExecutingAssembly()),
            new DirectoryCatalog("./modules"));
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}
