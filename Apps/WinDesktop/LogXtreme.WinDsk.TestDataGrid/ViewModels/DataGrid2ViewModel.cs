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

            var model1 = new ItemModel("itemX", "ss-1", 13, DateTime.Now);
            this.items.Add(new ItemViewModel(model1));

            var model2 = new ItemModel("itemY", "st-32", 98, DateTime.Now.AddDays(-23));
            this.items.Add(new ItemViewModel(model2));
        }

        public ObservableCollection<ItemViewModel> Items => this.items;
    }
}
