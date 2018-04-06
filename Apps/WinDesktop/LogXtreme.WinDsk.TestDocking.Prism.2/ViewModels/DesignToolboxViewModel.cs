using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Regions;
using System;

namespace LogXtreme.WinDsk.TestDocking.Prism.ViewModels {

    public class DesignToolboxViewModel :
        ViewModelBase,
        IRegionManagerAware,
        IDesignToolboxViewModel,
        IDisposable {

        public DesignToolboxViewModel() { }

        private IRegionManager scopedRegionManager;
        public IRegionManager RegionManager {
            get => this.scopedRegionManager;
            set => this.scopedRegionManager = value;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) {
            base.OnNavigatedFrom(navigationContext);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
            base.OnNavigatedTo(navigationContext);
            base.RaiseNavigatedTo();
        }

        #region IDisposable

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {

            if (disposing) {
                // dispose of subcriptions, etc.               
            }
        }

        #endregion
    }
}
