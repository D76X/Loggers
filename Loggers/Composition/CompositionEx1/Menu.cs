using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionEx1 {  

    public class Menu {

        [Import]
        private Module _module;

        public void OptionList() {
            Console.WriteLine(_module.Title);
        }
    }

    
}
