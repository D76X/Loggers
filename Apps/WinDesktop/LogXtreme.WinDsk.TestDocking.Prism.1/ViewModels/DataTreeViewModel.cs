﻿using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Mvvm;
using System;

namespace LogXtreme.WinDsk.TestDocking.Prism.ViewModels {
    public class DataTreeViewModel : BindableBase, IDataTreeViewModel, IDisposable {

        public DataTreeViewModel() { }

        #region IDisposable

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {

            if (disposing) {
                // dispose of subcriptions, etc.               
            }
        }

        #endregion
    }
}