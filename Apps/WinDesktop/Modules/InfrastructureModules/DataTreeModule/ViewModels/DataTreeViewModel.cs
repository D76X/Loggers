using DataTreeModule.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataTreeModule.ViewModels {
    public class DataTreeViewModel : 
        BindableBase,
        IDataTreeViewModel, 
        IDisposable {

        readonly ReadOnlyCollection<DataViewModel> _firstGeneration;
        readonly DataViewModel _rootData;

        public DataTreeViewModel() {

            var childModel = new DataModel() { Name = "child" };
            var childData = new List<DataModel>();
            childData.Add(childModel);
            var rootData = new DataModel(childData);

            _rootData = new DataViewModel(rootData);

            _firstGeneration = new ReadOnlyCollection<DataViewModel>(
                new DataViewModel[]
                {
                    _rootData
                });

        }

        public ReadOnlyCollection<DataViewModel> FirstGeneration {
            get { return _firstGeneration; }
        }

        #region IDisposable

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {

            if (disposing) {
                // dispose of subscriptions, etc.               
            }
        }

        #endregion
    }
}
