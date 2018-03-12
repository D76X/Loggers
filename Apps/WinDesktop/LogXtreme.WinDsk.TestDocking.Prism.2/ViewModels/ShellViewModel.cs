using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Regions;

namespace LogXtreme.WinDsk.TestDocking.Prism.ViewModels {

    public class ShellViewModel :
        ViewModelBase,
        IShellViewModel,
        IRegionManagerAware {

        private IShellService shellService;

        private int id;

        public ShellViewModel(IShellService shellService) {

            this.shellService = shellService;
            this.Id = this.shellService.ShellCreatedCount;
        }

        /// <summary>
        /// A reference to the region manager for the shell.        
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        public int Id {
            get { return this.id; }
            private set { this.SetProperty(ref this.id, value); }
        }        
    }
}
