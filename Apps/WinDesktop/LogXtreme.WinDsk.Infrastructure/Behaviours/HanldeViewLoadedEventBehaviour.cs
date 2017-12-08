using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure.Behaviours {

    /// <summary>
    /// Attached Behaviours that can be used on a view to add logic in the 
    /// view model to be executed when the view OnLoaded event is fired.
    /// Typical application is in view models in which we wish not to load
    /// all data on construction of teh view model but rather delay data 
    /// loading in the view model to teh point where the view has completed
    /// loading.
    /// 
    /// This behaviour is based on a simple attached property that must be 
    /// used on the view. The AP is defined with a PropertyChangeCallBack
    /// which is what makes a simple AP into a behaviour. 
    /// 
    /// Refs
    /// https://app.pluralsight.com/player?course=wpf-mvvm-in-depth&author=brian-noyes&name=wpf-mvvm-in-depth-m4&clip=4&mode=live
    /// </summary>
    public class HanldeViewLoadedEventBehaviour {

        public static string GetLoadedMethodName(DependencyObject obj) {
            return (string)obj.GetValue(LoadedMethodNameProperty);
        }

        public static void SetLoadedMethodName(DependencyObject obj, string value) {
            obj.SetValue(LoadedMethodNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for LoadedMethodName.  
        // This enables animation, styling, binding, etc...
        // To turn any AP into a Behaviour we need to use specify a change hanlder
        // in the AP's metadata.
        public static readonly DependencyProperty LoadedMethodNameProperty =
            DependencyProperty.RegisterAttached(
                "LoadedMethodName", 
                typeof(string), 
                typeof(HanldeViewLoadedEventBehaviour), 
                new PropertyMetadata(null, OnLoadedMethodNameChanged));



        private static void OnLoadedMethodNameChanged(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e) {

            throw new NotImplementedException();
        }
    }
}
