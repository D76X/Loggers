using Prism.Regions;
using System.Windows.Controls;
using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure.Prism {

    /// <summary>    /// 
    /// A RegionAdapter<T> sits between the Region and the Host Control of type T.
    /// When Prism injects a view into or removes a view from a Region a RegionAdapter 
    /// provides the logic for the underlying control of type T for the Region. 
    /// 
    /// Refs
    /// https://app.pluralsight.com/player?course=prism-introduction&author=brian-lagunas&name=prism1-03-regions&clip=5&mode=live
    /// </summary>
    public class RegionAdapterStackPanel : RegionAdapterBase<StackPanel> {

        public RegionAdapterStackPanel(IRegionBehaviorFactory regionBehaviorFactory) 
            : base(regionBehaviorFactory) {

        }

        /// <summary> 
        /// Handles the logic that adds to or remove from the underlying host control a 
        /// FrameworkElement instance that is the view that is injected into the region.     
        /// </summary>
        /// <param name="region"></param>
        /// <param name="regionTarget"></param>
        protected override void Adapt(
            IRegion region, 
            StackPanel regionTarget) {

            region.Views.CollectionChanged += (s, e) => {

                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {

                    foreach (FrameworkElement element in e.NewItems) {

                        // the RegionAdapter adds the elements to the StackPanel Host Control when a View is 
                        // added to the corresponding region 
                        regionTarget.Children.Add(element);
                    }

                } else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {

                    // TODO: should handle the remove

                    // when a view is removed from the region the region adapter for the host control
                    // that is a StackPanel must remove it from the Children collection and perform any
                    // housekeeping.
                }
            };
        }

        /// <summary>
        /// RegionAdapterBase<T>.CreateRegion returns an IRegion
        /// 
        /// There exist 3 possible types of IRegion implementations that can be returned to the caller,
        /// which one dependens on the type of Region as explained below.
        ///         
        /// -1 SingleActiveRegion:	
        /// 
        /// Allows none or one ACTIVE view at any one time i.e. this implemenation of IRegion is returned  
        /// when ContentControl is the type T.
        ///                
        /// -2 AllActiveRegion 
        /// 
        /// All the views are ACTIVE and DEACTIVATION of views is NOT ALLOWED.
        /// This implemenation of IRegion is returned  when ItemsControl is the Type T.
        ///               
        /// -3 Region 
        /// 
        /// Allows multiple active views. This s returned when SelectorControl is the Type T.
        /// 
        /// </summary>
        /// <returns>IRegion</returns>
        protected override IRegion CreateRegion() {

            // for a StackPanel which might hold multiple items all of which need to be 
            // active at the same time
            return new AllActiveRegion();
        }
    }
}
