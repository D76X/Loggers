using LogXtreme.Infrastructure.ContractValidators;
using LogXtreme.Reactive.Extensions;
using LogXtreme.WinDsk.Infrastructure.Utils;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure.Prism {


    /// <summary> 
    /// This is a custom Prism region behaviour and must be registered with the Bootstrapper 
    /// to be invoked in the life cycle of a Prism application.
    ///  
    /// This modified behavior solves the problem of cross-shell navigation in Prism
    /// applications that might have multiple shells each having its own RegionManager.
    /// 
    /// This behavior looks at the changes of Active Views of a region and when a view is added 
    /// to a region the view's RegionManager is used as value of the view model's property 
    /// IRegionManagerAware.RegionManager when the view model implements IRegionManagerAware. 
    /// This should be the case for any view model in a Prism application with multiple shells.
    /// 
    /// In this way the view model can retain a reference to the RegionManager of the 
    /// corresponding view and this can be used when a navigation request is made instead of the 
    /// global RegionManager that in general is the RegionManager of the first shell created
    /// when the application starts in the Bootstrapper.
    ///     
    /// </summary>
    public class RegionManagerAwareBehavior : 
        RegionBehavior,
        IDisposable {

        private List<IDisposable> eventsubscriptions_ViewLoaded = new List<IDisposable>();

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

            var regionName = Region.Name;
            var views = Region.Views;
            var count = views.ToList().Count;

            Region.ActiveViews.CollectionChanged -= ActiveViewsCollectionChanged;
            Region.ActiveViews.CollectionChanged += ActiveViewsCollectionChanged;

            // secondary shells when instantiated might already have views in 
            // their regions hence the event handlers on the region's active 
            // views might not fire. To make sure that also the views already
            // present in the regions of the secondary shells get their scoped
            // region manager set to that of the shell they are contained in 
            // a weak event hanlder is attached to their loaded event to be sure 
            // that the containing window has already been shown by then.

            foreach (var view in Region.Views) {
                FrameworkElement element = view as FrameworkElement;
                if (element == null) { return; }
                this.SubscribeToViewLoadedEvent(element);               
            }
        }

        private void OnElementLoaded(
            object sender,
            RoutedEventArgs e) {

            this.TryToSetRegionManager(sender);            
        }

        private void TryToSetRegionManager(object item) {

            IRegionManager regionManager = Region.RegionManager;

            FrameworkElement element = item as FrameworkElement;

            if (element == null) { return; }

            // the view might have already its attached property value set to its scoped region manager             
            IRegionManager scopedRegionManager = element.GetValue(RegionManager.RegionManagerProperty) as IRegionManager;

            if (scopedRegionManager != null) {
                regionManager = scopedRegionManager;
            }
            else {

                // if a scoped region manager could not be found on the view try to look it up from the containing shell.
                regionManager = PrismUtils.GetScopedRegionManager(element);
            }

            // if a RegionManager instance for this element could not yet be found subscribe to its loaded event
            // to re-attempt the process later when the view is in a shell that has its region manager set.
            if (regionManager == null) {
                this.SubscribeToViewLoadedEvent(element);
                return;
            }

            // properly set the IRegionManagerAware.RegionManager property on view and/or ViewModel 
            InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = regionManager);            
        }

        /// <summary>
        /// Subcribes weakly to the Loaded event of the view so that on this
        /// event an hanlder is run to try to set the RegionManager property
        /// if either the view or view model or both implements IRegionManagerAware.
        /// </summary>
        /// <param name="element">the element to subscribe to the Loaded event</param>
        private void SubscribeToViewLoadedEvent(FrameworkElement element) {

            element.Validate(nameof(element)).NotNull();
            
            var subscription = Observable
                    .FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                    h => element.Loaded += h,
                    h => element.Loaded -= h)
                    .SubscribeWeakly(
                    this,
                    (target, ep) => target.OnElementLoaded(ep.Sender, ep.EventArgs));

            this.eventsubscriptions_ViewLoaded.Add(subscription);
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
                    this.TryToSetRegionManager(item);                    
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove) {

                foreach (var item in e.OldItems) {

                    // properly clear IRegionManagerAware.RegionManager property on view and/or ViewModel 
                    // this makes sure that the scoped region manager can be disposed of when no longer 
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

        #region IDisposable

        private bool disposedValue = false; // To detect redundant calls         

        protected virtual void Dispose(bool disposing) {

            if (!disposedValue) {

                if (disposing) {

                    foreach (var subscription in this.eventsubscriptions_ViewLoaded) {
                        subscription?.Dispose();
                    }

                    this.eventsubscriptions_ViewLoaded = null;
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose() {
            this.Dispose(true);
            //GC.SuppressFinalize(this);
        }

        #endregion
    }
}
