﻿//using LogXtreme.Extensions;
//using LogXtreme.Ifrastructure.Enums;
//using LogXtreme.Reactive.Extensions;
//using LogXtreme.WinDsk.Infrastructure.Events;
//using LogXtreme.WinDsk.Infrastructure.Services;
//using LogXtreme.WinDsk.Infrastructure.Utils;
//using Prism.Regions;
//using Prism.Regions.Behaviors;
//using System;
//using System.Collections.ObjectModel;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Reactive.Linq;
//using System.Windows;
//using System.Windows.Data;
//using Xceed.Wpf.AvalonDock;
//using Xceed.Wpf.AvalonDock.Layout;

//namespace LogXtreme.WinDsk.Infrastructure.Prism {

//    /// <summary>
//    /// This RegionBehavior is used to turn AvaloDock DockingManager control into a Prism region.
//    /// 
//    /// The role of <see cref="DockingManagerDocumentsSourceSyncBehavior"/> is to provide 
//    /// synchronization logic between the underlying control DockingManager and the corresponding
//    /// Prism region. Prism sees the the underlying DockingManager instance as a named region thus
//    /// it needs to decide what to do when instances of LayoutAnchorable or LayoutDocument are 
//    /// added to or removed from the underlying instance of DockingManager. 
//    /// 
//    /// In the OnAttached override for this behavior.
//    /// 
//    /// 1- a binding is set between the DP DocumentsSourceProperty on the underlying control 
//    /// DockingManager and the Documents property on the behavior.
//    /// 
//    /// 2- the behaviors subscribes and provides an an handler for the event that 
//    /// DockingManager.ActiveContentChanged so that the Prism region can correctly set the active 
//    /// view when the active document or anchorable in the underlying DockingManager instance
//    /// changes.
//    /// 
//    /// 3- the behaviors subscribes and provides an an handler for the event 
//    /// this.Region.ActiveViews.CollectionChanged so that when the active view in the region for 
//    /// the underlying DockingManager instance the DockingManager synchs its ActiveContent
//    /// property to the corresponding item.
//    ///       
//    /// 4- the behaviors subscribes and provides an an handler for the event 
//    /// this.Region.Views.CollectionChanged so that whan a region internal to the underlying 
//    /// DockingManager instance is added or removed from the Region for the DockingManager instance 
//    /// the underlying DockingManager instance can synch its own document property to reflect 
//    /// such addition/removal. 
//    /// 
//    /// Refs
//    /// 
//    /// This is a course where it is explained how to design and use a Prism RegionBehavior
//    /// in relation to the TabControl. 
//    /// https://app.pluralsight.com/player?course=prism-mastering-tabcontrol&author=brian-lagunas&name=prism-mastering-tabcontrol-m4&clip=7&mode=live
//    /// 
//    /// These are two GitHub public repos where this is exact behavior is used. 
//    /// https://github.com/miseeger/MashedVVM/blob/master/Prism/RegionAdapter/AvalonDock/DockingManagerDocumentsSourceSyncBehavior.cs
//    /// https://github.com/alfredoperez/Central/blob/master/Central.Infrastructure/Behaviors/DockingManagerDocumentsSourceSyncBehavior.cs
//    /// 
//    /// This is the original thread where the issue of enhancing AvalonDock into a Prism region 
//    /// was discussed.
//    /// http://avalondock.codeplex.com/discussions/390255
//    /// 
//    /// 
//    /// https://stackoverflow.com/questions/17297581/get-hostcontrol-from-region-in-prism
//    /// 
//    /// Refs on binding
//    /// https://msdn.microsoft.com/en-us/library/system.windows.data.bindingoperations.setbinding(v=vs.110).aspx
//    /// https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/how-to-create-a-binding-in-code
//    /// https://www.codeproject.com/Articles/483507/AvalonDock-Tutorial-Part-Adding-a-Tool-Windo
//    /// https://github.com/Dirkster99/Edi
//    /// </summary>
//    public class DockingManagerDocumentsSourceSyncBehavior :
//        RegionBehavior,
//        IHostAwareRegionBehavior,
//        IDisposable {

//        public static readonly string BehaviorKey = nameof(DockingManagerDocumentsSourceSyncBehavior);

