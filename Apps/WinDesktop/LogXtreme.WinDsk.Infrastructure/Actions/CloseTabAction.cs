using LogXtreme.WinDsk.Infrastructure.Utils;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace LogXtreme.WinDsk.Infrastructure.Actions {

    /// <summary>
    /// This is an implementation of TriggerAction<T> that can be used with an T=Button.
    /// 
    /// Some UI declares a button in its XAML code and uses the i:Interaction.Triggers
    /// element within the button to declare a i:EventTrigger EventName="Click" and
    /// within it a declaration to the CloseTabAction.
    /// 
    /// Example
    /// 
    // xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
    // xmlns:actions="clr-namespace:LogXtreme.WinDsk.Infrastructure.Actions;assembly=LogXtreme.WinDsk.Infrastructure"
    //
    // <Button Content="X"
    //        Grid.Column="1">
    //    <i:Interaction.Triggers>
    //        <i:EventTrigger EventName = "Click" >
    //            < actions:CloseTabAction />
    //        </i:EventTrigger>
    //    </i:Interaction.Triggers>
    //</Button>
    ///
    /// </summary>
    public class CloseTabAction : TriggerAction<Button> {

        /// <summary>
        /// Finds the TabControl and the TabItem of the DO that has invoked this action by using 
        /// the RoutedEventArgs parameter and implements the logic to remove the view from the 
        /// region defined within the TabItem of the TabControl.
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

            if (region.Views.Contains(tabItem.Content)) {
                region.Remove(tabItem.Content);
            }
        }        
    }
}
