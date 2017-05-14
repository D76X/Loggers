using System;
using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Prism;
using ModuleD.Interfaces;
using Prism.Commands;
using Prism.Regions;

namespace ModuleD.ViewModels
{
    public class ViewAViewModel : 
        ViewModelBase,
        IViewAViewModel,
        IRegionManagerAware,
        IConfirmNavigationRequest {

        public DelegateCommand NavigateCommand { get; private set; }

        /// <summary>
        /// A reference to the region manager of the shell in which the view 
        /// of this view model in displayed.
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        public string Title => nameof(ViewAViewModel);

        public ViewAViewModel() {

            this.NavigateCommand = new DelegateCommand(Navigate);
        }

        private void Navigate() {
            this.RegionManager.RequestNavigate(RegionNames.RegionContent, "ViewB");
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext) {
            return false;
        }

        public void ConfirmNavigationRequest(
            NavigationContext navigationContext, 
            Action<bool> continuationCallback) {

            continuationCallback(true);
        }
    }
}
