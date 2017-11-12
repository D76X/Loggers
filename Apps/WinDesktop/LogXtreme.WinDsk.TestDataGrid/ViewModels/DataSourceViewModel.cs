using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataSourceViewModel :
        IDataSourceViewModel,
        INotifyPropertyChanged { 

        private readonly IDataSourceModel dataSourceModel;

        private IDisposable dataObsevable;

        //private readonly RelayCommand cmdReadNext;
        private readonly RelayCommand<string> cmdStartReading;
        private readonly RelayCommand cmdStopReading;

        private bool readingData;

        public DataSourceViewModel(IDataSourceModel dataSourceModel) {

            this.dataSourceModel = dataSourceModel;

            //TODO replace with weak event pattern
            this.dataSourceModel.OnStartDataReads += 
                DataSourceModel_OnStartDataReads;

            this.dataSourceModel.OnStopDataReads += 
                DataSourceModel_OnStopDataReads;  

            //this.cmdReadNext = new RelayCommand(
            //    this.ExecuteReadNext,
            //    this.CanExecuteExecuteReadNext);

            this.cmdStartReading = new RelayCommand<string>(
                this.ExecuteStartReadingData,
                this.CanExecuteStartReadingData);

            this.cmdStopReading = new RelayCommand(
                this.ExecuteStopReadingData,
                this.CanExecuteStopReadingData);           
        }

        private void DataSourceModel_OnStartDataReads(
            object sender, 
            IObservable<IDataModel> e) {

            this.dataObsevable?.Dispose();
            this.dataObsevable = null;

            //TODO: handle observable exceptions and completion
            this.dataObsevable = e
                ?.SubscribeOn(ThreadPoolScheduler.Instance)
                .ObserveOn(DispatcherScheduler.Current)
                .Subscribe(
                    d => { },
                    exc => { },
                    () => {
                        
                        // on completion raise this causes the data source
                        // to raise the stop event

                        this.dataSourceModel.StopDataReads();
                    });
        }

        private void DataSourceModel_OnStopDataReads(
            object sender, 
            EventArgs e) {

            this.dataObsevable?.Dispose();
            this.dataObsevable = null;
            this.ReadingData = false;
        }

        //public bool ReadingData {

        //    get => this.readingData;

        //    private set {

        //        if (value != this.readingData) {
        //            this.readingData = value;
        //            this.cmdReadNext.RaiseCanExecuteChanged();
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //public ICommand CommandReadNext =>
        //    this.cmdReadNext;

        public bool ReadingData {

            get => this.readingData;

            private set {

                if (value != this.readingData) {

                    this.readingData = value;

                    this.cmdStartReading.RaiseCanExecuteChanged();
                    this.cmdStopReading.RaiseCanExecuteChanged();

                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand CommandStartReadingData =>
            this.cmdStartReading;

        public ICommand CommandStopReadingData =>
            this.cmdStopReading;

        //private bool CanExecuteExecuteReadNext() =>
        //    !this.readingData;

        //private void ExecuteReadNext() {

        //    this.dataSourceModel.StartDataReads(1);
        //}

        //private void ExecuteStartReadingData() {
        //    this.dataSourceModel.StartDataReads(0);
        //}

        //private void ExecuteStopReadingData() {
        //    this.dataSourceModel.StopDataReads();
        //}

        private void ExecuteStartReadingData(string countParam) {

            if (!int.TryParse(countParam, out int count) || count < 0) {
                this.ReadingData = false;
                return;
            }

            this.dataSourceModel.StartDataReads(count);
            this.ReadingData = true;
        }

        private bool CanExecuteStartReadingData(string count) =>
            !this.ReadingData;

        private void ExecuteStopReadingData() {
            this.dataSourceModel.StopDataReads();
            this.ReadingData = false;
        }

        private bool CanExecuteStopReadingData() =>
            this.ReadingData;

        //private void ExecuteStopReadingData() {
        //    this.dataSourceModel.StopDataReads();
        //}

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

                    this.dataObsevable?.Dispose();
                    this.dataObsevable = null;
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
