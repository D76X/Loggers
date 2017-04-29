using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace LogXtreme.WinDsk {
    public class ShellViewModel : BindableBase {

        IRegionManager regionManager;

        public DelegateCommand<string> OpenShellCommand { get; private set; }

        public ShellViewModel(IRegionManager regionManager) {

            this.regionManager = regionManager;

            this.OpenShellCommand = new DelegateCommand<string>(OpenShell)
        }

        private void OpenShell(string viewName) {
            
        }
    }
}
