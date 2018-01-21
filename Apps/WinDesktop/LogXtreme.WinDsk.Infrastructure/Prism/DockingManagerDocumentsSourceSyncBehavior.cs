﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Xceed.Wpf.AvalonDock;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary>
    /// Refs
    /// http://avalondock.codeplex.com/discussions/390255
    /// https://stackoverflow.com/questions/17297581/get-hostcontrol-from-region-in-prism
    /// Refs on binding
    /// https://msdn.microsoft.com/en-us/library/system.windows.data.bindingoperations.setbinding(v=vs.110).aspx
    /// https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/how-to-create-a-binding-in-code
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
        /// of DockingManager. The Region Adapter that attaches thi behaviors must set the host 
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

            bool itemsSourceIsSet = this.dockingManager.DocumentsSource != null;

            if (itemsSourceIsSet) {
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
