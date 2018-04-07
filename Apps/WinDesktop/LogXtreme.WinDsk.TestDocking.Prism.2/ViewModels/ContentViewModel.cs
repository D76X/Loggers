using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Regions;
using System;

namespace LogXtreme.WinDsk.TestDocking.Prism.ViewModels {

    public class ContentViewModel :
        ViewModelBase, 
        IContentViewModel,
        //IDockingManagerViewModel,        
        IDisposable {

        //private object activeContent;
        private DockingManagerSyncBehavior dockingManagerSyncBehavior;

        public ContentViewModel() {

            //this.Documents = new ObservableCollection<object>();
            //this.Anchorables = new ObservableCollection<object>();
        }       

        //public void SetDockingManagerSyncBehavior(DockingManagerSyncBehavior syncBehavior) {
            
        //    // can be set only once
        //    this.dockingManagerSyncBehavior = this.dockingManagerSyncBehavior ?? syncBehavior;
        //    this.dockingManagerSyncBehavior.SyncChanged += DockingManagerSyncBehavior_SyncChanged;
        //}

        //private void DockingManagerSyncBehavior_SyncChanged(object sender, EventArgs e) {
        //    //this.RaisePropertyChanged(nameof(ActiveContent));
        //    //this.RaisePropertyChanged(nameof(Documents));
        //    //this.RaisePropertyChanged(nameof(Anchorables));
        //    var active = this.ActiveContent;
        //    var docs = this.Documents;
        //    var anchs = this.Anchorables;
        //}

        //public object ActiveContent {

        //    get => this.dockingManagerSyncBehavior == null ? 
        //        null : 
        //        this.dockingManagerSyncBehavior.ActiveContent;

        //    //get => this.activeContent;

        //    //set  {
        //    //    this.SetProperty<object>(ref activeContent, value);
        //    //}
        //}

        //public ReadOnlyObservableCollection<object> Anchorables {

        //    get => this.dockingManagerSyncBehavior == null ?
        //        null :
        //        this.dockingManagerSyncBehavior.Anchorables;

        //    //get;
        //    //set;
        //}

        //public ReadOnlyObservableCollection<object> Documents {

        //    get => this.dockingManagerSyncBehavior == null ?
        //        null :
        //        this.dockingManagerSyncBehavior.Documents;
            
        //    //get;
        //    //set;
        //}

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
