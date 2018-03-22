using LogXtreme.Infrastructure.ContractValidators;
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

        public RegionAdapterLayoutDocumentPane(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory) { }

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

            // monitor when views are injectedinto or removed from the region. 
            region.Views.CollectionChanged += (s, e) => {

                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {

                    // new views have been injeted into this region.
                    foreach (var item in e.NewItems) {

                        UIElement view = item as UIElement;

                        LayoutDocument document = new LayoutDocument();
                        document.Content = item;

                        // apply some metadata!
                        document.Title = view.GetType().ToString();

                        document.Closed += (sender, args) => {

                            // from CloseTabAction.cs

                            RemoveItemFromRegion(sender, region);
                        };

                        regionTarget.Children.Add(document);
                        document.IsActive = true;
                    }

                }
                else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {

                    var x = e.NewItems;
                    var y = e.OldItems;
                    // ? what to do here ?
                }
            };
        }

        private void RemoveItemFromRegion(object item, IRegion region) {

            // some more navigation logic here...
            region.Remove(item);
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