//        public ObservableCollection<object> documents = new ObservableCollection<object>();
//        public ReadOnlyObservableCollection<object> readonlyDocuments = null;

//        public ObservableCollection<object> anchorables = new ObservableCollection<object>();
//        public ReadOnlyObservableCollection<object> readonlyAnchorables = null;

//        private bool updatingActiveViewsInManagerActiveContentChanged;
//        private DockingManager dockingManager;

//        private IAvalonDockService avalonDockService;
//        private IDisposable eventsubscription_DockingManagerChanged;

//        public DockingManagerDocumentsSourceSyncBehavior(
//            IAvalonDockService avalonDockService) {

//            this.avalonDockService = avalonDockService;

//            this.eventsubscription_DockingManagerChanged = Observable
//                .FromEventPattern<AvalonDockEventArgs>(
//                h => this.avalonDockService.DockingManagerChanged += h,
//                h => this.avalonDockService.DockingManagerChanged -= h)
//                .SubscribeWeakly(
//                this,
//                (target, ep) => target.AvalonDockEventHandler(ep.Sender, ep.EventArgs));
//        }

//        /// <summary>
//        /// We want to turn a DockingManager into a region, thus the HostControl must be an instance 
//        /// of DockingManager. The Region Adapter that attaches this behavior must set the host 
//        /// control on it before adding to its collection of region behaviors.
//        /// </summary>
//        public DependencyObject HostControl {
//            get => this.dockingManager;
//            set => this.dockingManager = value as DockingManager;
//        }

//        public ReadOnlyObservableCollection<object> Documents {

//            get {

//                return readonlyDocuments == null ?
//                       readonlyDocuments = new ReadOnlyObservableCollection<object>(this.documents) :
//                       readonlyDocuments;
//            }
//        }

//        public ReadOnlyObservableCollection<object> Anchorables {

//            get {

//                return readonlyAnchorables == null ?
//                       readonlyAnchorables = new ReadOnlyObservableCollection<object>(this.anchorables) :
//                       readonlyAnchorables;
//            }
//        }

//        /// <summary>
//        /// Starts to monitor the <see cref="IRegion"/> to keep it in synch with the items of the 
//        /// <see cref="HostControl"/>
//        /// </summary>
//        protected override void OnAttach() {

//            var documentsSource = this.dockingManager.DocumentsSource;

//            if (documentsSource != null &&
//                documentsSource.ToList<object>().Any()) {

//                // we want to begin the synch from the state where the DockingManager
//                // does not have documents bound to it. 
//                // Is this really necessary?
//                throw new InvalidOperationException();
//            }

//            this.SynchronizeItems();

//            // this is OK when a new document is added it fires
//            this.dockingManager.ActiveContentChanged += this.ManagerActiveContentChanged;
//            this.dockingManager.DocumentClosed += DockingManager_DocumentClosed;

//            // these are on the region and never fire - why?
//            this.Region.ActiveViews.CollectionChanged += this.ActiveViewsCollectionChanged;
//            this.Region.Views.CollectionChanged += this.ViewsCollectionChanged;
//        }

//        private void DockingManager_DocumentClosed(
//            object sender,
//            DocumentClosedEventArgs e) {

//            // what to do here?
//        }

//        private void AvalonDockEventHandler(
//            object sender,
//            AvalonDockEventArgs args) {

//            // all that we need to know
//            var eventType = args.EventType;
//            var cArgs = args.NotifyCollectionChangedEventArgs;
//            var action = cArgs.Action;
//            var _oldItem = cArgs.OldStartingIndex > -1 ? cArgs.OldItems[0] : null;
//            var _newItem = cArgs.NewStartingIndex > -1 ? cArgs.NewItems[0] : null;

//            if (this.dockingManager == null) { return; }

//            ObservableCollection<object> target = null;

//            if (sender is RegionAdapterLayoutDocumentPane) {
//                target = this.documents;
//            }
//            else if (sender is RegionAdapterLayoutAnchorable) {
//                target = this.anchorables;
//            }
//            else {
//                throw new ArgumentException($"{nameof(sender)} of type {sender.GetType()} is invalid.");
//            }

//            if (action == NotifyCollectionChangedAction.Add &&
//                eventType == AvalonDockEventEnum.ViewsCollectionChanged) {

