using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    /// <summary>
    /// Refs
    /// https://stackoverflow.com/questions/1763696/how-can-i-make-a-read-only-observablecollection-property
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotifyCollection<T>
           : ObservableCollection<T>,
             INotifyCollection<T>,
             IReadOnlyNotifyCollection<T> {

        public NotifyCollection():base() { }
        public NotifyCollection(IEnumerable<T> items) : base(items) { }
        public NotifyCollection(List<T> items) : base(items) { }
    }    
}
