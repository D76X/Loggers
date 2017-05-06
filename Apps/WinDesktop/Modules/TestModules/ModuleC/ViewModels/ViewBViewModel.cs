using LogXtreme.WinDsk.Infrastructure;
using ModuleC.Interfaces;
using Prism.Commands;
using Prism.Regions;

namespace ModuleC.ViewModels {
    public class ViewBViewModel : 
        ViewModelBase,
        IViewBViewModel {

        private readonly IRegionManager regionManager;

        public DelegateCommand NavigateCommand { get; private set; }

        public ViewBViewModel(IRegionManager regionManager) {

            this.regionManager = regionManager;

            this.NavigateCommand = new DelegateCommand(Navigate);
        }

        private void Navigate() {
            this.regionManager.RequestNavigate(RegionNames.RegionContent, "ViewA");
        }
    }
}
