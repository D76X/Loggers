using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Interfaces;
using Prism.Commands;
using Prism.Regions;
using System;

namespace LogXtreme.WinDsk {
    public class ShellViewModel : 
        ViewModelBase, 
        IShellViewModel, 
        IRegionManagerAware {

        private IShellService shellService;

        private int shellId;
        private string statusMessage;

        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand<string> SaveSessionCommand { get; private set; }

        public ShellViewModel(IShellService shellService) {

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

        /// <summary>
        /// This properties holds a reference to the scoped region manager for the instance of 
        /// the Shell supported by this instance of the ShellViewModel.
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        public string StatusMessage {
            get { return this.statusMessage; }
            set { SetProperty(ref this.statusMessage, value); }
        }

        private void SaveSession(string obj) {

            //..do the saving...ISessionService.SaveSession()....
        }

        private bool CanSaveSession(string arg) {
            return !(this.shellService.RegisteredShellCount % 2 == 0);
        }

        private void OpenShell(string viewName) {

            this.shellService.ShowShell(viewName);
        }

        private void Navigate(string viewName) {

            this.RegionManager.RequestNavigate(
                RegionNames.RegionContent, 
                new Uri(viewName, UriKind.Relative),
                NavigateComplete);
        }

        private void NavigateComplete(NavigationResult navigationResult) {
            this.StatusMessage = navigationResult.Context.Uri.ToString();
        }
    }
}