//                int startIndex = cArgs.NewStartingIndex;

//                foreach (object newItem in cArgs.NewItems) {
//                    target.Insert(startIndex++, newItem);
//                }
//            }
//            else if (action == NotifyCollectionChangedAction.Add &&
//                eventType == AvalonDockEventEnum.ActiveViewsCollectionChanged) {

//                //if (this.dockingManager.ActiveContent != null &&
//                //    this.dockingManager.ActiveContent != cArgs.NewItems[0] &&
//                //    this.Region.ActiveViews.Contains(this.dockingManager.ActiveContent)) {

//                //    this.Region.Deactivate(this.dockingManager.ActiveContent);
//                //}

//                //if (this.dockingManager.ActiveContent != null &&
//                //    this.dockingManager.ActiveContent != cArgs.NewItems[0]) {

//                //    this.Region.Deactivate(this.dockingManager.ActiveContent);
//                //}

//                this.dockingManager.ActiveContent = cArgs.NewItems[0];
//            }
//            else if (action == NotifyCollectionChangedAction.Remove &&
//                eventType == AvalonDockEventEnum.ViewsCollectionChanged) {

//                foreach (object oldItem in cArgs.OldItems) {
//                    target.Remove(oldItem);
//                }
//            }
//            else if (action == NotifyCollectionChangedAction.Remove &&
//                eventType == AvalonDockEventEnum.ActiveViewsCollectionChanged) {

//                if (cArgs.OldItems.Contains(this.dockingManager.ActiveContent)) {
//                    this.dockingManager.ActiveContent = null;
//                }
//            }
//        }

//        private void ManagerActiveContentChanged(
//            object sender,
//            EventArgs e) {

//            // access the datacontext is one exists!

//        }

//        /// <summary>
//        /// Handles any change of the region views collection.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void ViewsCollectionChanged(
//            object sender,
//            NotifyCollectionChangedEventArgs e) {

//            //if (e.Action == NotifyCollectionChangedAction.Add) {

//            //    int startIndex = e.NewStartingIndex;

//            //    foreach (object newItem in e.NewItems) {

//            //        this.documents.Insert(startIndex++, newItem);
//            //    }
//            //}
//            //else if (e.Action == NotifyCollectionChangedAction.Remove) {

//            //    foreach (object oldItem in e.OldItems) {

//            //        this.documents.Remove(oldItem);
//            //    }
//            //}
//        }

//        /// <summary>
//        /// Handles a change of the active view on the Region.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void ActiveViewsCollectionChanged(
//            object sender,
//            NotifyCollectionChangedEventArgs e) {

//            //if (this.updatingActiveViewsInManagerActiveContentChanged) {
//            //    return;
//            //}

//            //if (this.dockingManager == null) {
//            //    return;
//            //}

//            //if (e.Action == NotifyCollectionChangedAction.Add) {

//            //    if (this.dockingManager.ActiveContent != null &&
//            //        this.dockingManager.ActiveContent != e.NewItems[0] &&
//            //        this.Region.ActiveViews.Contains(this.dockingManager.ActiveContent)) {

//            //        this.Region.Deactivate(this.dockingManager.ActiveContent);
//            //    }

//            //    this.dockingManager.ActiveContent = e.NewItems[0];
//            //}
//            //else if (e.Action == NotifyCollectionChangedAction.Remove &&
//            //        e.OldItems.Contains(this.dockingManager.ActiveContent)) {

//            //    this.dockingManager.ActiveContent = null;
//            //}
//        }


//        /// <summary>
//        /// Handles a change event of the active content on the docking manager.
//        /// When such change is detected the view for the active content on the 
//        /// DockingManager must become active on the corresponding region. At the
//        /// same time all other views on the region must be deactivated. This will
//        /// allow the region to display the view corresponding to the active content
//        /// on the host control instance DockingManager.
//        /// </summary>
//        /// <param name="sender">Should be the DockingManager instance.</param>
//        /// <param name="e"></param>
//        //private void ManagerActiveContentChanged(
//        //    object sender,
//        //    EventArgs e) {

//        //    try {

//        //        this.updatingActiveViewsInManagerActiveContentChanged = true;

//        //        if (this.dockingManager == sender) {

//        //            object activeContent = this.dockingManager.ActiveContent;

