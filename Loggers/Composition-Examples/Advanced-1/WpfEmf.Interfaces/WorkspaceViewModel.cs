using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// http://stackoverflow.com/questions/12204614/load-wpf-ui-into-mvvm-application-from-plug-in-assembly
/// </summary>
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
