using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class DataGrid1ViewModel {

        ObservableCollection<ItemViewModel> items;

        public DataGrid1ViewModel() {

            this.items = new ObservableCollection<ItemViewModel>();

            var model1 = new ItemModel("item1","ABC123",100,DateTime.Now, new Uri("http://product/item1"));
            this.items.Add(new ItemViewModel(model1));

            var model2 = new ItemModel("item2", "EFG323", 17, DateTime.Now.AddDays(-1), new Uri("http://product/item2"));
            this.items.Add(new ItemViewModel(model2));

            var model3 = new ItemModel("item3", "EFG324", 1, DateTime.Now.AddDays(-1), new Uri("http://product/item3"));
            this.items.Add(new ItemViewModel(model3));

            var model4 = new ItemModel("item4", "EFG000", 0, DateTime.Now.AddDays(29), new Uri("http://product/item4"));
            this.items.Add(new ItemViewModel(model4));

            var model5 = new ItemModel("item5", "EFG567", -1, DateTime.Now.AddDays(90), new Uri("http://product/item5"));
            this.items.Add(new ItemViewModel(model5));
        }

        public ObservableCollection<ItemViewModel> Items => this.items;
    }
}
