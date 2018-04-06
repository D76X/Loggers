//using LogXtreme.Extensions;
//using LogXtreme.Ifrastructure.Enums;
//using LogXtreme.Reactive.Extensions;
//using LogXtreme.WinDsk.Infrastructure.Events;
//using LogXtreme.WinDsk.Infrastructure.Services;
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
//    /// 
//    /// Refs
//    /// https://stackoverflow.com/questions/31330031/in-avalondock-how-i-find-a-layoutcontent-by-conentid
//    /// https://stackoverflow.com/questions/23617707/binding-to-layoutanchorableitem-visibility-in-avalondock-2
//    /// </summary>
//    public class DockingManagerSyncBehavior :
//        RegionBehavior,
//        IHostAwareRegionBehavior,
//        IDisposable {

//        public static readonly string BehaviorKey = nameof(DockingManagerSyncBehavior);

//        private IAvalonDockService avalonDockService;
//        private IDisposable eventsubscription_DockingManagerChanged;
//        private DockingManager dockingManager;

//        private bool updatingActiveViewsInManagerActiveContentChanged;

//        private LayoutDocumentPane layoutDocumentPane;
//        private LayoutAnchorablePane layoutAnchorablePaneLeft;
//        private LayoutAnchorablePane layoutAnchorablePaneRight;

//        public DockingManagerSyncBehavior(
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

//        public DependencyObject HostControl {
//            get => this.dockingManager;
//            set => this.dockingManager = value as DockingManager;
//        }

//        public ReadOnlyObservableCollection<object> Documents {

//            get {

//                var documents = this.dockingManager.Layout.Descendents().OfType<LayoutDocument>();
//                var oc = new ObservableCollection<object>(documents);
//                return new ReadOnlyObservableCollection<object>(oc);                
//            }
//        }

//        public ReadOnlyObservableCollection<object> Anchorables {

//            get {

//                var anchorables = this.dockingManager.Layout.Descendents().OfType<LayoutAnchorable>();
//                var oc = new ObservableCollection<object>(anchorables);
//                return new ReadOnlyObservableCollection<object>(oc);                
//            }
//        }

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

//            this.dockingManager.ActiveContentChanged += this.ManagerActiveContentChanged;

//            this.Region.ActiveViews.CollectionChanged += this.ActiveViewsCollectionChanged;

//            this.Region.Views.CollectionChanged += this.ViewsCollectionChanged;
//        }

//        private void AvalonDockEventHandler(
//            object sender,
//            AvalonDockEventArgs args) {

//            UIElement view = sender as UIElement;

//            if (view == null || !(view is IAvalonDockView)) { return; }

//            if (args.EventType == AvalonDockEventEnum.AvalonDockViewNavigatedTo) {

//                // the view might be in an hidden anchorable
//                var anchorable = this.ShowAnchorable(view);

//                if (anchorable != null) { return; }

//                // if the view was not in an hidden anchorable it should not be 
//                // anywhere else
//                var content = this.dockingManager
//                        .Layout
//                        .Descendents()
//                        .OfType<LayoutContent>()
//                        .FirstOrDefault(a => a.ContentId == view.GetType().ToString());

//                if (content != null) {
//                    // log warning!
//                    return;
//                }                
//            }
//        }

//        private LayoutAnchorable ShowAnchorable(UIElement view) {

//            var anchorable = this.dockingManager
//                    .Layout
//                    .Descendents()
//                    .OfType<LayoutAnchorable>()
//                    .FirstOrDefault(a => a.ContentId == view.GetType().ToString());

//            if (anchorable == null) { return null; }

//            anchorable.Show();
//            return anchorable;
//        }

//        private void ManagerActiveContentChanged(
//            object sender,
//            EventArgs e) {

//            try {

//                this.updatingActiveViewsInManagerActiveContentChanged = true;

//                if (this.dockingManager == sender) {

//                    object activeContent = this.dockingManager.ActiveContent;

//                    var allOtherActiveViewsInTheRegion = this.Region
//                        .ActiveViews
//                        .Where(v => v != activeContent);

