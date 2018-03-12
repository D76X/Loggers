using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System.Windows;

namespace LogXtreme.WinDsk.TestDocking.Prism.Services {
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
        /// Resolve a new Shell and sets the value of the attached property 
        /// Shell.RegionManager to an instance of a scoped RegionManager. 
        /// If a URI to a view is provided it also requests navigation to 
        /// that view in order to show the view when the shell is shown.
        /// prior to showing it.
        /// </summary>
        /// <param name="uri">The URI to a view to show in the shell when the shell is shown</param>
        /// <summary>
        /// Resolve and show a new Shell and set the value of the property Shell.RegionManager to an instance 
        /// of a scoped RegionManager. If a URI to a view is provided it also requests navigation to that view
        /// prior to showing it.
        /// </summary>
        /// <param name="uri">The URI to a view to show</param>
        public DependencyObject CreateShell(IRegionManager regionManager) {

            var shell = this.container.Resolve<LogXtreme.WinDsk.TestDocking.Prism.Shell>();

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

        /// <summary>
        /// Given a shell window as a DependencyObject it shows it.
        /// It also checks whether the shell implements navigation
        /// and if it does and a Uri to a view is given it invokes
        /// a navigation request to the given Uri. 
        /// </summary>
        /// <param name="shellDependencyObject">A reference to the shell</param>
        /// <param name="uri">The Uri of a view to navigate to when the shell is shown</param>
        public void ShowShell(
            DependencyObject shellDependencyObject,
            string uri = null) {

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