//        //            var allOtherActiveViewsInTheRegion = this.Region
//        //                .ActiveViews
//        //                .Where(v => v != activeContent);

//        //            foreach (var item in allOtherActiveViewsInTheRegion) {

//        //                this.Region.Deactivate(item);
//        //            }

//        //            if (this.Region.Views.Contains(activeContent) &&
//        //                !this.Region.ActiveViews.Contains(activeContent)) {

//        //                // activate the view in the Region for the 
//        //                // DockManager active content
//        //                this.Region.Activate(activeContent);
//        //            }
//        //        }
//        //    }
//        //    finally {

//        //        this.updatingActiveViewsInManagerActiveContentChanged = false;
//        //    }
//        //}

//        /// <summary>
//        /// Creates a binding between this behavior Documents property and the DockingManager
//        /// DocumentsSourceProperty dependency property on the instance of the DockingManager
//        /// that is used as a host for the region.  
//        /// </summary>
//        private void SynchronizeItems() {

//            if (this.Region.RegionManager == null) {

//                this.Region.RegionManager = PrismUtils.GetScopedRegionManager(this.dockingManager);
//            }

//            // there might be some sync to do
//            foreach (var registeredRegion in this.avalonDockService.RegisteredRegionNames) {

//                var regionName = registeredRegion.Key;
//                var regionType = registeredRegion.Value;
//                ObservableCollection<object> target = null;

//                var region = this.Region.RegionManager.Regions[regionName];

//                if (region == null) { continue; }

//                if (regionType.Equals(typeof(LayoutDocument))) {
//                    target = this.documents;
//                }
//                else if (regionType.Equals(typeof(LayoutAnchorable))) {
//                    target = this.anchorables;
//                }
//                else {
//                    continue;
//                }

//                foreach (var view in region.Views) {

//                    if (target.FirstOrDefault(v => v == view) == null) {
//                        target.Add(view);
//                    }
//                }
//            }

//            // BindingOperations attaches a binding to an arbitrary DependencyObject that may 
//            // not expose its own SetBinding method. In this case we want to set a binding
//            // between this instance of the DockingManagerDocumentsSourceSyncBehavior and the 
//            // instance of the DockingManager that is the host control for the region. This
//            // syncs this instance DockingManagerDocumentsSourceSyncBehavior.Documents property
//            // to the DockingManager.DocumentsSourceProperty DP on the target.
//            BindingOperations.SetBinding(
//                target: this.dockingManager,
//                dp: DockingManager.DocumentsSourceProperty,
//                binding: new Binding(@"Documents") { Source = this });

//            BindingOperations.SetBinding(
//                target: this.dockingManager,
//                dp: DockingManager.AnchorablesSourceProperty,
//                binding: new Binding(@"Anchorables") { Source = this });

//            var dockingManagerViewModel = this.dockingManager.DataContext as IDockingManagerViewModel;

//            if (dockingManagerViewModel != null) {

//                //BindingOperations.SetBinding(
//                //target: dockingManagerViewModel,
//                //dp: DockingManager.DocumentsSourceProperty,
//                //binding: new Binding(@"Documents") { Source = this });

//                //BindingOperations.SetBinding(
//                //    target: dockingManagerViewModel,
//                //    dp: DockingManager.AnchorablesSourceProperty,
//                //    binding: new Binding(@"Anchorables") { Source = this });

//                dockingManagerViewModel.Anchorables = this.anchorables;
//                dockingManagerViewModel.Documents = this.documents;
//                dockingManagerViewModel.ActiveContent = this.dockingManager.ActiveContent;
//            }
//        }

//        #region IDisposable

//        private bool disposedValue = false; // To detect redundant calls         

//        protected virtual void Dispose(bool disposing) {

//            if (!disposedValue) {

//                if (disposing) {

//                    this.eventsubscription_DockingManagerChanged?.Dispose();
//                    this.eventsubscription_DockingManagerChanged = null;
//                }

//                // free unmanaged resources (unmanaged objects) and override a finalizer below.
//                // set large fields to null.

//                disposedValue = true;
//            }
//        }

//        public void Dispose() {
//            this.Dispose(true);
//            //GC.SuppressFinalize(this);
//        }

//        #endregion
//    }
//}
