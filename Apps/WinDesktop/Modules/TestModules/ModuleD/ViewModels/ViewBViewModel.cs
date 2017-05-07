using LogXtreme.WinDsk.Infrastructure;
using ModuleD.Interfaces;
using Prism.Commands;
using Prism.Regions;

namespace ModuleD.ViewModels {
    public class ViewBViewModel : 
        ViewModelBase,
        IViewBViewModel,
        IRegionManagerAware {       

        public DelegateCommand NavigateCommand { get; private set; }

        public IRegionManager RegionManager { get; set; }

        public ViewBViewModel() {       
            this.NavigateCommand = new DelegateCommand(Navigate);
        }

        private void Navigate() {
            this.RegionManager.RequestNavigate(RegionNames.RegionContent, "ViewA");
        }
    }
}
