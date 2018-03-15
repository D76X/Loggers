using LogXtreme.Infrastructure.ContractValidators;
using Prism.Regions;
using System;
using System.Collections.Specialized;
using System.Linq;
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
    /// control when views are injected or removed from the region respectively. 
    /// 
    /// It also tells Prism what kind of region it is. In this case the override 
    /// <see cref="CreateRegion"/> returns an instance of <see cref="SingleActiveRegion"/>
    /// to let the Prism framework know that in this region only one wie may be 
    /// active at any given time.
    /// 
    /// Refs
    /// http://avalondock.codeplex.com/discussions/390255
    /// </summary>
    public class RegionAdapterLayoutAnchorable : RegionAdapterBase<LayoutAnchorable> {

        public RegionAdapterLayoutAnchorable(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory) {

        }

        /// <summary>
        /// Provides events handlers to sych the region views with the 
        /// content of the underlying instance of <see cref="LayoutAnchorable"/>        /// 
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

            region.Views.CollectionChanged += (s, e) => {

                if (e.Action == NotifyCollectionChangedAction.Add &&
                                region.ActiveViews.Count() == 0) {

                    region.Activate(e.NewItems[0]);
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove) {

                    // should I do anything here?
                }
            };

            region.ActiveViews.CollectionChanged += (s, e) => {

                regionTarget.Content = region.ActiveViews.FirstOrDefault();
            };
        }

        /// <summary>
        /// Only a sinlge view can be active in the region at any one time.
        /// </summary>
        /// <returns></returns>
        protected override IRegion CreateRegion() =>
            new SingleActiveRegion();
    }
}
