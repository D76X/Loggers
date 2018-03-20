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
        IRegionManagerAware,
        IDisposable {

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
        private IRegionManager scopedRegionManager;
        public IRegionManager RegionManager {
            get => this.scopedRegionManager;
            set => this.scopedRegionManager = value;
        }

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

        private void Navigate(string navigationUri) {

            var parameters = new NavigationParameters();
            parameters.Add(NavigationRequestParametersBase.KeyNavigationRequestedBy, this);

            var navigationAddress = this.ExtractTargetRegionFromNavigationUri(navigationUri);
            var uri = new Uri(navigationAddress.ViewName, UriKind.Relative);
            var regionName = navigationAddress.RegionName ?? RegionNames.RegionContent;

            this.RegionManager.RequestNavigate(
                regionName,
                uri,
                NavigateComplete,
                parameters);
        }        

        private void NavigateComplete(NavigationResult navigationResult) {

            this.LastUriNavigatedTo = navigationResult.Context.Uri.ToString();
        }

        private (string RegionName, string ViewName) ExtractTargetRegionFromNavigationUri(string navigationUri) {

            string[] split = navigationUri.Split('|');
            int length = split.Length;

            if (length > 2 || length == 0) {
                throw new ArgumentException($"{navigationUri} is not a valid URI");
            };

            if(length == 1) { return (RegionName: null, ViewName: split[0]); }

            return (RegionName: split[0], ViewName: split[1]);
        }

        #region IDisposable

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {

            if (disposing) {
                // dispose of subcriptions, etc.               
            }
        }
        #endregion
    }
}
