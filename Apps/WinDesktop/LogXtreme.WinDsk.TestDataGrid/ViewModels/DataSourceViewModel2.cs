using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    /// <summary>
    /// This class evolves DataSourceViewModel1 with additional details.
    /// </summary>
    public class DataSourceViewModel2 :
        INotifyPropertyChanged,
        IDisposable {

        private readonly ISampleSource sampleSource;

        public DataSourceViewModel2(ISampleSource sampleSource) {

            this.sampleSource = sampleSource;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDisposable Support     


        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {

            if (!disposedValue) {

                if (disposing) {

                    //this.samplesObsevable?.Dispose();
                    //this.samplesObsevable = null;
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(true);
        }
        #endregion
    }
}
