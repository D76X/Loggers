using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    /// <summary>
    /// https://stackoverflow.com/questions/1763696/how-can-i-make-a-read-only-observablecollection-property
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INotifyCollection<T>
       : ICollection<T>,
         INotifyCollectionChanged { }

    public interface IReadOnlyNotifyCollection<out T>
           : IReadOnlyCollection<T>,
             INotifyCollectionChanged { }

    public class NotifyCollection<T>
           : ObservableCollection<T>,
             INotifyCollection<T>,
             IReadOnlyNotifyCollection<T> {

        public NotifyCollection():base() { }
        public NotifyCollection(IEnumerable<T> items) : base(items) { }
        public NotifyCollection(List<T> items) : base(items) { }
    }    

    /// <summary>
    /// 
    /// Refs
    /// https://www.codeproject.com/Articles/26288/Simplifying-the-WPF-TreeView-by-Using-the-ViewMode
    /// Refs
    /// https://stackoverflow.com/questions/28844518/bindablebase-vs-inotifychanged
    /// https://www.danrigby.com/2012/04/01/inotifypropertychanged-the-net-4-5-way-revisited/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeItemViewModel<T> : BindableBase {

        private Node<T> data;
        private TreeItemViewModel<T> parent;
        private readonly IReadOnlyNotifyCollection<TreeItemViewModel<T>> children;

        private bool isSelected;
        private bool isExpanded;        

        public TreeItemViewModel(Node<T> data, TreeItemViewModel<T> parent) {

            this.data = data;
            this.parent = parent;

            this.children = 
                new NotifyCollection<TreeItemViewModel<T>>(
                    (from child in this.data.Children
                     select new TreeItemViewModel<T>((Node<T>)child, this)));
        }                

        #region Person Properties

        public bool IsExpanded {
            get { return isExpanded; }
            set {
                // some more here!
                SetProperty(ref this.isExpanded, value);
            }
        }

        public bool IsSelected {
            get { return this.isSelected; }
            set { SetProperty(ref this.isSelected, value); }
        }

        public IReadOnlyNotifyCollection<TreeItemViewModel<T>> Children => this.children;

        #endregion           
    }
}
