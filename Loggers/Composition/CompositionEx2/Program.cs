using Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// http://blogs.msmvps.com/bsonnino/2013/08/31/developing-modular-applications-with-mef/
/// http://stackoverflow.com/questions/834270/visual-studio-post-build-event-copy-to-relative-directory-location
/// </summary>
namespace CompositionEx2 {
    class Program {
        static void Main(string[] args) {

            var catalog = new AggregateCatalog(
                new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                new DirectoryCatalog("./modules"));

            var container = new CompositionContainer(catalog);

            var menu = new Menu();

            container.ComposeParts(menu);

            menu.OptionList();

            Console.ReadKey();
        }
    }
}
