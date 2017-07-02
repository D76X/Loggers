using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System.Windows;

namespace LogXtreme.WinDsk.Services {

    /// <summary>
    /// The main purpose of ShellService 
    /// </summary>
    public class ShellService : IShellService {

        private IUnityContainer container;
        private IRegionManager shellServiceRegionManager;

        private int shellCreatedCount;

        public ShellService(
            IUnityContainer container,
            IRegionManager regionManager) {

            this.container = container;
            this.shellServiceRegionManager = regionManager;

        }

        public int ShellCreatedCount => this.shellCreatedCount;

        /// <summary>
        /// Resolve and show a new Shell and set the value of the property Shell.RegionManager to an instance 
        /// of a scoped RegionManager. If a URI to a view is provided it also requests navigation to that view
        /// prior to showing it.
        /// </summary>
        /// <param name="uri">The URI to a view to show</param>
        public DependencyObject CreateShell(IRegionManager regionManager) {

            var shell = this.container.Resolve<Shell>();

            var scopedRegionManager = regionManager == null ?
                this.shellServiceRegionManager.CreateRegionManager() :
                regionManager;

            // set the attached property RegionManager on the Shell to the scoped RegionManger
            RegionManager.SetRegionManager(shell, scopedRegionManager);

            // allow this instance of the shell to retain a reference to its own scoped region manager
            // this will allow the shell to control its on navigation 
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegionManager);

            // do all the initialisation you need on the shell then show it
            this.shellCreatedCount += 1;
            return shell;
        }

        public void ShowShell(DependencyObject shellDependencyObject, string uri = null) {

            var shell = shellDependencyObject as Shell;

            if (shell == null) {
                return;
            }

            var regionManagerAwareShellViewModel = shell.ViewModel as IRegionManagerAware;

            if (regionManagerAwareShellViewModel == null) {
                return;
            }

            var scopedRegionManager = regionManagerAwareShellViewModel.RegionManager;

            if (scopedRegionManager != null && !string.IsNullOrEmpty(uri)) {
                scopedRegionManager.RequestNavigate(RegionNames.RegionContent, uri);
            }

            shell.Show();
        }
    }
}
