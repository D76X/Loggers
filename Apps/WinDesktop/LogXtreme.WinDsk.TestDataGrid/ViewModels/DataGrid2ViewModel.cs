using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    /// <summary>
    /// Refs
    /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/how-to-implement-the-inotifypropertychanged-interface
    /// </summary>
    public class DataGrid2ViewModel : INotifyPropertyChanged {

        ObservableCollection<ItemViewModel> items;
        public readonly IReadOnlyList<DataGridHeadersVisibility>  DataGridVisibilityOptions;

        public DataGrid2ViewModel() {

            this.items = new ObservableCollection<ItemViewModel>();
            this.DataGridVisibilityOptions = 

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

        //public string HeadersVisibility {

        //    get { return this.HeadersVisibility;  }

        //    set {

        //        if(value != this.HeadersVisibility) {
        //            this.hearderVisibility = value;
        //            NotifyPropertyChanged();               
        //        }
        //    }
        //} 

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
