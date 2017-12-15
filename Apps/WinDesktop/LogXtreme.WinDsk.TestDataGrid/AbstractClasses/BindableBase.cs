
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.TestDataGrid.AbstractClasses {

    /// <summary>
    /// This is a stand-in class for the equivalent implementation 
    /// available in Prism. It is replicated here to avoid this 
    /// test project having to reference Prism. Some of the view 
    /// models in this project derive from this base class.
    /// 
    /// BindableBase is a class to encapsulate a convinient implementation
    /// of INotifyPropertyChanged.
    /// 
    /// Refs
    /// https://stackoverflow.com/questions/28844518/bindablebase-vs-inotifychanged
    /// https://app.pluralsight.com/player?course=wpf-mvvm-in-depth&author=brian-noyes&name=wpf-mvvm-in-depth-m6&clip=6&mode=live
    /// </summary>
    public abstract class BindableBase : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(
            ref T storage, 
            T value,
            [CallerMemberName] String propertyName = null) {

            if (Equals(storage, value)) {
                return false;
            }

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(
            [CallerMemberName] string propertyName = null) {

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
 