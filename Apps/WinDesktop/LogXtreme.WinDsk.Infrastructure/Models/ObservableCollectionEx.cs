using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace LogXtreme.WinDsk.Infrastructure.Models {
    public class ObservableCollectionEx<T> :
        ObservableCollection<T> {

        /// <summary>
        /// An overload of the base class Remove method that accept a predicate.
        /// Refs
        /// https://stackoverflow.com/questions/5118513/removeall-for-observablecollections
        /// </summary>
        /// <typeparam name="T"></typeparam>        
        /// <param name="predicate">condition for removal</param>
        /// <returns></returns>
        public int Remove(Func<T, bool> predicate) {

            var itemsToRemove = this.Where(predicate).ToList();

            foreach (var itemToRemove in itemsToRemove) {
               this.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }

        /// <summary>
        /// Effiently removes all items in the collection.
        /// 
        /// Refs
        /// https://stackoverflow.com/questions/5118513/removeall-for-observablecollections
        /// https://stackoverflow.com/questions/224155/when-clearing-an-observablecollection-there-are-no-items-in-e-olditems
        /// https://stackoverflow.com/questions/224155/when-clearing-an-observablecollection-there-are-no-items-in-e-olditems/9416535#9416535
        /// </summary>
        public void RemoveAll() {
            this.Items.Clear();
            this.OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
