﻿using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using Prism.Regions;
using System;

namespace LogXtreme.WinDsk.TestDocking.Prism.ViewModels {
    public class StatusBarViewModel :
        ViewModelBase, 
        IStatusBarViewModel,
        IRegionManagerAware,
        IDisposable {

        private IRegionManager scopedRegionManager;

        public StatusBarViewModel() { }

        public IRegionManager RegionManager {

            get => this.scopedRegionManager;

            set {
                if (this.scopedRegionManager != null) { return; }
                this.scopedRegionManager = value;
            }
        }

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
