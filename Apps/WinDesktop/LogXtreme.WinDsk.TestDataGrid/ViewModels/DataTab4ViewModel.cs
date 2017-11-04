using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using LogXtreme.WinDsk.TestDataGrid.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataTab4ViewModel :
        INotifyPropertyChanged,
        IDisposable  {

        private readonly IDataSourceViewModel dataSourceViewModel;
        private DataGridViewModel dataGridViewModel;      

        public DataTab4ViewModel() {
            
            IEnumerable<string> generatorSources = 
                new string[] { "CHN0", "CHN1", "CHN2" };

            IGeneratorDescriptorModel generatorDescriptorModel = 
                new RandomGeneratorDescriptorModel(generatorSources);

            ISampleGeneratorModel sampleGeneratorModel = 
                new RandomGeneratorModel(generatorDescriptorModel);

            ISampleDescriptorModel sampleDescriptorModel = 
                new SampleDescriptorModel(sampleGeneratorModel.Descriptor);

            ISampleSourceModel sampleSourceModel = 
                new SampleSourceModel(
                    sampleDescriptorModel, 
                    sampleGeneratorModel);

            var dataService = new DataService();

            IDataSourceModel dataSourceModel =
                (dataService as DataService)
                .GenerateDataSourceModel(sampleSourceModel);

            this.dataSourceViewModel = new DataSourceViewModel(dataSourceModel);

            // now that we have a data source view model we can use it to 
            // 1- read a single piece of data 
            // 2- read a stream of data
            // 3- stop the stream of data
            // 4- other commands to influence the behaviour of the data source?

            // we now need to display the data obtained from the data source into a data grid.
            // the data grid controls how the data is going to be displayed.

            IDataGridService dataGridService =
                new DataGridService();

            IDataGridStructureModel dataGridStructureModel =
                dataGridService.GenerateDataGridStructureModel(dataSourceModel);

            IDataGridSettingsModel dataGridSettingsModel = 
                new DataGridSettingsModel();

            var dataGridModel = new DataGridModel(
                dataGridStructureModel,
                dataGridSettingsModel);

            this.DataGridViewModel = new DataGridViewModel(dataGridModel);
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
