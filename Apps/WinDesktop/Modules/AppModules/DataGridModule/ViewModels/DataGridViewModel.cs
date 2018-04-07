using LogXtreme.WinDsk.DataGridModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Models;
using Prism.Regions;
using System;

namespace LogXtreme.WinDsk.DataGridModule.ViewModels {
    public class DataGridViewModel :
        ViewModelBase,
        IDataGridViewModel,
        IDisposable {

        public DataGridViewModel() { }

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
