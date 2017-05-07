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

        public string Title => nameof(ViewBViewModel);

        /// <summary>
        /// A reference to the region manager of the shell in which the view 
        /// of this view model in displayed.
        /// </summary>
        public ViewBViewModel() {       
            this.NavigateCommand = new DelegateCommand(Navigate);
        }

        private void Navigate() {
            this.RegionManager.RequestNavigate(RegionNames.RegionContent, "ViewA");
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext) {
            return false;
        }
    }
}
