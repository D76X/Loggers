﻿using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class DataSourceViewModel :
        IDataSourceViewModel,
        INotifyPropertyChanged,
        IDisposable {

        private readonly IDataSourceModel dataSourceModel;  
        private readonly RelayCommand cmdReadNext;
        private readonly ICommand cmdStartReading;
        private readonly ICommand cmdStopReading;

        private bool readingData;

        public DataSourceViewModel(IDataSourceModel dataSourceModel) {

            this.dataSourceModel = dataSourceModel;
            this.cmdReadNext = new RelayCommand(
                this.ExecuteReadNext,
                this.CanExecuteExecuteReadNext);

            this.cmdStartReading = new RelayCommand(this.ExecuteStartReading);
            this.cmdStopReading = new RelayCommand(this.ExecuteStopReading);
        }
        public bool ReadingData {

            get => this.readingData;

            private set {

                if (value != this.readingData) {
                    this.readingData = value;
                    this.cmdReadNext.RaiseCanExecuteChanged();
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand CommandReadNext =>
            this.cmdReadNext;

        public ICommand CommandStartReading =>
            this.cmdStartReading;

        public ICommand CommandStopReading =>
            this.cmdStopReading;

        private bool CanExecuteExecuteReadNext() =>
            !this.readingData;

        private void ExecuteReadNext() {

            this.dataSourceModel.StartDataReads(1);
        }

        private void ExecuteStartReading() {
            this.dataSourceModel.StartDataReads(0);
        }

        private void ExecuteStopReading() {
            this.dataSourceModel.StopDataReads();
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

                    // dispose of obsevables, etc.
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
