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

        private readonly string workSpaceName;

        public WorkSpaceViewModel(string wsName) {
            this.workSpaceName = wsName;
        }

        public string WorkSpaceName {
            get { return this.workSpaceName; }
        }

        public string ViewModelName {
            get { return this.GetType().Name; }
        }


        public override string ToString() {
            return this.WorkSpaceName+"."+this.ViewModelName ;
        }
    }
}
