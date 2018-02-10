using LogXtreme.WinDsk.Infrastructure.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.Infrastructure.Wpf {

    /// <summary>
    /// This is a stand-in class for Prism.BindableBase.
    /// 
    /// This class is used to avoid having to reference the Prism library in some test projects or 
    /// projects that reference the Infrastructure and do not need to reference Prism. The View 
    /// models in those project may derive from this base class instead of Prism.BindableBase.
    /// 
    /// In essence this class provides the same function as Prism.Bindable base that is a 
    /// conveiently packaged implementation of INotifyPropertyChanged and a set method so that
    /// VMs that use this class as their base class do not need to repeat the boilerplate code to
    /// support INotifyPropertyChanged.
    /// 
    /// Refs
    /// https://stackoverflow.com/questions/28844518/bindablebase-vs-inotifychanged
    /// https://app.pluralsight.com/player?course=wpf-mvvm-in-depth&author=brian-noyes&name=wpf-mvvm-in-depth-m6&clip=6&mode=live
    /// </summary>
    public class NotifyPropertyChangedBase : IBindableBase {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetPropertyBase<T>(
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

        protected void OnPropertyChanged([CallerMemberName] String propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null) =>
            ((IBindableBase)this).SetPropertyValue<T>(ref storage, value, propertyName);
        
        bool IBindableBase.SetPropertyValue<T>(ref T storage, T value, string propertyName) =>
            this.SetPropertyBase(ref storage, value, propertyName);        
    }
}
