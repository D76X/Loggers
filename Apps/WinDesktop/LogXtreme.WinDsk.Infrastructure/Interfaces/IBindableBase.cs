using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogXtreme.WinDsk.Infrastructure.Interfaces {

    /// <summary>
    /// Exposes the SetProperty method as in Prism.BindableBase.
    /// </summary>
    public interface IBindableBase : INotifyPropertyChanged {

       bool SetPropertyValue<T>(ref T storage, T value, [CallerMemberName] String propertyName = null);
    }
}
