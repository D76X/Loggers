using LogXtreme.Ifrastructure.Enums;
using LogXtreme.Infrastructure.ContractValidators;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Services;
using Prism.Regions;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary>
    /// A Prism region adapter to make <see cref="LayoutAnchorable"/> into a Prism 
    /// region.
    /// 
    /// As for any other Prism region adapter it handles the communication between 
    /// the the Prism region and its underlying control which in this case is a 
    /// <see cref="LayoutAnchorable"/>. 
    /// 
    /// It provides the logic to add or remove FrameworkElements from the underilying 
    /// control when views are injected or removed from the region respectively. It 
    /// also hanles the upkeeping of the active views for the region in conjuction 
    /// with the evemts of addition and removal of views from the region. 
    /// This logic is implemented in the form of event hanlders attached to the relevant
    /// events on the region's collections of views and active views. 
    /// All of this is done in the override  <see cref="Adapt"/>.
    /// 
    /// It also tells Prism what kind of region it is. In this case the override 
    /// <see cref="CreateRegion"/> returns an instance of <see cref="SingleActiveRegion"/>
    /// to let the Prism framework know that in this region only one wie may be 
    /// active at any given time.
    ///     
    /// 
    /// Refs
    /// http://avalondock.codeplex.com/discussions/390255
    /// </summary>
    public class RegionAdapterLayoutAnchorable : RegionAdapterBase<LayoutAnchorable> {

        private IAvalonDockService avalonDockService;

        public RegionAdapterLayoutAnchorable(
            IRegionBehaviorFactory regionBehaviorFactory,
            IAvalonDockService avalonDockService)
            : base(regionBehaviorFactory) {

            this.avalonDockService = avalonDockService;
        }

        /// <summary>
        /// Provides events handlers to sych the region views with the 
        /// content of the underlying instance of <see cref="LayoutAnchorable"/>         
        /// </summary>
        /// <param name="region">The Prism region for the instance of <see cref="LayoutAnchorable"/></param>
        /// <param name="regionTarget">the instance of <see cref="LayoutAnchorable"/></param>
        protected override void Adapt(
            IRegion region,
            LayoutAnchorable regionTarget) {

            regionTarget.Validate(nameof(regionTarget)).NotNull();

            if (regionTarget.Content != null) {
                throw new InvalidOperationException();
            }

            this.avalonDockService.RegisterRegionName<LayoutAnchorable>(region.Name);

            // monitor when views are injectedinto or removed from the region.
            region.Views.CollectionChanged += (s, e) => {

                UIElement childUiElement = s as UIElement;

                if (e.Action == NotifyCollectionChangedAction.Add &&
                                region.ActiveViews.Count() == 0) {

                    // a view has been injeted into the region.
                    // the injected view must be set to be the active view for the region.
                    // this type of region can only have one view in it at any time.
                    var lastAddedView = e.NewItems[0];
                    region.Activate(lastAddedView);
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove) {
                    region.Deactivate(e.OldItems[0]);
                    region.Remove(e.OldItems[0]);
                }

                this.avalonDockService
                    .RaiseDockingManagerChanged(this, new AvalonDockEventArgs(
                        e, 
                        AvalonDockEventEnum.ViewsCollectionChanged));         
            };

            region.ActiveViews.CollectionChanged += (s, e) => {

                var newContent = region.ActiveViews.FirstOrDefault();
                regionTarget.Content = newContent;

                this.avalonDockService
                    .RaiseDockingManagerChanged(this, new AvalonDockEventArgs(
                        e,
                        AvalonDockEventEnum.ActiveViewsCollectionChanged));
            };            
        }

        /// <summary>
        /// Only a sinlge view can be active in the region at any one time.
        /// </summary>
        /// <returns><see cref="IRegion"/></returns>
        protected override IRegion CreateRegion() =>
            new SingleActiveRegion();      
    }
}
