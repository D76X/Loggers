using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTreeModule.ViewModels {
    public class DataViewModel : BindableBase {

        private DataModel _data;
        readonly ReadOnlyCollection<DataViewModel> _children;
        readonly DataViewModel _parent;        

        bool _isExpanded;
        bool _isSelected;

        public DataViewModel(DataModel data) : this(data, null) {
            this._data = data;
        }

        public DataViewModel(DataModel data, DataViewModel parent) {

            _data = data;
            _parent = parent;

            _children = new ReadOnlyCollection<DataViewModel>(
                    (from child in _data.Children
                     select new DataViewModel(child, this))
                     .ToList<DataViewModel>());
        }

        public ReadOnlyCollection<DataViewModel> Children {
            get { return _children; }
        }

        public string Name {
            get { return _data.Name; }
        }

        public bool IsExpanded {
            get { return _isExpanded; }
            set {

                //if (value != _isExpanded) {
                //    _isExpanded = value;
                //    this.OnPropertyChanged("IsExpanded");
                //}

                SetProperty(ref this._isExpanded, value);

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        public bool IsSelected {
            get { return _isSelected; }
            set {

                SetProperty(ref this._isSelected , value);

                //if (value != _isSelected) {
                //    _isSelected = value;
                //    this.OnPropertyChanged("IsSelected");
                //}
            }
        }
    }
}
