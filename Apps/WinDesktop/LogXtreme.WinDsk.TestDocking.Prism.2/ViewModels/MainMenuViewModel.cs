using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace LogXtreme.WinDsk.TestDocking.Prism.ViewModels {

    public class MainMenuViewModel : 
        BindableBase, 
        IMainMenuViewModel, 
        IDisposable {

        private IShellService shellService;
        private INavigationService navigatioService;

        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainMenuViewModel(
            IShellService shellService,
            INavigationService navigatioService) {

            this.shellService = shellService;

            this.OpenShellCommand = new DelegateCommand<string>(OpenShell);
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void OpenShell(string viewName) {
            
        }

        private void Navigate(string viewName) {
            
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

