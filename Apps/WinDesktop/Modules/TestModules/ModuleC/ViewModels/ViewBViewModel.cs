using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Prism;
using ModuleC.Interfaces;
using Prism.Commands;
using Prism.Regions;

namespace ModuleC.ViewModels {
    public class ViewBViewModel : 
        ViewModelBase,
        IViewBViewModel,
        IRegionManagerAware {       

        public DelegateCommand NavigateCommand { get; private set; }

        /// <summary>
        /// A reference to the region manager of the shell in which the view 
        /// of this view model in displayed.
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        public ViewBViewModel() {       
            this.NavigateCommand = new DelegateCommand(Navigate);
        }

        private void Navigate() {
            this.RegionManager.RequestNavigate(RegionNames.RegionContent, "ViewA");
        }
    }
}
