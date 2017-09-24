using LogXtreme.WinDsk.TestDataGrid.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {

    /// <summary>
    /// Refs
    /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/how-to-implement-the-inotifypropertychanged-interface
    /// </summary>
    public class ItemViewModel : INotifyPropertyChanged  {

        private ItemModel model; 

        public ItemViewModel(ItemModel model) {
            this.model = model;
        }

        public int Id => this.model.Id;

        public string Name {
            get {
                return this.model.Name;
            }

            set {

                if(value != this.model.Name) {
                    this.model.Name = value;
                    NotifyPropertyChanged();
                }
            }
        }        

        public string Code {
            get {
                return this.model.Code;
            }

            set {

                if(value != this.model.Code) {
                    this.model.Code = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Quantity {
            get {
                return this.model.Quantity;
            }

            set {

                if(value != this.model.Quantity) {
                    this.model.Quantity = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime Time {
            get {
                return this.model.Time;
            }

            set {

                if(value != this.model.Time) {
                    this.model.Time = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Uri Web {
            get {
                return this.model.Web;
            }

            set {

                if(value != this.model.Web) {
                    this.model.Web = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            this.PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
