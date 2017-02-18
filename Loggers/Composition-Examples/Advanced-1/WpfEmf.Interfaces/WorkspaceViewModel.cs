using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEmf.Interfaces {
    public abstract class WorkSpaceViewModel: BaseViewModel {

        public String HeaderText { get; set; }
        public override string ToString() {
            return HeaderText;
        }
    }
}
