using LogXtreme.Infrastructure.ContractValidators;
using Prism.Regions;
using System;
using System.Collections.Specialized;
using System.Linq;
using Xceed.Wpf.AvalonDock;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    public class RegionAdapterDockingManager : RegionAdapterBase<DockingManager> {

        public RegionAdapterDockingManager(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory) {

        }

        /// <summary>
        /// Multiple views may be active at the same time in the region.
        /// This must be a custom region.
        /// </summary>
        /// <returns></returns>
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
