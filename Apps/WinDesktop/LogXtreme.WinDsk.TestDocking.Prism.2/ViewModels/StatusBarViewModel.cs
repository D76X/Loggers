using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using System;

namespace LogXtreme.WinDsk.TestDocking.Prism.ViewModels {

    public class StatusBarViewModel :
        ViewModelBase, 
        IStatusBarViewModel,
        IDisposable {

        public StatusBarViewModel() { }        

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
