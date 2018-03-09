using LogXtreme.Infrastructure.ContractValidators;
using Prism.Regions;
using Xceed.Wpf.AvalonDock;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary>
    /// A Prism region adapter to make <see cref="DockingManager"/> into a Prism 
    /// region.
    /// 
    /// As for any other Prism region adapter it handles the communication 
    /// between the the Prism region and its underlying control which in this
    /// case is a <see cref="DockingManager"/>. 
    /// 
    /// It provides the logic to add or remove FrameworkElements from the 
    /// underilying control when views are injected or removed from the region 
    /// respectively. 
    /// 
    /// It also tells Prism what kind of region it is. In this case it is a 
    /// region that may hold several active views at any one time thus the 
    /// override <see cref="CreateRegion"/> returns an instance of Region.
    /// 
    /// This adapter is designed to work in conjunction with the custom behavior
    /// <see cref="DockingManagerDocumentsSourceSyncBehavior"/>.
    /// 
    /// Refs
    /// http://avalondock.codeplex.com/discussions/390255
    /// </summary>
    public class RegionAdapterDockingManager : RegionAdapterBase<DockingManager> {

        public RegionAdapterDockingManager(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory) {

        }

        /// <summary>
        /// Multiple views may be active at the same time in this region
        /// thus returns an instance of Region to let the Prims framework
        /// know.
        /// </summary>
        /// <returns>an instance of Region</returns>
        protected override IRegion CreateRegion() => new Region();

        protected override void Adapt(
            IRegion region,
            DockingManager regionTarget) { }

        protected override void AttachBehaviors(
            IRegion region,
            DockingManager regionTarget) {

            region.Validate(nameof(region)).NotNull();

            // Add the behavior that syncs the items source items with the rest of the items.
            region.Behaviors.Add(
                DockingManagerDocumentsSourceSyncBehavior.BehaviorKey,
                new DockingManagerDocumentsSourceSyncBehavior() {
                    HostControl = regionTarget
                });

            base.AttachBehaviors(region, regionTarget);
        }
    }
}
