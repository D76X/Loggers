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
/// </summary>
namespace Modules {
    class Program {
        static void Main(string[] args) {

            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);   
            var menu = new Menu();
            container.ComposeParts(menu);
            menu.OptionList();
            Console.ReadLine();
        }
    }
}
