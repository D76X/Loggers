using LogXtreme.Reactive.Extensions;
using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataSourceViewModel :
        IDataSourceViewModel,
        INotifyPropertyChanged {

        private readonly IDataSourceModel dataSourceModel;

        private IDisposable dataObsevable;

        // subscriptions for events
        private IDisposable eventSubscription_SubscribeToDataModelsObservable;
        private IDisposable eventSubscription_DisposeSubscriptionToDataModelsObservable;

        private readonly RelayCommand<string> cmdStartReading;
        private readonly RelayCommand cmdStopReading;

        private bool readingData;        

        public DataSourceViewModel(IDataSourceModel dataSourceModel) {

            this.dataSourceModel = dataSourceModel;            

            this.eventSubscription_SubscribeToDataModelsObservable = Observable
                .FromEventPattern<IConnectableObservable<IDataModel>>(
                    h => this.dataSourceModel.OnStartDataReads += h,
                    h => this.dataSourceModel.OnStartDataReads -= h)
                .SubscribeWeakly(
                this,
                (target, ep) => target.SubscribeToDataModelsObservable(ep.Sender, ep.EventArgs));
            
            this.eventSubscription_DisposeSubscriptionToDataModelsObservable = Observable
                .FromEventPattern(
                    h => this.dataSourceModel.OnStopDataReads += h,
                    h => this.dataSourceModel.OnStopDataReads -= h)
                .SubscribeWeakly(
                this,
                (target, ep) => target.DisposeSubscriptionToDataModelsObservable(ep.Sender, ep.EventArgs as EventArgs));            

            this.cmdStartReading = new RelayCommand<string>(
            this.ExecuteStartReadingData,
            this.CanExecuteStartReadingData);

            this.cmdStopReading = new RelayCommand(
                this.ExecuteStopReadingData,
                this.CanExecuteStopReadingData);
        }

        private void SubscribeToDataModelsObservable(
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
                        this.dataSourceModel.StopDataReads();
                    });
        }

        private void DisposeSubscriptionToDataModelsObservable(
            object sender,
            EventArgs e) {

            this.dataObsevable?.Dispose();
            this.dataObsevable = null;
            this.ReadingData = false;
        }

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

                    this.eventSubscription_SubscribeToDataModelsObservable?.Dispose();
                    this.eventSubscription_SubscribeToDataModelsObservable = null;

                    this.eventSubscription_DisposeSubscriptionToDataModelsObservable?.Dispose();
                    this.eventSubscription_DisposeSubscriptionToDataModelsObservable = null;
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
