using System;
using LogXtreme.WinDsk.Infrastructure;
using ModuleC.Interfaces;
using Prism.Commands;
using Prism.Regions;

namespace ModuleC.ViewModels {
    public class ViewAViewModel : 
        ViewModelBase,
        IViewAViewModel,
        IRegionManagerAware {

        public DelegateCommand NavigateCommand { get; private set; }

        public IRegionManager RegionManager { get; set; }

        public ViewAViewModel() {

            this.NavigateCommand = new DelegateCommand(Navigate);
        }

        private void Navigate() {
            this.RegionManager.RequestNavigate(RegionNames.RegionContent, "ViewB");
        }
    }
}
