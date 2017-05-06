using System.Collections.Specialized;
using Prism.Regions;
using System.Windows;
using System;

namespace LogXtreme.WinDsk.Infrastructure {

    /// <summary>
    /// 
    /// This class is a custom region behaviour and must be registered with the Bootstrapper 
    /// to be invoked in the life cycle of teh Prism application.
    /// 
    /// This behavior looks at the changes of Active Views of a region and when the view or
    /// its view model implement IAwareBehavior it uses ... to set the IAwareBehavior.RegionManager
    /// property value to...
    /// 
    /// </summary>
    public class RegionManagerAwareBehavior : RegionBehavior {

        /// <summary>
        /// A unique key on a RegionBehavior is necessary in order to register the 
        /// behavior with the Bootstrapper.
        /// </summary>
        public const string BehaviorKey = nameof(RegionManagerAwareBehavior);

        /// <summary>
        /// OnAttach is called when the Behavior is attached to its region adapter.
        /// In OnAttach it is possible to get references to the Region or the RegionAdapter
        /// and register handlers for the events on the Region or the RegionAdapter.
        /// </summary>
        protected override void OnAttach() {

            Region.ActiveViews.CollectionChanged += ActiveViewsCollectionChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {

            if (e.Action == NotifyCollectionChangedAction.Add) {

                foreach (var item in e.NewItems) {

                    // get the global region manager 
                    IRegionManager regionManager = Region.RegionManager;

                    // is this is a view?
                    // Normally it will be a view.
                    FrameworkElement element = item as FrameworkElement;

                    if (element != null) {

                        // the view might have its attached property value set its scoped region manager 
                        IRegionManager scopedRegionManager = element.GetValue(RegionManager.RegionManagerProperty) as IRegionManager;

                        if (scopedRegionManager != null) {
                            regionManager = scopedRegionManager;
                        }

                        // properly set the IRegionManagerAware.RegionManager property on view and/or ViewModel 
                        InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = regionManager);
                    }
                }

            } else if (e.Action == NotifyCollectionChangedAction.Remove) {

                foreach (var item in e.OldItems) {

                    // properly clear IRegionManagerAware.RegionManager property on view and/or ViewModel 
                    InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = null);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">aon object that might be a view implementing IRegionManagerAware</param>
        /// <param name="invocation">The action to perform on IRegionManagerAware</param>
        static void InvokeOnRegionManagerAwareElement(object item, Action<IRegionManagerAware> invocation) {
           
            var rmAwareItem = item as IRegionManagerAware;
            if (rmAwareItem != null) {
                invocation(rmAwareItem);
            }

            // now we need to figure out whether it is a view and it has a view model that also implements IRegionManagerAware
            var frameworkElement = item as FrameworkElement;

            if (frameworkElement != null) {

                // it is a view - does its view model implement IRegionManagerAware?
                IRegionManagerAware rmAwareDataContext = frameworkElement.DataContext as IRegionManagerAware;

                if (rmAwareDataContext != null) {

                    // it is a view with a ViewModel implementing IRegionManagerAware               
                    // this IRegionManagerAware also needs to be taken care of in terms
                    // of doing something to IRegionManagerAware.RegionManager.

                    // in WPF a view might not have ITS OWN DataContext in which case it inherits the ViewModel 
                    // of the parent in the visual tree we must check whether this is the case for this view
                    var frameworkElementParent = frameworkElement.Parent as FrameworkElement;

                    if (frameworkElementParent != null) {

                        var rmAwareDataContextParent = frameworkElementParent.DataContext as IRegionManagerAware;

                        if (rmAwareDataContextParent != null) {

                            if (rmAwareDataContext == rmAwareDataContextParent) {

                                // This view does not have its own view model it's using the parents'.
                                // The view inherits the DataContext from the parent view in the visual tree
                                // we do not want to perform any action on IRegionManagerAware.RegionManager
                                // of the parents.
                                return; 
                            }
                        }
                    }

                    // here we are sure that the view has its own DataContext and that it is a inplementation 
                    // of IRegionManagerAware thus we can invoke the action to either set or null the 
                    // IRegionManagerAware.RegionManager
                    invocation(rmAwareDataContext);
                }
            }
        }
    }
}
