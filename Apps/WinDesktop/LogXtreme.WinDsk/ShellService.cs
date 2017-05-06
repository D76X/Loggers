using System;
using LogXtreme.WinDsk.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace LogXtreme.WinDsk {

    /// <summary>
    /// The main purpose of ShellService 
    /// </summary>
    public class ShellService : IShellService {

        private IUnityContainer container;
        private IRegionManager regionManager;

        private int registeredShellCount; 

        public ShellService(
            IUnityContainer container,
            IRegionManager regionManager) {

            this.container = container;
            this.regionManager = regionManager;

        }

        public int RegisteredShellCount {
            get {
                return this.registeredShellCount;
            }
        }

        public int RegisterShellId() {
            this.registeredShellCount += 1;
            return this.registeredShellCount;
        }

        /// <summary>
        /// Resolve and show a new Shell and set the value of the property Shell.RegionManager to an instance 
        /// of a scoped RegionManager. If a URI to a view is provided it also requests navigation to that view
        /// prior to showing it.
        /// </summary>
        /// <param name="uri">The URI to a view to show</param>
        public void ShowShell(string uri=null) {

            var shell = this.container.Resolve<Shell>();

            // use the global manager to produce a scoped region manager
            var scopedRegionManager = this.regionManager.CreateRegionManager();

            // set the attached property RegionManager on the Shell to the scoped RegionManger
            RegionManager.SetRegionManager(shell, scopedRegionManager);

            // allow this instance of the shell to retain a refernce to its own scoped region manager
            // this will allow the shell to control its on navigation 
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegionManager);

            // navigate to the view passed by the caller
            // notice that the reference to the scoped region manager is used to request the navigation
            if (!string.IsNullOrEmpty(uri)) {
                scopedRegionManager.RequestNavigate(RegionNames.RegionContent, uri); 
            }

            // do all the initialisation you need on the shell
            // then show it
            shell.Show();            
        }
    }
}
