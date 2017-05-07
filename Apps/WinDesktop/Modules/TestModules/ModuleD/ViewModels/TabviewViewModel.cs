using System;
using LogXtreme.WinDsk.Infrastructure;
using Prism.Commands;
using Prism.Regions;
using ModuleD.Interfaces;

namespace ModuleD.ViewModels {
    public class TabviewViewModel: 
        ViewModelBase,
        ITabviewViewModel,
        IRegionManagerAware {

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public IRegionManager RegionManager { get; set; }

        public TabviewViewModel() {
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string viewName) {
            this.RegionManager.RequestNavigate(
                RegionNames.RegionContent,
                new Uri(viewName, UriKind.Relative));
        }
    }
}
