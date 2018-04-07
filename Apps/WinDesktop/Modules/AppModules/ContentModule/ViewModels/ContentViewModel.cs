using LogXtreme.WinDsk.ContentModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Models;
using System;

namespace LogXtreme.WinDsk.ContentModule.ViewModels {

    public class ContentViewModel :
        ViewModelBase, 
        IContentViewModel,
        IDisposable {

        public ContentViewModel() { }       

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
