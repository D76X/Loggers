using System;
using LogXtreme.WinDsk.Infrastructure;
using ModuleC.Interfaces;
using Prism.Commands;
using Prism.Regions;

namespace ModuleC.ViewModels {
    public class ViewAViewModel : 
        ViewModelBase,
        IViewAViewModel {

        private readonly IRegionManager regionManager;

        public DelegateCommand NavigateCommand { get; private set; }

        public ViewAViewModel(IRegionManager regionManager) {

            this.regionManager = regionManager;

            this.NavigateCommand = new DelegateCommand(Navigate);
        }

        private void Navigate() {
            this.regionManager.RequestNavigate(RegionNames.RegionContent, "ViewB");
        }
    }
}
