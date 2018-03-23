using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.TestDocking.Prism.ViewModels {

    public class ContentViewModel :
        ViewModelBase, 
        IContentViewModel,
        IRegionManagerAware,
        IDisposable {

        private object activeContent;

        public ContentViewModel() {

            this.Documents = new ObservableCollection<object>();
            this.Anchorables = new ObservableCollection<object>();
        }

        private IRegionManager scopedRegionManager;
        public IRegionManager RegionManager {
            get => this.scopedRegionManager;
            set => this.scopedRegionManager = value;
        }

        public ObservableCollection<object> Anchorables {
            get;
            set;
        }

        public ObservableCollection<object> Documents {
            get;
            set;
        }

        public object ActiveContent {

            get => this.activeContent;

            set {
                this.SetProperty<object>(ref activeContent, value);
            }
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) {
            base.OnNavigatedFrom(navigationContext);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
            base.OnNavigatedTo(navigationContext);
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
