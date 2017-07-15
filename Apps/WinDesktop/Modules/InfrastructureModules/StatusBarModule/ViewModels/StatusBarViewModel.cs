using Prism.Mvvm;
using StatusBarModule.Interfaces;
using System;

namespace StatusBarModule.ViewModels {
    public class StatusBarViewModel : BindableBase ,IStatusBarViewModel, IDisposable {

        public StatusBarViewModel() {

        }

        #region IDisposable

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {

            if (disposing) {
                // dispose of subscriptions, etc.               
            }
        }

        #endregion
    }
}
