using LogXtreme.Extensions;
using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    /// <summary>
    /// Refs
    /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/how-to-implement-the-inotifypropertychanged-interface
    /// https://stackoverflow.com/questions/20290842/converter-to-show-description-of-an-enum-and-convert-back-to-enum-value-on-sele
    /// </summary>
    public class DataGrid2ViewModel : INotifyPropertyChanged {

        ObservableCollection<ItemViewModel> items;

        private readonly IReadOnlyList<DataGridHeadersVisibility>  dataGridVisibilityOptions;
        private DataGridHeadersVisibility selectedDataGridVisibilityOption;

        public DataGrid2ViewModel() {

            this.items = new ObservableCollection<ItemViewModel>();
            this.dataGridVisibilityOptions = EnumExtensions.GetValues<DataGridHeadersVisibility>().ToArray();

            var model1 = new ItemModel(1,"itemX", "ss-1", 13, DateTime.Now, new Uri("http://product/itemx"));
            this.items.Add(new ItemViewModel(model1));

            var model2 = new ItemModel(2,"itemY", "st-32", 98, DateTime.Now.AddDays(-23), new Uri("http://product/itemy"));
            this.items.Add(new ItemViewModel(model2));

            var model3 = new ItemModel(3,"itemZ", "st-00", -10, DateTime.Now.AddDays(10), new Uri("http://product/itemz"));
            this.items.Add(new ItemViewModel(model2));

            var model4 = new ItemModel(4,"itemW", "st-090", 11, DateTime.Now.AddDays(66), new Uri("http://product/itemw"));
            this.items.Add(new ItemViewModel(model4));

            var model5 = new ItemModel(5, "itemQ", "st-09", 8, DateTime.Now.AddDays(-10), new Uri("http://product/itemq"));
            this.items.Add(new ItemViewModel(model5));
        }

        public ObservableCollection<ItemViewModel> Items => this.items;

        /// <summary>
        /// Refs
        /// https://stackoverflow.com/questions/20290842/converter-to-show-description-of-an-enum-and-convert-back-to-enum-value-on-sele
        /// </summary>
        public IReadOnlyList<DataGridHeadersVisibility> DataGridVisibilityOptions => this.dataGridVisibilityOptions;

        public DataGridHeadersVisibility SelectedDataGridVisibilityOption {

            get { return this.selectedDataGridVisibilityOption; }

            set {

                if(value != this.selectedDataGridVisibilityOption) {
                    this.selectedDataGridVisibilityOption = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