//                    foreach (var item in allOtherActiveViewsInTheRegion) {

//                        this.Region.Deactivate(item);
//                    }

//                    if (this.Region.Views.Contains(activeContent) &&
//                        !this.Region.ActiveViews.Contains(activeContent)) {

//                        // activate the view in the Region for the 
//                        // DockManager active content
//                        this.Region.Activate(activeContent);
//                    }
//                }
//            }
//            finally {

//                this.updatingActiveViewsInManagerActiveContentChanged = false;
//            }
//        }

//        private void ActiveViewsCollectionChanged(
//            object sender,
//            NotifyCollectionChangedEventArgs e) {

//            if (this.updatingActiveViewsInManagerActiveContentChanged) {
//                return;
//            }

//            if (this.dockingManager == null) {
//                return;
//            }

//            if (e.Action == NotifyCollectionChangedAction.Add) {

//                if (this.dockingManager.ActiveContent != null &&
//                    this.dockingManager.ActiveContent != e.NewItems[0] &&
//                    this.Region.ActiveViews.Contains(this.dockingManager.ActiveContent)) {

//                    this.Region.Deactivate(this.dockingManager.ActiveContent);
//                }

//                this.dockingManager.ActiveContent = e.NewItems[0];
//            }
//            else if (e.Action == NotifyCollectionChangedAction.Remove &&
//                    e.OldItems.Contains(this.dockingManager.ActiveContent)) {

//                this.dockingManager.ActiveContent = null;
//            }
//        }

//        private void ViewsCollectionChanged(
//            object sender,
//            NotifyCollectionChangedEventArgs e) {

//            if (e.Action == NotifyCollectionChangedAction.Add) {

//                int startIndex = e.NewStartingIndex;

//                foreach (object item in e.NewItems) {
//                    this.AddContent(item);                    
//                }
//            }
//            else if (e.Action == NotifyCollectionChangedAction.Remove) {

//                foreach (object oldItem in e.OldItems) {

//                    // not all removal may be triggered by the user click on the 
//                    // close button of a tab i.e. some removal may be caused by 
//                    // events programmatically. You must check that the view is
//                    // still in the region to remove it.

//                    if (this.Region.Views.Contains(oldItem)) {
//                        this.Region.Remove(oldItem);
//                    }
//                }
//            }
//        }

//        private LayoutContent AddContent(object item) {

//            UIElement uiElement = item as UIElement;

//            if (uiElement == null) {
//                new ArgumentException($"{nameof(item)} is not of type {nameof(UIElement)}");
//            }

//            var view = item as IAvalonDockView;

//            if (view == null) {
//                new ArgumentException($"{nameof(item)} is not of type {nameof(IAvalonDockView)}");
//            }

//            if (view.AvalonDockViewType == AvalonDockViewTypeEnum.Document) {

//                return this.AddDocument(item);

//            }
//            else if (view.AvalonDockViewType == AvalonDockViewTypeEnum.Anchorable) {

//                return this.AddAnchorable(item);
//            }

//            return null;
//        }

//        private LayoutAnchorable AddAnchorable(object item) {

//            UIElement uiElement = item as UIElement;

//            if (uiElement == null) {
//                new ArgumentException($"{nameof(item)} is not of type {nameof(UIElement)}");
//            }

//            var view = item as IAvalonDockView;

//            if (view == null) {
//                new ArgumentException($"{nameof(item)} is not of type {nameof(IAvalonDockView)}");
//            }

//            if (view.AvalonDockViewType != AvalonDockViewTypeEnum.Anchorable) {
//                new ArgumentException($"the {nameof(IAvalonDockView)} view is not set as {AvalonDockViewTypeEnum.Anchorable}");
//            }

//            var targetAnchorablePane = view.AvalonDockViewAnchor == AvalonDockViewAnchorEnum.Right ?
//                            this.layoutAnchorablePaneRight :
//                            this.layoutAnchorablePaneLeft;

