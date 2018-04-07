using LogXtreme.WinDsk.DataTreeModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Models;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.DataTreeModule.ViewModels {
    public class DataTreeViewModel :
        ViewModelBase,
        IDataTreeViewModel,
        IDisposable {

        readonly ReadOnlyCollection<DataViewModel> _firstGeneration;
        readonly DataViewModel _rootData;

        public DataTreeViewModel() {

            var childModel2 = new DataModel() { Name = "child2" };

            var childModel1 = new DataModel(
                new List<DataModel>() { new DataModel() { Name = "child11" } }) {
                Name = "child1"
            };

            var childData = new List<DataModel>();
            childData.Add(childModel1);
            childData.Add(childModel2);

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

        public override void OnNavigatedFrom(NavigationContext navigationContext) {
            base.OnNavigatedFrom(navigationContext);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
            base.OnNavigatedTo(navigationContext);
            base.RaiseNavigatedTo();
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
