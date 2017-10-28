using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataGrid4ViewModel :
        INotifyPropertyChanged,
        IDisposable  {

        private DataSourceViewModel2 dataSource;
        private DataGrid4SettingsViewModel sampleGridSettings;

        public DataGrid4ViewModel() {

            // ?
            this.DataGridSettings = new DataGrid4SettingsViewModel();

            // ?
            this.DataSource = this.CreateDataSource(this.DataGridSettings);

        }

        public DataGrid4SettingsViewModel DataGridSettings {

            get {
                return this.sampleGridSettings;
            }

            private set {

                if (value != this.sampleGridSettings) {
                    this.sampleGridSettings = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DataSourceViewModel2 DataSource {

            get {
                return this.dataSource;
            }

            private set {

                if (value != this.dataSource) {
                    this.dataSource = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DataSourceViewModel2 CreateDataSource(ISampleDataGridSettings dataSourceSettings) {

            var generatorDescriptor = new RandomGeneratorDescriptor(new string[] { "CHN0", "CHN1", "CHN2" });
            var sampleGenerator = new RandomGenerator(generatorDescriptor);
            var sampleDescriptor = new SampleDescriptor(sampleGenerator.Descriptor);
            var dataSource = new DataSource1(sampleDescriptor, sampleGenerator);
            return new DataSourceViewModel2(dataSource);
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
