using LogXtreme.WinDsk.Infrastructure.Utils;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace LogXtreme.WinDsk.Infrastructure.TriggerActions {

    /// <summary>
    /// This is an implementation of <see cref="TriggerAction"/> that can be used with 
    /// a type T such as Button.
    /// 
    /// Some UI declares a button in its XAML code and uses the i:Interaction.Triggers
    /// element within the button to declare a i:EventTrigger EventName="Click" and
    /// within it a declaration to the CloseTabAction.
    /// 
    /// This action is used when in a Prism application a TabControl is set to be a prism
    /// Region by marking the TabControl with a value for the attached property 
    /// RegionManager.RegionName. This turn a TabControl into a prism Region. 
    /// 
    /// It is important to understand tha <see cref="TabControl"/> is a descendant of 
    /// <see cref="Selector"/> and Prism already provides a Region Adapter for the 
    /// <see cref="Selector"/> out of the box <see cref="SelectorRegionAdapter"/>.
    /// This means that you can set the AP RegionManager.RegionName on a <see cref="TabControl"/>
    /// in XAML and expect it to have a region adapter already. You do not need to write 
    /// your own ** RegionAdapterBase<TabControl> **. 
    /// 
    /// However, the <see cref="TabControl"/> as a <see cref="Selector"/> does not have 
    /// natively a mechanism for the removal of items from the selector. It is not a problem
    /// to add a custom style like below that changes with a HeaderTemplate that shows a 
    /// x (close) button on the tab and a trigger to point to the action.
    /// 
    /// <Style TargetType="TabItem">
    ////    <Setter Property = "Header"
    ////            Value="{Binding DataContext.Title}" />
    ////    <Setter Property = "HeaderTemplate" >
    ////        < Setter.Value >
    ////            < DataTemplate >
    ////                < Grid >
    ////                    < Grid.ColumnDefinitions >
    ////                        < ColumnDefinition />
    ////                        < ColumnDefinition />
    ////                    </ Grid.ColumnDefinitions >
    ////                    < ContentControl Content="{Binding}"
    ////                                    Grid.Column="0"
    ////                                    VerticalAlignment="Center"
    ////                                    HorizontalAlignment="Center"
    ////                                    Margin="0,0,7,7">
    ////                    </ContentControl>
    ////                    <Button Content = "X"
    ////                            Grid.Column="1">
    ////                        <i:Interaction.Triggers>
    ////                            <i:EventTrigger EventName = "Click" >
    ////                                < triggeractions:CloseTabAction />
    ////                            </i:EventTrigger>
    ////                        </i:Interaction.Triggers>
    ////                    </Button>
    ////                </Grid>
    ////            </DataTemplate>
    ////        </Setter.Value>
    ////    </Setter>
    ////</Style>
    ///
    /// The fact is that default <see cref="SelectorRegionAdapter"/> does not have logic to hanlde
    /// the removal of the views in the TabItems of the TabControl. This must therefore be hanlded
    /// in some other way and <see cref="CloseTabAction"/> does exaclty that.
    /// 
    /// Some of the logic for the removal of <see cref="TabItem"/> from the region in this class is
    /// repeated in <see cref="LogXtreme.WinDsk.Infrastructure.Prism.RegionAdapterLayoutDocumentPane"/>.
    /// 
    /// Refs
    /// 
    /// TabControl
    /// https://msdn.microsoft.com/en-us/library/system.windows.controls.tabcontrol(v=vs.110).aspx
    /// 
    /// Prism out-of-the-box region adapters
    /// https://stackoverflow.com/questions/1404299/available-prism-region-controls
    /// 
    /// In particular Closing Tab Items >> Problem/Solution/Demo for Removing Views from a TabControl.
    /// https://app.pluralsight.com/player?course=prism-mastering-tabcontrol&author=brian-lagunas&name=prism-mastering-tabcontrol-m3&clip=12&mode=live
    /// 
    /// Example of how to use this action.
    /// 
    // xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
    // xmlns:actions="clr-namespace:LogXtreme.WinDsk.Infrastructure.Actions;assembly=LogXtreme.WinDsk.Infrastructure"
    ///
    /// <Button Content="X"
    ///        Grid.Column="1">
    ///    <i:Interaction.Triggers>
    ///        <i:EventTrigger EventName = "Click" >
    ///            < actions:CloseTabAction />
    ///        </i:EventTrigger>
    ///    </i:Interaction.Triggers>
    /// </Button>
    ///
    /// </summary>
    public class CloseTabAction : TriggerAction<Button> {

        /// <summary>
        /// Finds the <see cref="TabControl"/> and the <see cref="TabItem"/> of the DO that has 
        /// invoked this action by using the <see cref="RoutedEventArgs"/> parameter and implements 
        /// the logic to remove the view from the region defined within the <see cref="TabItem"/> 
        /// of the <see cref="TabControl"/>.
        /// We want not only that the <see cref="TabItem"/> is removed from the <see cref="TabControl"/>
        /// we also must make sure that the view in the same instance of the <see cref="TabItem"/> is 
        /// removed from teh region set on the containing <see cref="TabControl"/>. Only then the 
        /// view in the <see cref="TabItem"/> si no longer referenced and can actually be GCed. 
        /// </summary>
        /// <param name="parameter">An instance of RoutedEventArgs holding the invoking DO</param>
        protected override void Invoke(object parameter) {

            var args = parameter as RoutedEventArgs;

            if (args == null) {
                return;
            }

            var tabItem = SearchVisualTree.FindParent<TabItem>(args.OriginalSource as DependencyObject);

            if (tabItem == null) {
                return;
            }

            var tabControl = SearchVisualTree.FindParent<TabControl>(tabItem);

            if (tabControl == null) {
                return;
            }

            IRegion region = RegionManager.GetObservableRegion(tabControl).Value;

            if (region == null) {
                return;
            }

            this.RemoveItemFromRegion(tabItem.Content, region);
        }

        private void RemoveItemFromRegion(object item, IRegion region) {

            // a NavigationContext is provided with the NavigationService of the region
            // from which the item is to be removed the Uri does not matter.
            var navigationContext = new NavigationContext(region.NavigationService, null); 

            if (this.CanRemove(item, navigationContext)) {

                this.InvokeOnNavigatedFrom(item, navigationContext);
                region.Remove(item);
            }
        }
        
        private bool CanRemove(object item, NavigationContext navigationContext) {

            bool canRemove = true;

            // the IConfirmNavigationRequest may be implemented on either the View or its 
            // ViewModel thus both are checked.

            var confirmRequestItem = item as IConfirmNavigationRequest;

            if (confirmRequestItem != null) {

                confirmRequestItem.ConfirmNavigationRequest(navigationContext, result => {
                    canRemove = result;
                });
            }

            var frameworkElement = item as FrameworkElement;

            if (frameworkElement != null && canRemove) {

                IConfirmNavigationRequest confirmRequestDataContext = 
                    frameworkElement.DataContext as IConfirmNavigationRequest;

                if (confirmRequestDataContext != null) {

                    confirmRequestDataContext.ConfirmNavigationRequest(navigationContext, result => {
                        canRemove = result;
                    });
                }
            }

            return canRemove;
        }

        private void InvokeOnNavigatedFrom(object item, NavigationContext navigationContext) {

            var navigationAware = item as INavigationAware;

            if (navigationAware != null) {
                navigationAware.OnNavigatedFrom(navigationContext);
            }

            var frameworkElement = item as FrameworkElement;

            if (frameworkElement != null) {

                var navigationAwareDataContext = frameworkElement.DataContext as INavigationAware;

                if (navigationAwareDataContext != null) {
                    navigationAwareDataContext.OnNavigatedFrom(navigationContext);
                }
            }
        }
    }
}
