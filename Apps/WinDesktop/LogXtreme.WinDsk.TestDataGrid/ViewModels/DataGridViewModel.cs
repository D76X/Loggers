﻿using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataGridViewModel : 
        IDataGridViewModel,
        INotifyPropertyChanged {
        private readonly IDataSourceModel dataSourceModel;
        private readonly IDataGridModel dataGridModel;               

        private ObservableCollection<IHeaderModel> headers;
        private ObservableCollection<IDataModel> data =
            new ObservableCollection<IDataModel>();       

        private IDisposable dataObsevable;

        public DataGridViewModel(
            IDataGridModel dataGridModel,
            IDataSourceModel dataSourceModel = null) {

            this.dataGridModel = dataGridModel;            

            // the headers of the datagrid are initially the same as those 
            // of the undelying grid model but can be changed in the UI.
            this.headers = new ObservableCollection<IHeaderModel>(
                dataGridModel.GridStructure.Columns.Select(c =>
                new HeaderModel(c.Header.Name)));            

            //TODO when there's no dataSource should one be set up as default or with a setter or what?
            if (dataSourceModel == null) { return; }

            this.dataSourceModel = dataSourceModel; 

            //TODO : use Event to Observable pattern to prevent leaks
            this.dataSourceModel.OnStartDataReads += StartDataReads;
            this.dataSourceModel.OnStopDataReads += StopDataReads;
        }

        private void StopDataReads(
            object sender, 
            EventArgs e) {

            this.dataObsevable?.Dispose();
            this.dataObsevable = null;
        }

        private void StartDataReads(
            object sender, 
            IObservable<IDataModel> e) {
            
            this.dataObsevable?.Dispose();
            this.dataObsevable = null;            

            //TODO: handle observable exceptions and completion
            this.dataObsevable = e
                ?.SubscribeOn(ThreadPoolScheduler.Instance)
                .ObserveOn(DispatcherScheduler.Current)
                .Subscribe(
                    d => this.data.Add(d),
                    exc => { },
                    () => { });
        }

        public ObservableCollection<IHeaderModel> Headers => 
            this.headers;

        public ObservableCollection<IDataModel> Data => 
            this.data;

        public IDataGridSettingsModel GridSettings => 
            this.dataGridModel.GridSettings;
        
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