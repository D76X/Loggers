using System;
using LogXtreme.WinDsk.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using LogXtreme.WinDsk.Interfaces;

namespace LogXtreme.WinDsk {
    public class ShellViewModel : BindableBase, IShellViewModel {

        IRegionManager regionManager;
        IShellService shellService;

        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }        

        public ShellViewModel(
            IRegionManager regionManager, 
            IShellService shellService) {

            this.regionManager = regionManager;
            this.shellService = shellService;                  

            this.OpenShellCommand = new DelegateCommand<string>(OpenShell);
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
        }


        private void OpenShell(string viewName) {

            this.shellService.ShowShell();
        }

        private void Navigate(string viewName) {

            //this.regionManager.RequestNavigate(RegionNames.RegionContent, viewName);
            this.regionManager.RequestNavigate(RegionNames.RegionContent, new Uri(viewName, UriKind.Relative));
        }
    }
}
