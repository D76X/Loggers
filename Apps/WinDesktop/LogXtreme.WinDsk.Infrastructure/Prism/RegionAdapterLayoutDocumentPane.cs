using LogXtreme.Ifrastructure.Enums;
using LogXtreme.Infrastructure.ContractValidators;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Services;
using Prism.Regions;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary>
    /// 
    /// In the AvalonDock library the LayoutDocumentPane playes the role of the TabControl.
    /// In a LayoutDocumentPane there would normally be a number of <see cref="LayoutDocument"/> 
    /// instances which correspond to the TabItems of a TabControl.
    /// 
    /// In order to be able to make an instance of <see cref="LayoutDocumentPane"/> into a Prism 
    /// region it is necessary to have a corresponding RegionAdapter which specifies to Prism what 
    /// kind of region it is. This is a done in the override <see cref="CreateRegion"/>.
    /// 
    /// The region adapter also provides the logic to add or remove FrameworkElements from the 
    /// underilying control when views are injected or removed from the region respectively.
    /// This is done in the override <see cref="Adapt"/>.
    /// 
    /// Notice that <see cref="LayoutDocumentPane"/> does not descend from any of the WPF classes 
    /// for which Prism provides default region adapters as shown in the refrences below. Hence,
    /// while for a TabControl there exist already a <see cref="SelectorRegionAdapter"/> in Prism
    /// implementing the logic to synch a region on a TabControl with the underlying instance of 
    /// the TabControl this is not the case for <see cref="LayoutDocumentPane"/> for which it is 
    /// necessary to code a cusrom RegionAdapter as below.
    /// 
    /// https://xceed.com/wp-content/documentation/xceed-toolkit-plus-for-wpf/Xceed.Wpf.AvalonDock~Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPane.html
    /// https://stackoverflow.com/questions/1404299/available-prism-region-controls   ///    
    /// 
    /// For the logic that handles the removal of documents from the region on a <see cref="LayoutDocumentPane"/>
    /// refer to the <see cref="LogXtreme.WinDsk.Infrastructure.TriggerActions.CloseTabAction "/>class 
    /// implementation.
    /// 
    /// Refs
    /// https://xceed.com/wp-content/documentation/xceed-toolkit-plus-for-wpf/Xceed.Wpf.AvalonDock~Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPane.html
    /// This reference talks about implementing the region adapter for <see cref="LayoutDocumentPane"/>
    /// http://blog.raffaeu.com/archive/2010/07/04/wpf-and-prism-tab-region-adapter-part-02.aspx
    /// </summary>
    public class RegionAdapterLayoutDocumentPane : RegionAdapterBase<LayoutDocumentPane> {

        private IAvalonDockService avalonDockService;

        public RegionAdapterLayoutDocumentPane(
            IRegionBehaviorFactory regionBehaviorFactory,
            IAvalonDockService avalonDockService)
            : base(regionBehaviorFactory) {

            this.avalonDockService = avalonDockService;
        }

        /// <summary>
        /// Provides events handlers to sych the region views with the 
        /// content of the underlying instance of <see cref="LayoutDocumentPane"/>         
        /// </summary>
        /// <param name="region">The Prism region for the instance of <see cref="LayoutDocumentPane"/></param>
        /// <param name="regionTarget">the instance of <see cref="LayoutDocumentPane"/></param>
        protected override void Adapt(
            IRegion region,
            LayoutDocumentPane regionTarget) {

            regionTarget.Validate(nameof(regionTarget)).NotNull();

            this.avalonDockService.RegisterRegionName<LayoutDocument>(region.Name);

            // monitor when views are injectedinto or removed from the region. 
            region.Views.CollectionChanged += (s, e) => {               

                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {

                    // new views have been injeted into this region.
                    foreach (var item in e.NewItems) {

                        UIElement view = item as UIElement;

                        LayoutDocument document = new LayoutDocument();
                        document.Content = item;

                        // apply some metadata - you need a better thing here!
                        document.Title = view.GetType().ToString();                        

                        // the region target is the underlying instance of LayoutDocumentPane
                        // add the view to it to let the pane create the tab to host the view
                        // then make this last added view active. Multiple views can be active
                        // at the same time in the same LayoutDocument instance.
                        regionTarget.Children.Add(document);
                        region.Activate(view);

                        // the LayoutDocument.Close event happens when the user click on the 
                        // x button on the document tab. This is one of the possible ways a 
                        // view may be removed from the instance of LayoutDocument. We need 
                        // to make sure that the view is also removed from the associated 
                        // Prism region. Another possibility is that a document view is 
                        // removed programmatically following some event.
                        document.Closed += (sender, args) => {
                            RemoveItemFromRegionWhenTheDocumentIsClosed(sender, region);
                        };
                    }

                }
                else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {

                    // the removal of views from the instance of LayoutDocument to which a Prism region 
                    // is associated may not always be triggered by the user interaction of a user that 
                    // clicks on the x button on the corresponding document tab. For example we might 
                    // have a command or another event to trigger the removal.

                    var viewToRemove = e.OldItems[0];

                    if (viewToRemove == null) { return; }                    

                    // check that the view to remove is still in the region before trying to remove it.
                    // when the removal is triggered by the user by clicking on teh x of teh document 
                    // tab a specific hanlder might have already removed it.
                    if (!region.Views.Contains(viewToRemove)) { return; }

                    region.Remove(viewToRemove);
                }

                this.avalonDockService
                    .RaiseDockingManagerChanged(this, new AvalonDockEventArgs(
                        e,
                        AvalonDockEventEnum.ViewsCollectionChanged));
            };

            region.ActiveViews.CollectionChanged += (s, e) => {               
                
                // handle the changes of active views
                var activeViews = region.ActiveViews;

                this.avalonDockService
                    .RaiseDockingManagerChanged(this, new AvalonDockEventArgs(
                        e,
                        AvalonDockEventEnum.ActiveViewsCollectionChanged));
            };
        }

        /// <summary>
        /// This event handlers removes the view from the region associated to the instance of 
        /// <see cref="LayoutDocument"/> when the user clicks the x on the corresponding tab.
        /// </summary>
        /// <param name="item">the instance of <see cref="LayoutDocument"/></param>
        /// <param name="region">the region associated with the <see cref="LayoutDocument"/> instance</param>
        private void RemoveItemFromRegionWhenTheDocumentIsClosed(
            object item, 
            IRegion region) {

            // some more navigation logic here...
            LayoutDocument document = item as LayoutDocument;

            if (document == null || document.Content == null) { return; }            

            // this should remove the view from the region.
            region.Remove(document.Content);

            //should this raise an event?
        }

        /// <summary>
        /// For a prism region set on an instance of <see cref="LayoutDocumentPane"/>
        /// there might be multiple active documents (tabs) at any one time.
        /// </summary>
        /// <returns><see cref="IRegion"/></returns>
        protected override IRegion CreateRegion() =>
            new AllActiveRegion();
    }
}
