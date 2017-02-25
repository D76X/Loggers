using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfEmf.Interfaces {

    /// <summary>
    /// Typical implementation of INotifyPropertyChanged, inspred by the PluralSight course
    /// WPF MVVM in Depth by Brian Noyes - Demo: Envapsulating INotifyPropertyChanged.
    /// </summary>
    public class BindableBase : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Notify WPF stack that the view model has changed its state and it time to update the view via databinding.
        /// Intended to be called from the settter ot the exposed property on the decendant view models.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member"></param>
        /// <param name="val"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null) {

            if (object.Equals(member, val)) {
                return;
            }

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notify WPF stack that the view model has changed its state and it time to update the view via databinding.
        /// A call of the kind OnPropertyChanged("nameOfPropertyThatNeedsBindingRefreshing") may be required in cases
        /// where setting a new value on a property of a view model might cause the values of other properties to change
        /// as well. Thi is the case with computed properties for example.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName) {

            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}
