using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using Prism.Commands;
using Prism.Regions;
using System;

namespace LogXtreme.WinDsk {
    public class ShellViewModel : 
        ViewModelBase, 
        IShellViewModel, 
        IRegionManagerAware {

        private IShellService shellService;

        private int id;
        private string statusMessage;

        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand<string> SaveSessionCommand { get; private set; }

        public ShellViewModel(IShellService shellService) {

            this.shellService = shellService;
            this.Id = this.shellService.ShellCreatedCount;

            this.OpenShellCommand = new DelegateCommand<string>(OpenShell);
            this.NavigateCommand = new DelegateCommand<string>(Navigate);

            this.SaveSessionCommand = new DelegateCommand<string>(SaveSession, CanSaveSession);
            CommandsGlobal.SaveSession.RegisterCommand(SaveSessionCommand);            
        }

        public int Id {
            get { return this.id; }
            private set { this.SetProperty(ref this.id, value); }
        }

        /// <summary>
        /// A reference to the region manager for the shell.        
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
            return !(this.shellService.ShellCreatedCount % 2 == 0);
        }

        private void OpenShell(string viewName) {

            var shell = this.shellService.CreateShell();
            this.shellService.ShowShell(shell, viewName);
        }

        private void Navigate(string viewName) {

            var parameters = new NavigationParameters();
            parameters.Add(NavigationRequestParametersBase.KeyNavigationRequestedBy, this);
            
            this.RegionManager.RequestNavigate(
                RegionNames.RegionContent,
                new Uri(viewName, UriKind.Relative),
                NavigateComplete,
                parameters);
        }

        private void NavigateComplete(NavigationResult navigationResult) {
            this.StatusMessage = navigationResult.Context.Uri.ToString();
        }
    }
}
