using LogXtreme.Ifrastructure.Enums;
using LogXtreme.Infrastructure.ContractValidators;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Services;
using Prism.Regions;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;


namespace LogXtreme.WinDsk.Infrastructure.Prism {    

    public class RegionAdapterLayoutAnchorablePane : RegionAdapterBase<LayoutAnchorablePane> {

        private IAvalonDockService avalonDockService;

        public RegionAdapterLayoutAnchorablePane(
            IRegionBehaviorFactory regionBehaviorFactory,
            IAvalonDockService avalonDockService)
            : base(regionBehaviorFactory) {

            this.avalonDockService = avalonDockService;
        }

        protected override void Adapt(
            IRegion region,
            LayoutAnchorablePane regionTarget) {

            //!
        }

        /// <summary>
        /// For a prism region set on an instance of <see cref="LayoutDocumentPane"/>
        /// there might be multiple active documents (tabs) at any one time.
        /// </summary>
        /// <returns><see cref="IRegion"/></returns>
        protected override IRegion CreateRegion() =>
            new SingleActiveRegion();
    }
}
