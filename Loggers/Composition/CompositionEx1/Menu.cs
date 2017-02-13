using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules {

    public class Menu {

        [ImportMany]
        private IEnumerable<IModule> _modules;

        public void OptionList() {

            foreach (var module in _modules) {
                Console.WriteLine(module.Title);
            }
        }
    }


}
