
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class DataGridSettingsViewModel :
        INotifyPropertyChanged,
        IDataGridSettingsViewModel {

        private readonly IDataGridSettingsModel dataGridSettingsModel;

        public DataGridSettingsViewModel(
            IDataGridSettingsModel dataGridSettingsModel) {

            this.dataGridSettingsModel = dataGridSettingsModel;
        }

        public int NumberOfItemsToDisplay {

            get => this.dataGridSettingsModel.NumberOfItemsToDisplay;

            set {

                var nOfItemsToDisplay = this.dataGridSettingsModel.NumberOfItemsToDisplay;

                if (value != NumberOfItemsToDisplay) {

                    this.dataGridSettingsModel.NumberOfItemsToDisplay = value;                   
                    this.NotifyPropertyChanged();
                }
            }
        }

        public ResizeObservableCollectionCycleModeEnum CycleMode {

            get => this.dataGridSettingsModel.CycleMode;

            set {

                var currentCycleModel = this.dataGridSettingsModel.CycleMode;

                if (value != currentCycleModel) {

                    this.dataGridSettingsModel.CycleMode = value;                    
                    this.NotifyPropertyChanged();
                }
            }
        }

        public event EventHandler OnGridSettingsChanged;

        private void RaiseOnGridSettingsChanged() {
            this.OnGridSettingsChanged?.Invoke(this, EventArgs.Empty);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;       

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            this.RaiseOnGridSettingsChanged();
        }

        #endregion

        #region IDisposable Support     

        private bool disposedValue = false; // To detect redundant calls       

        protected virtual void Dispose(bool disposing) {

            if (!disposedValue) {

                if (disposing) {
                    
                    // dispose of subscriptions, etc.
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
