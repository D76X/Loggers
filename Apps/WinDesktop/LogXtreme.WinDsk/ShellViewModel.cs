using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System;

namespace LogXtreme.WinDsk {
    public class ShellViewModel : ViewModelBase, IShellViewModel {

        private IRegionManager regionManager;
        private IShellService shellService;

        private int shellId;        

        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand<string> SaveSessionCommand { get; private set; }

        public ShellViewModel(
            IRegionManager regionManager, 
            IShellService shellService) {

            this.regionManager = regionManager;
            this.shellService = shellService;                  

            this.OpenShellCommand = new DelegateCommand<string>(OpenShell);
            this.NavigateCommand = new DelegateCommand<string>(Navigate);

            this.SaveSessionCommand = new DelegateCommand<string>(SaveSession, CanSaveSession);
            CommandsGlobal.SaveSession.RegisterCommand(SaveSessionCommand);

            this.ShellId = this.shellService.RegisterShellId();
        } 
        
        public int ShellId {
            get { return this.shellId; }
            private set { this.SetProperty(ref this.shellId, value); }
        }    

        private void SaveSession(string obj) {

            //..do the saving...ISessionService.SaveSession()....
        }

        private bool CanSaveSession(string arg) {
            return !(this.shellService.RegisteredShellCount % 2 == 0);
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
