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

        private IDataSourceViewModel dataSourceViewModel;
        private IDataGridViewModel dataGridViewModel;      

        public DataTab4ViewModel() {
            
            // first we build the data source view model

            IEnumerable<string> generatorSources = 
                new string[] { "CHN0", "CHN1", "CHN2", "CHN3" };

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

            //TODO: this is temporary until the code in TestDataGrid is relocated to LogXtreme.WinDsk.Infrastructure
            IDataSourceModel dataSourceModel =
                (dataService as DataService)
                .GenerateDataSourceModel(sampleSourceModel);

            this.DataSourceViewModel = 
                new DataSourceViewModel(dataSourceModel);            

            // then we bild the data grid view model 

            // we wish to display the data read by the data source into a data grid.
            // the data grid service can be used to costruct a data view model and 
            // hook it up to the data source.

            IDataGridService dataGridService =
               new DataGridService();

            IDataGridSettingsModel dataGridSettingsModel =
                new DataGridSettingsModel(0);

            this.DataGridViewModel =
                dataGridService.CreateDataGridViewModel(
                    dataSourceModel,
                    dataGridSettingsModel);
        }

        public IDataGridViewModel DataGridViewModel {

            get => this.dataGridViewModel;            

            private set {

                if (value != this.dataGridViewModel) {
                    this.dataGridViewModel = value;
                    NotifyPropertyChanged();
                }
            }
        } 

        public IDataSourceViewModel DataSourceViewModel {

            get => this.dataSourceViewModel;

            private set {

                if (value != this.dataSourceViewModel) {
                    this.dataSourceViewModel = value;
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

                    this.dataGridViewModel?.Dispose();
                    this.dataGridViewModel = null;

                    this.dataSourceViewModel?.Dispose();
                    this.dataSourceViewModel = null;
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
