using System;
using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Prism;
using ModuleD.Interfaces;
using Prism.Commands;
using Prism.Regions;
using LogXtreme.WinDsk.Modules.TestModules.ModuleD.Names;

namespace ModuleD.ViewModels
{
    public class ViewAViewModel : 
        ViewModelBase,
        IViewAViewModel,
        IRegionManagerAware {

        public IRegionManager RegionManager { get; set; }
        public DelegateCommand NavigateCommand { get; private set; }
        public string Title => nameof(ViewAViewModel);

        private bool isClosable = true;

        public ViewAViewModel() {

            this.NavigateCommand = new DelegateCommand(Navigate);
        }

        public bool IsClosable {
            get { return this.isClosable; }
            set { SetProperty(ref this.isClosable, value); }
        }       

        public override bool IsNavigationTarget(NavigationContext navigationContext) {
            return false;
        }

        public override void ConfirmNavigationRequest(
            NavigationContext navigationContext,
            Action<bool> continuationCallback) {

            continuationCallback(this.IsClosable);
        }

        private void Navigate() {
            this.RegionManager.RequestNavigate(
                RegionNames.RegionContent,
                ViewNamesModuleD.ViewB);
        }
    }
}
