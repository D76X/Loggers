using LogXtreme.Extensions;
using Prism.Regions;
using Prism.Regions.Behaviors;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Xceed.Wpf.AvalonDock;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary>
    /// This RegionBehavior is used to turn AvaloDock DockingManager control into a Prism region.
    /// 
    /// The role of <see cref="DockingManagerDocumentsSourceSyncBehavior"/> is to provide 
    /// synchronization logic between the underlying control DockingManager and the corresponding
    /// Prism region. Prism sees the the underlying DockingManager instance a name region thus
    /// it needs to decide what to do when instances of LayoutAnchorable or LayoutDocument are 
    /// added to or removed from the underlying instance of DockingManager. 
    /// 
    /// In the OnAttached override for this behavior.
    /// 
    /// 1- a binding is set between the DP DocumentsSourceProperty on the underlying control 
    /// DockingManager and the Documents property on the behavior.
    /// 
    /// 2- the behaviors subscribes and provides an an handler for the event that 
    /// DockingManager.ActiveContentChanged so that the Prism region can correctly set the active 
    /// view when the active document or anchorable in the underlying DockingManager instance
    /// changes.
    /// 
    /// 3- the behaviors subscribes and provides an an handler for the event 
    /// this.Region.ActiveViews.CollectionChanged so that when the active view in the region for 
    /// the underlying DockingManager instance the DockingManager synchs its ActiveContent
    /// property to the corresponding item.
    ///       
    /// 4- the behaviors subscribes and provides an an handler for the event 
    /// this.Region.Views.CollectionChanged so that whan a region internal to the underlying 
    /// DockingManager instance is added or removed from the Region for the DockingManager instance 
    /// the underlying DockingManager instance can synch its own document property to reflect 
    /// such addition/removal. 
    /// 
    /// Refs
    /// 
    /// This is a course where it is explained how to design and use a Prism RegionBehavior
    /// in relation to the TabControl. 
    /// https://app.pluralsight.com/player?course=prism-mastering-tabcontrol&author=brian-lagunas&name=prism-mastering-tabcontrol-m4&clip=7&mode=live
    /// 
    /// These are two GitHub public repos where this is exact behavior is used. 
    /// https://github.com/miseeger/MashedVVM/blob/master/Prism/RegionAdapter/AvalonDock/DockingManagerDocumentsSourceSyncBehavior.cs
    /// https://github.com/alfredoperez/Central/blob/master/Central.Infrastructure/Behaviors/DockingManagerDocumentsSourceSyncBehavior.cs
    /// 
    /// This is the original thread where the issue of enhancing AvalonDock into a Prism region 
    /// was discussed.
    /// http://avalondock.codeplex.com/discussions/390255
    /// 
    /// 
    /// https://stackoverflow.com/questions/17297581/get-hostcontrol-from-region-in-prism
    /// 
    /// Refs on binding
    /// https://msdn.microsoft.com/en-us/library/system.windows.data.bindingoperations.setbinding(v=vs.110).aspx
    /// https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/how-to-create-a-binding-in-code
    /// https://www.codeproject.com/Articles/483507/AvalonDock-Tutorial-Part-Adding-a-Tool-Windo
    /// https://github.com/Dirkster99/Edi
    /// </summary>
    public class DockingManagerDocumentsSourceSyncBehavior :
        RegionBehavior,
        IHostAwareRegionBehavior {

        public static readonly string BehaviorKey = nameof(DockingManagerDocumentsSourceSyncBehavior);
        public ObservableCollection<object> documents = new ObservableCollection<object>();
        public ReadOnlyObservableCollection<object> readonlyDocuments = null;
        private bool updatingActiveViewsInManagerActiveContentChanged;
        private DockingManager dockingManager;

        /// <summary>
        /// We want to turn a DockingManager into a region, thus the HostControl must be an instance 
        /// of DockingManager. The Region Adapter that attaches this behaviors must set the host 
        /// control on it before adding to its collection of region behaviors.
        /// </summary>
        public DependencyObject HostControl {
            get => this.dockingManager;
            set => this.dockingManager = value as DockingManager;
        }

        public ReadOnlyObservableCollection<object> Documents {

            get {

                return readonlyDocuments == null ?
                       readonlyDocuments = new ReadOnlyObservableCollection<object>(this.documents) :
                       readonlyDocuments;
            }
        }

        /// <summary>
        /// Starts to monitor the <see cref="IRegion"/> to keep it in synch with the items of the 
        /// <see cref="HostControl"/>
        /// </summary>
        protected override void OnAttach() {

            var documentSource = this.dockingManager.DocumentsSource;

            if (documentsSource != null &&
                documentsSource.ToList<object>().Any()) {

                // we want to begin the synch from the state where the DockingManager
                // does not have documents bound to it. 
                // Is this really necessary?
                throw new InvalidOperationException();
            }

            this.SynchronizeItems();

            this.dockingManager.ActiveContentChanged += this.ManagerActiveContentChanged;

            this.Region.ActiveViews.CollectionChanged += this.ActiveViewsCollectionChanged;

            this.Region.Views.CollectionChanged += this.ViewsCollectionChanged;
        }

        /// <summary>
        /// Handles any change of the region views collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewsCollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs e) {

            if (e.Action == NotifyCollectionChangedAction.Add) {

                int startIndex = e.NewStartingIndex;

                foreach (object newItem in e.NewItems) {

                    this.documents.Insert(startIndex++, newItem);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove) {

                foreach (object oldItem in e.OldItems) {

                    this.documents.Remove(oldItem);
                }
            }
        }

        /// <summary>
        /// Handles a change of the active view on the Region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveViewsCollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs e) {

            if (this.updatingActiveViewsInManagerActiveContentChanged) {
                return;
            }

            if (e.Action == NotifyCollectionChangedAction.Add) {

                if (this.dockingManager.ActiveContent != null &&
                    this.dockingManager.ActiveContent != e.NewItems[0] &&
                    this.Region.ActiveViews.Contains(this.dockingManager.ActiveContent)) {

                    this.Region.Deactivate(this.dockingManager.ActiveContent);
                }

                this.dockingManager.ActiveContent = e.NewItems[0];
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove &&
                    e.OldItems.Contains(this.dockingManager.ActiveContent)) {

                this.dockingManager.ActiveContent = null;
            }
        }


        /// <summary>
        /// Handles a change event of the active content on the docking manager.
        /// When such change is detected the view for the active content on the 
        /// DockingManager must become active on the corresponding region. At the
        /// same time all other views on the region must be deactivated. This will
        /// allow the region to display the view corresponding to the active content
        /// on the host control instance DockingManager.
        /// </summary>
        /// <param name="sender">Should be the DockingManager instance.</param>
        /// <param name="e"></param>
        private void ManagerActiveContentChanged(
            object sender,
            EventArgs e) {

            try {

                this.updatingActiveViewsInManagerActiveContentChanged = true;

                if (this.dockingManager == sender) {

                    object activeContent = this.dockingManager.ActiveContent;

                    var allOtherActiveViewsInTheRegion = this.Region
                        .ActiveViews
                        .Where(v => v != activeContent);

                    foreach (var item in allOtherActiveViewsInTheRegion) {

                        this.Region.Deactivate(item);
                    }

                    if (this.Region.Views.Contains(activeContent) &&
                        !this.Region.ActiveViews.Contains(activeContent)) {

                        // activate the view in the Region for the 
                        // DockManager active content
                        this.Region.Activate(activeContent);
                    }
                }
            }
            finally {

                this.updatingActiveViewsInManagerActiveContentChanged = false;
            }
        }

        /// <summary>
        /// Creates a binding between this behavior Documents property and the DockingManager
        /// DocumentsSourceProperty dependency property on the instance of the DockingManager
        /// that is used as a host for the region.  
        /// </summary>
        private void SynchronizeItems() {

            // BindingOperations attaches a binding to an arbitrary DependencyObject that may 
            // not expose its own SetBinding method. In this case we want to set a binding
            // between this instance of the DockingManagerDocumentsSourceSyncBehavior and the 
            // instance of the DockingManager that is the host control for the region. This
            // syncs this instance DockingManagerDocumentsSourceSyncBehavior.Documents property
            // to the DockingManager.DocumentsSourceProperty DP on the target.
            BindingOperations.SetBinding(
                target: this.dockingManager,
                dp: DockingManager.DocumentsSourceProperty,
                binding: new Binding(@"Documents") { Source = this });

            // now that the binding is in place sync the views in the region with the DP on the
            // region host control instance.
            foreach (object view in this.Region.Views) {
                this.documents.Add(view);
            }
        }
    }
}
