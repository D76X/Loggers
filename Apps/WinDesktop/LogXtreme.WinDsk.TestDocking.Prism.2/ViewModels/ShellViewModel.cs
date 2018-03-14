using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using Prism.Commands;
using Prism.Regions;
using System;

namespace LogXtreme.WinDsk.TestDocking.Prism.ViewModels {

    public class ShellViewModel :
        ViewModelBase,
        IShellViewModel,
        IRegionManagerAware {

        private IShellService shellService;

        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }

        private int id;
        private string lastUriNavigatedTo;

        public ShellViewModel(IShellService shellService) {

            this.shellService = shellService;
            this.Id = this.shellService.ShellCreatedCount;

            this.OpenShellCommand = new DelegateCommand<string>(OpenShell);
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
        }       

        /// <summary>
        /// A reference to the region manager for the shell.        
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        public int Id {
            get { return this.id; }
            private set { this.SetProperty(ref this.id, value); }
        }

        public string LastUriNavigatedTo {
            get { return this.lastUriNavigatedTo; }
            set { SetProperty(ref this.lastUriNavigatedTo, value); }
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

            this.LastUriNavigatedTo = navigationResult.Context.Uri.ToString();
        }        
    }
}
