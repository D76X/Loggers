using LogXtreme.WinDsk.Infrastructure.Prism;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace LogXtreme.WinDsk.TestDocking.Prism.Services {

    /// <summary>
    /// The main purpose of ShellService 
    /// </summary>
    public class ShellService : ShellServiceBase<Shell> {

        public ShellService(
            IUnityContainer container,
            IRegionManager regionManager)
            : base(container, regionManager) { }
    }
}