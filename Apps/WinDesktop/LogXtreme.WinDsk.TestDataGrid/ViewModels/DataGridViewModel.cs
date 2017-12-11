using LogXtreme.Extensions;
using LogXtreme.Reactive.Extensions;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataGridViewModel :
        IDataGridViewModel,
        INotifyPropertyChanged {

        private readonly IDataSourceModel dataSourceModel;
        private readonly IDataGridModel dataGridModel;
        private readonly IDataGridSettingsViewModel gridSettings;

        private ObservableCollection<IHeaderModel> headers;
        private ResizeObservableCollection<IDataModel> data;

        // subscriptions
        private IDisposable dataObsevableSubscription;
        private IDisposable eventSubscription_SubscribeAndConnectToDataModelsObservable;
        private IDisposable eventSubscription_DisposeSubscriptionToDataModelsObservable;
        private IDisposable eventSubscription_OnGridSettingsChanged;

        public DataGridViewModel(
            IDataGridModel dataGridModel,
            IDataSourceModel dataSourceModel = null) {

            this.dataGridModel = dataGridModel;

            this.gridSettings = new DataGridSettingsViewModel(this.dataGridModel.GridSettings);

            this.eventSubscription_OnGridSettingsChanged = Observable
                .FromEventPattern(
                    h => this.gridSettings.OnGridSettingsChanged += h,
                    h => this.gridSettings.OnGridSettingsChanged -= h)
                .SubscribeWeakly(
                this,
                (target, ep) => target.GridSettingsChangedHanlder(ep.Sender, ep.EventArgs as EventArgs));

            // the headers of the datagrid are initially the same as those 
            // of the undelying grid model but can be changed in the UI.
            this.headers = new ObservableCollection<IHeaderModel>(
                dataGridModel.GridStructure.Columns.Select(c =>
                new HeaderModel(c.Header.Name)));

            //TODO when there's no dataSource should one be set up as default or with a setter or what?
            if (dataSourceModel == null) { return; }

            this.dataSourceModel = dataSourceModel;

            this.eventSubscription_SubscribeAndConnectToDataModelsObservable = Observable
                .FromEventPattern<IConnectableObservable<IDataModel>>(
                    h => this.dataSourceModel.OnStartDataReads += h,
                    h => this.dataSourceModel.OnStartDataReads -= h)
                .SubscribeWeakly(
                this,
                (target, ep) => target.SubscribeAndConnectToDataModelsObservable(ep.Sender, ep.EventArgs));

            this.eventSubscription_DisposeSubscriptionToDataModelsObservable = Observable
                .FromEventPattern(
                    h => this.dataSourceModel.OnStopDataReads += h,
                    h => this.dataSourceModel.OnStopDataReads -= h)
                .SubscribeWeakly(
                this,
                (target, ep) => target.DisposeSubscriptionToDataModelsObservable(ep.Sender, ep.EventArgs as EventArgs));

            this.SetUpDataGrid();
        }

        public ObservableCollection<IHeaderModel> Headers =>
            this.headers;

        public ObservableCollection<IDataModel> Data {

            get => this.data;

            set {

                var collection = value as ResizeObservableCollection<IDataModel>;

                if (collection == null) { return; }

                this.data = collection;
                this.NotifyPropertyChanged();
            }
        }

        public IDataGridSettingsViewModel GridSettings
             => this.gridSettings;

        private void SetUpDataGrid() {

            this.Data = new ResizeObservableCollection<IDataModel>(
                this.GridSettings.NumberOfItemsToDisplay,
                this.GridSettings.CycleMode);
        }

        private void GridSettingsChangedHanlder(
            object sender,
            EventArgs e) {

            // save data?
            // ...
            this.SetUpDataGrid();
        }      

        private void DisposeSubscriptionToDataModelsObservable(
            object sender,
            EventArgs e) {

            this.dataObsevableSubscription?.Dispose();
            this.dataObsevableSubscription = null;
        }

        private void SubscribeAndConnectToDataModelsObservable(
            object sender,
            IConnectableObservable<IDataModel> e) {
            this.dataObsevableSubscription = null;

            //TODO: handle observable exceptions and completion
            var connectableData = e
                ?.SubscribeOn(ThreadPoolScheduler.Instance)
                .ObserveOn(DispatcherScheduler.Current)
                .Subscribe(d => {
                    var x = d.Values.Stringify(StringExtentions.SingleSpace);
                    this.data.Add(d);
                },
                    exc => { },
                    () => { });

            this.dataObsevableSubscription = e.Connect();
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

                    this.dataObsevableSubscription?.Dispose();
                    this.dataObsevableSubscription = null;

                    this.eventSubscription_SubscribeAndConnectToDataModelsObservable?.Dispose();
                    this.eventSubscription_SubscribeAndConnectToDataModelsObservable = null;

                    this.eventSubscription_DisposeSubscriptionToDataModelsObservable?.Dispose();
                    this.eventSubscription_DisposeSubscriptionToDataModelsObservable = null;

                    this.eventSubscription_OnGridSettingsChanged?.Dispose();
                    this.eventSubscription_OnGridSettingsChanged = null;

                    this.gridSettings?.Dispose();
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
