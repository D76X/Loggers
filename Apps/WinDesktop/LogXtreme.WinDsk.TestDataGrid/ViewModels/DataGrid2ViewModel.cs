using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class DataGrid2ViewModel {

        ObservableCollection<ItemViewModel> items;

        public DataGrid2ViewModel() {

            this.items = new ObservableCollection<ItemViewModel>();

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
    }
}
