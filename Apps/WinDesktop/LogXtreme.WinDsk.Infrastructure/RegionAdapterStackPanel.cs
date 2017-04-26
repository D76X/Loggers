using Prism.Regions;
using System.Windows.Controls;
using System;
using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure {

    /// <summary>
    /// 
    /// A RegionAdapter<T> sits between the Region and the Host Control of type T.
    /// When Prism injects a view into or remove a view from a Region a RegionAdapter 
    /// provides the smae logic for the underlying control of the Region. 
    /// 
    /// </summary>
    public class RegionAdapterStackPanel : RegionAdapterBase<StackPanel> {

        public RegionAdapterStackPanel(IRegionBehaviorFactory regionBehaviorFactory) 
            : base(regionBehaviorFactory) {

        }

        /// <summary>
        /// 
        /// Handles the logic that adds to or remove from the underlying host control a 
        /// FrameworkElement instance that is the view that is injected into the region.        /// 
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="regionTarget"></param>
        protected override void Adapt(IRegion region, StackPanel regionTarget) {

            region.Views.CollectionChanged += (s, e) => {

                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {

                    foreach (FrameworkElement element in e.NewItems) {

                        // the RegionAdaptr adds the element to the StackPanel when a View is added to the 
                        // corresponding region Region 
                        regionTarget.Children.Add(element);
                    }

                } else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {

                    // TODO: should handle the remove

                    // when a view is removed from the region the region adapter for the host control
                    // tha is a StackPanel must remove it from the Children collection
                }
            };
        }

        /// <summary>
        /// RegionAdapterBase<T>.CreateRegion returns an IRegion
        /// 
        /// SingleActiveRegion	=> allows none or one ACTIVE view at any one time => used for ContentControl
        /// 
        /// AllActiveRegion => all the views are ACTIVE and DEACTIVATION of views is NOT ALLOWED => used for ItemControl
        /// 
        /// Region => allows multiple active views => used for SelectorControl
        /// </summary>
        /// <returns>IRegion</returns>
        protected override IRegion CreateRegion() {

            // for a StackPanel which might hold multiple items
            return new AllActiveRegion();
        }
    }
}
