using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEmf.Interfaces {
    public abstract class WorkSpaceViewModel : BindableBase {

        private string headerText;
        public string HeaderText {
            get { return this.headerText; }
            set {
                this.SetProperty(ref this.headerText, value);
            }
        }

        public override string ToString() {
            return HeaderText;
        }
    }
}
