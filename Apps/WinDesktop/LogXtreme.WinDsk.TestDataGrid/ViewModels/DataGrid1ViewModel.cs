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

            var model1 = new ItemModel("item1","ABC123",100,DateTime.Now);
            this.items.Add(new ItemViewModel(model1));

            var model2 = new ItemModel("item2", "EFG323", 17, DateTime.Now.AddDays(-1));
            this.items.Add(new ItemViewModel(model2));
        }

        public ObservableCollection<ItemViewModel> Items => this.items;
    }
}
