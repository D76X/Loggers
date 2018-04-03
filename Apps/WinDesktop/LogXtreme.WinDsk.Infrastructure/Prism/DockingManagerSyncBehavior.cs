using LogXtreme.Extensions;
using LogXtreme.Ifrastructure.Enums;
using LogXtreme.Reactive.Extensions;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.Infrastructure.Utils;
using Prism.Regions;
using Prism.Regions.Behaviors;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Data;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    public class DockingManagerSyncBehavior :
        RegionBehavior,
        IHostAwareRegionBehavior,
        IDisposable {

        public static readonly string BehaviorKey = nameof(DockingManagerSyncBehavior);

        private IAvalonDockService avalonDockService;
        private IDisposable eventsubscription_DockingManagerChanged;
        private DockingManager dockingManager;
        //private ObservableCollection<object> documents = new ObservableCollection<object>();
        //private ReadOnlyObservableCollection<object> readonlyDocuments = null;
        //private ObservableCollection<object> anchorables = new ObservableCollection<object>();
        //private ReadOnlyObservableCollection<object> readonlyAnchorables = null;

        private bool updatingActiveViewsInManagerActiveContentChanged;

        private LayoutDocumentPane layoutDocumentPane;
        private LayoutAnchorablePane layoutAnchorablePaneLeft;
        private LayoutAnchorablePane layoutAnchorablePaneRight;

        public DockingManagerSyncBehavior(
            IAvalonDockService avalonDockService) {

            this.avalonDockService = avalonDockService;

            this.eventsubscription_DockingManagerChanged = Observable
                .FromEventPattern<AvalonDockEventArgs>(
                h => this.avalonDockService.DockingManagerChanged += h,
                h => this.avalonDockService.DockingManagerChanged -= h)
                .SubscribeWeakly(
                this,
                (target, ep) => target.AvalonDockEventHandler(ep.Sender, ep.EventArgs));
        }

        public DependencyObject HostControl {
            get => this.dockingManager;
            set => this.dockingManager = value as DockingManager;
        }

        public ReadOnlyObservableCollection<object> Documents {

            get {

                if (this.layoutDocumentPane == null) {
                    return null;
                }

                return new ReadOnlyObservableCollection<object>(new ObservableCollection<object>(this.layoutDocumentPane.Children));
            }
        }

        public ReadOnlyObservableCollection<object> Anchorables {

            get {

                if (this.layoutAnchorablePaneLeft == null &&
                    this.layoutAnchorablePaneRight == null) {
                    return null;
                }

                var oc = new ObservableCollection<object>();

                if (this.layoutAnchorablePaneLeft != null) {
                    oc.AddRange(this.layoutAnchorablePaneLeft.Children);
                }

                if (this.layoutAnchorablePaneRight != null) {
                    oc.AddRange(this.layoutAnchorablePaneRight.Children);
                }

                return new ReadOnlyObservableCollection<object>(oc);
            }            
        }

        //public ReadOnlyObservableCollection<object> Documents {

        //    get {

        //        return readonlyDocuments == null ?
        //               readonlyDocuments = new ReadOnlyObservableCollection<object>(this.documents) :
        //               readonlyDocuments;
        //    }
        //}

        //public ReadOnlyObservableCollection<object> Anchorables {

        //    get {

        //        return readonlyAnchorables == null ?
        //               readonlyAnchorables = new ReadOnlyObservableCollection<object>(this.anchorables) :
        //               readonlyAnchorables;
        //    }
        //}

        protected override void OnAttach() {

            var documentsSource = this.dockingManager.DocumentsSource;

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

        private void AvalonDockEventHandler(
            object sender,
            AvalonDockEventArgs args) { }

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

        private void ActiveViewsCollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs e) {

            if (this.updatingActiveViewsInManagerActiveContentChanged) {
                return;
            }

            if (this.dockingManager == null) {
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

        private void ViewsCollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs e) {

            if (e.Action == NotifyCollectionChangedAction.Add) {

                int startIndex = e.NewStartingIndex;

                foreach (object item in e.NewItems) {

                    UIElement uiElement = item as UIElement;

                    if (uiElement == null) {
                        new ArgumentException($"{nameof(item)} is not of type {nameof(UIElement)}");
                    }

                    var view = item as IAvalonDockView;
                   
                    if (view == null) {
                        new ArgumentException($"{nameof(item)} is not of type {nameof(IAvalonDockView)}");
                    }

                    if (view.AvalonDockViewType == AvalonDockViewTypeEnum.Document) {

                        LayoutDocument document = new LayoutDocument();
                        document.Content = item;

                        // apply some metadata - you need a better thing here!
                        document.Title = uiElement.GetType().ToString();

                        layoutDocumentPane.Children.Add(document);                       

                        document.Closed += (s, args) => {
                            RemoveContentFromDockManagerRegionWhenUserClickOnTheTabClosingButton(document);
                        };
                    }
                    else if (view.AvalonDockViewType == AvalonDockViewTypeEnum.Anchorable) {

                        var targetAnchorablePane = view.AvalonDockViewAnchor == AvalonDockViewAnchorEnum.Right ?
                            this.layoutAnchorablePaneRight:
                            this.layoutAnchorablePaneLeft;

                        LayoutAnchorable anchorable = new LayoutAnchorable();
                        anchorable.Content = item;

                        //title? metadata?...
                        //...

                        targetAnchorablePane.Children.Add(anchorable);

                        anchorable.Closed += (s, args) => {
                            RemoveContentFromDockManagerRegionWhenUserClickOnTheTabClosingButton(anchorable);
                        };
                    }                    
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove) {

                foreach (object oldItem in e.OldItems) {

                    // not all removal may be triggered by the user click on the 
                    // close button of a tab i.e. some removal may be caused by 
                    // events programmatically. You must check that the view is
                    // still in the region to remove it.

                    if (this.Region.Views.Contains(oldItem)) {
                        this.Region.Remove(oldItem);
                    }                                 
                }
            }
        }

        private void RemoveContentFromDockManagerRegionWhenUserClickOnTheTabClosingButton(
            LayoutContent item) {

            LayoutContent layoutContent = item as LayoutContent;

            if (layoutContent == null || layoutContent.Content == null) { return; }

            // this should remove the view from the region.
            this.Region.Remove(layoutContent.Content);

            //should this raise an event?
        }

        private void SynchronizeItems() {
            
            this.layoutDocumentPane = this.dockingManager.FindName("layoutDocumentPane") as LayoutDocumentPane;
            this.layoutAnchorablePaneLeft = this.dockingManager.FindName("leftLayoutAnchorablePane") as LayoutAnchorablePane;
            this.layoutAnchorablePaneRight = this.dockingManager.FindName("rightLayoutAnchorablePane") as LayoutAnchorablePane;

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

            BindingOperations.SetBinding(
                target: this.dockingManager,
                dp: DockingManager.AnchorablesSourceProperty,
                binding: new Binding(@"Anchorables") { Source = this });
        }

        #region IDisposable

        private bool disposedValue = false; // To detect redundant calls         

        protected virtual void Dispose(bool disposing) {

            if (!disposedValue) {

                if (disposing) {

                    this.eventsubscription_DockingManagerChanged?.Dispose();
                    this.eventsubscription_DockingManagerChanged = null;
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose() {
            this.Dispose(true);
            //GC.SuppressFinalize(this);
        }

        #endregion
    }
}
