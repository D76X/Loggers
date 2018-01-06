using System.Collections.Specialized;
using Prism.Regions;
using System.Windows;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Prism {


    /// <summary> 
    /// This is a custom Prism region behaviour and must be registered with the Bootstrapper 
    /// to be invoked in the life cycle of a Prism application.
    ///  
    /// This modified behavior solves the problem of cross-shell navigation in Prims 
    /// applications that might have multiple shels each having its own RegionManager.
    /// 
    /// This behavior looks at the changes of Active Views of a region and when a view is added 
    /// to a region the view's RegionManager is used as values of the view model's property 
    /// IRegionManagerAware.RegionManager when the view model implements IRegionManagerAware. 
    /// This should be the cases for any view model in a Prism application with multiple shells.
    /// 
    /// In this way the view model can retain a reference to the RegionManager of the corresponding
    /// view and this can be used when a navigation request is made instead of the global 
    /// RegionManager that in general is the RegionManager of the first shell created.
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
        /// In OnAttach the RegionBehavior has access to the public API of the Region or 
        /// RegionAdapter thus it is possible to subscribe to events or access properties.
        /// It is also possible to test the type of Region or RegionAdapter and perform
        /// action only of some specific type(s) so that the RegionBehavior is specific 
        /// to those Region or RegionAdapter type(s).
        /// </summary>
        protected override void OnAttach() {

            Region.ActiveViews.CollectionChanged += ActiveViewsCollectionChanged;
        }

        /// <summary>
        /// Any time a new view is added to the collection of views of a region it is tested whether
        /// the view has a value for the attached property RegionManager.RegionManagerProperty and if 
        /// this is the case a refence to this region manager is tempoarily stored and passed to the 
        /// the next call to InvokeOnRegionManagerAwareElement with an action that sets the property
        /// IRegionManagerAware.RegionManager to the scope region of  . 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveViewsCollectionChanged(
            object sender, 
            NotifyCollectionChangedEventArgs e) {

            if (e.Action == NotifyCollectionChangedAction.Add) {

                foreach (var item in e.NewItems) {

                    // A view must have a RegionAdapter in order to be a region in Prism.
                    // A RegionAdapter can hold one ore more RegionBehaviors.
                    // When a view is created it is given a RegionManager.
                    // The RegionManager of the view may be the global Prism RegionManager that is that of the first shell
                    // or a scoped region when the view is in a secondary shell created after bootstrapping.
                    // Initally we get a reference to the Region manager of the region that may be the global Prism RegionManager.
                    // Then we try to get the region manager of the view.
                    // The goal is to get the DataContext that is the VM of the view to get a reference to a scoped RegionManager
                    // when the view has one and the DataContext/VM is an implementation of IRegionManagerAware.
                    // In this way when commands on the VM to request navigation are actioned the VM can use the scoped RegionManager
                    // instead of the global RegionManager and navigation happens only in the shell of containing the view instance 
                    // that invoked the command. This solve the problem of cross-shell navigation from views. 
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
                    // this makes usre that the scoped region manager can be disposed of when no longer 
                    // referenced.
                    InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = null);
                }
            }
        }

        /// <summary>
        /// Given item it is tested whether the item is an instance of IRegionManagerAware. 
        /// This may be the case when it is chosen to have the view to implement this interface.
        /// However, more commonly item will be a FrameworkElement that is a view whose DataContext
        /// my be an implementation of IRegionManagerAware. In any case when an implementation of 
        /// IRegionManagerAware the given action is performed. It si also possible that both the 
        /// view and the corresponding VM (DataContext) amy be implementation of IRegionManagerAware
        /// is which case the given action is invoked for both.
        /// </summary>
        /// <param name="item">an object that might be a view implementing IRegionManagerAware or a view with a VM that implements IRegionManagerAware</param>
        /// <param name="invocation">The action to perform on IRegionManagerAware</param>
        static void InvokeOnRegionManagerAwareElement(
            object item, 
            Action<IRegionManagerAware> invocation) {
           
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

                    // here we are sure that the view has its own DataContext and that it is a implementation 
                    // of IRegionManagerAware thus we can invoke the action to either set or null the property
                    // IRegionManagerAware.RegionManager
                    invocation(rmAwareDataContext);
                }
            }
        }
    }
}
