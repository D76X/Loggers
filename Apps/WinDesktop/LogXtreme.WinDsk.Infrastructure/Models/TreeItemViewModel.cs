using Prism.Mvvm;
using System.Linq;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    /// <summary>
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

        #region Properties

        public bool IsExpanded {
            get { return isExpanded; }
            set {               
                
                SetProperty(ref this.isExpanded, value);

                if (this.isExpanded && this.parent != null) {
                    this.parent.IsExpanded = true;
                } 
            }
        }

        public bool IsSelected {
            get { return this.isSelected; }
            set { SetProperty(ref this.isSelected, value); }
        }

        public string Name => "Placeholder";

        public IReadOnlyNotifyCollection<TreeItemViewModel<T>> Children => this.children;

        #endregion           
    }
}
