using ContentModule.Interfaces;
using Prism.Mvvm;
using System;

namespace ContentModule.ViewModels {
    public class ContentViewModel : BindableBase, IContentViewModel, IDisposable {

        public ContentViewModel() {

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