//            LayoutAnchorable anchorable = new LayoutAnchorable();
//            anchorable.Content = item;

//            //title? metadata?...
//            anchorable.Title = view.GetType().Name;
//            anchorable.ContentId = uiElement.GetType().ToString();            
//            // prevent the tab from being closed when it is in the dock
//            anchorable.CanClose = false;

//            targetAnchorablePane.Children.Add(anchorable);

//            anchorable.Closing += AnchorableClosing;

//            anchorable.Closed += (s, args) => {
//                RemoveContentFromDockManagerRegionWhenUserClickOnTheTabClosingButton(anchorable);
//            };

//            return anchorable;
//        }

//        private LayoutDocument AddDocument(object item) {

//            UIElement uiElement = item as UIElement;

//            if (uiElement == null) {
//                new ArgumentException($"{nameof(item)} is not of type {nameof(UIElement)}");
//            }

//            var view = item as IAvalonDockView;

//            if (view == null) {
//                new ArgumentException($"{nameof(item)} is not of type {nameof(IAvalonDockView)}");
//            }

//            if (view.AvalonDockViewType != AvalonDockViewTypeEnum.Document) {
//                new ArgumentException($"the {nameof(IAvalonDockView)} view is not set as {AvalonDockViewTypeEnum.Document}");
//            }

//            LayoutDocument document = new LayoutDocument();
//            document.Content = item;

//            // apply some metadata - you need a better thing here!
//            document.Title = uiElement.GetType().ToString();

//            layoutDocumentPane.Children.Add(document);

//            document.Closed += (s, args) => {
//                RemoveContentFromDockManagerRegionWhenUserClickOnTheTabClosingButton(document);
//            };

//            return document;
//        }

//        private void AnchorableClosing(
//            object sender, 
//            System.ComponentModel.CancelEventArgs e) {
//            //e.Cancel = true;
//            // log...
//        }

//        private void RemoveContentFromDockManagerRegionWhenUserClickOnTheTabClosingButton(
//            LayoutContent item) {

//            LayoutContent layoutContent = item as LayoutContent;

//            if (layoutContent == null || layoutContent.Content == null) { return; }

//            // this should remove the view from the region.
//            this.Region.Remove(layoutContent.Content);
//        }

//        private void SynchronizeItems() {

//            this.layoutDocumentPane = this.dockingManager.FindName("layoutDocumentPane") as LayoutDocumentPane;

//            this.layoutAnchorablePaneLeft = this.dockingManager.FindName("leftLayoutAnchorablePane") as LayoutAnchorablePane;
//            this.layoutAnchorablePaneLeft.AllowDuplicateContent = false;
//            this.layoutAnchorablePaneLeft.Name = "LeftPane";
//            this.layoutAnchorablePaneLeft.ChildrenCollectionChanged += LayoutAnchorablePaneLeft_ChildrenCollectionChanged;            

//            this.layoutAnchorablePaneRight = this.dockingManager.FindName("rightLayoutAnchorablePane") as LayoutAnchorablePane;
//            this.layoutAnchorablePaneRight.AllowDuplicateContent = false;
//            this.layoutAnchorablePaneRight.Name = "RightPane";
//            this.layoutAnchorablePaneRight.ChildrenCollectionChanged += LayoutAnchorablePaneRight_ChildrenCollectionChanged;    

//            this.dockingManager.DocumentClosing += DockingManager_DocumentClosing;
//            this.dockingManager.DocumentClosed += DockingManager_DocumentClosed;        

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
//        }

//        private void DockingManager_DocumentClosing(object sender, DocumentClosingEventArgs e) {
//            // log
//        }

//        private void DockingManager_DocumentClosed(object sender, DocumentClosedEventArgs e) {
//            // log
//        }

//        private void LayoutAnchorablePaneRight_ChildrenCollectionChanged(object sender, EventArgs e) {
//            // log
//        }

//        private void LayoutAnchorablePaneLeft_ChildrenCollectionChanged(object sender, EventArgs e) {
//            // log
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
