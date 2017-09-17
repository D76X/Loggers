using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    public class ItemViewModel : INotifyPropertyChanged  {

        private ItemModel model; 

        public ItemViewModel(ItemModel model) {
            this.model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
