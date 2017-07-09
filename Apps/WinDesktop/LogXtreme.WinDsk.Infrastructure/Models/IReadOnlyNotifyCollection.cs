using System.Collections.Generic;
using System.Collections.Specialized;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    /// <summary>
    /// Refs
    /// https://stackoverflow.com/questions/1763696/how-can-i-make-a-read-only-observablecollection-property
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyNotifyCollection<out T>
           : IReadOnlyCollection<T>,
             INotifyCollectionChanged { }
}
