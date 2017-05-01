using System;
using LogXtreme.WinDsk.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace LogXtreme.WinDsk {
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

        public void ShowShell(string uri=null) {

            var shell = this.container.Resolve<Shell>();

            // use the global manager to produce a scoped region manager
            var scopedRegionManager = this.regionManager.CreateRegionManager();

            // register this new instance of the Shell with its scoped region manager
            RegionManager.SetRegionManager(shell, scopedRegionManager);

            // allow this instance of the shell to retain a refernce to its own scoped region manager
            // this will allow the shell to control its on navigation
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegionManager);

            // navigate to the view passed by the caller
            if (!string.IsNullOrEmpty(uri)) {
                scopedRegionManager.RequestNavigate(RegionNames.RegionContent, uri); 
            }

            // do all the initialisation you need on the shell
            // then show it
            shell.Show();            
        }
    }
}
