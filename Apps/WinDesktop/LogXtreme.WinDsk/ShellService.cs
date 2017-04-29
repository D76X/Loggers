using LogXtreme.WinDsk.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace LogXtreme.WinDsk {
    public class ShellService : IShellService {

        IUnityContainer container;
        IRegionManager regionManager;

        public ShellService(
            IUnityContainer container,
            IRegionManager regionManager) {

            this.container = container;
            this.regionManager = regionManager;

        }

        public void ShowShell() {

            var shell = this.container.Resolve<Shell>();

            // use the global manager to produce a scoped region manager
            var scopedRegionManager = this.regionManager.CreateRegionManager();

            // register the shell with the scoped region
            RegionManager.SetRegionManager(shell, scopedRegionManager);


            // do all the initialisation you need on the shell
            // then show it
            shell.Show();
        }
    }
}
