using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataTab4ViewModel :
        INotifyPropertyChanged,
        IDisposable  {

        private readonly IDataSourceViewModel dataSourceViewModel;
        private DataGridViewModel dataGridViewModel;

        public DataTab4ViewModel() {

            var generatorDescriptorModel = new RandomGeneratorDescriptorModel(new string[] { "CHN0", "CHN1", "CHN2" });
            var sampleGeneratorModel = new RandomGeneratorModel(generatorDescriptorModel);
            var sampleDescriptorModel = new SampleDescriptorModel(sampleGeneratorModel.Descriptor);
            var sampleSourceModel = new SampleSourceModel(sampleDescriptorModel, sampleGeneratorModel);
            var dataSourceModel = new DataSourceModel(sampleSourceModel);
            this.dataSourceViewModel = new DataSourceViewModel(dataSourceModel);

            // you need some service to bridge between the data source and the grid view
            // the serice encapsulates the logic that produces the data required by a grid 
            // view to display data from a data source.

            this.DataGridViewModel = new DataGridViewModel();
        }

        public DataGridViewModel DataGridViewModel {

            get {
                return this.dataGridViewModel;
            }

            private set {

                if (value != this.dataGridViewModel) {
                    this.dataGridViewModel = value;
                    NotifyPropertyChanged();
                }
            }
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
