using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure.Behaviours {

    /// <summary>
    /// Attached Behaviours that can be used on a view to add logic in the 
    /// view model to be executed when the view OnLoaded event is fired.
    /// Typical application is in view models in which we not wish to load
    /// data in the constructor of the view model but rather delay data 
    /// loading in the view model to the point where the view has completed
    /// loading that is when its OnLoaded event fires. There is a good 
    /// technical reason to use this pattern as it is not possible to use 
    /// async cide in a constructor thus if the data loading is done in the 
    /// constructor a view model the execution will have to be synchronous
    /// and of course this cannot be all right when the amount fo data to 
    /// load is large.
    /// 
    /// This behaviour is based on a simple attached property that must be 
    /// used on the view that essentially is any descendant of FrameworkElement. 
    /// The AP on the view is set to a string value that is the name of a method
    /// on the data context of the view (normally its view model) that must be 
    /// invoked when the view's OnLoaded event is fired. 
    /// 
    /// The logic that hooks up the view's OnLoaded event and the invokation of 
    /// the specified view model's method is given in the PropertyChangedCallack 
    /// of this AP. 
    /// 
    /// It is indeed the fact that a PropertyChangedCallack is specified for this
    /// AP that makes it into an attached behaviour. 
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

        /// <summary>
        /// Using a DependencyProperty as the backing store for LoadedMethodName.  
        /// This enables animation, styling, binding, etc...
        /// To turn any AP into a Behaviour we need to use specify a change hanlder
        /// in the AP's metadata.
        /// </summary>
        public static readonly DependencyProperty LoadedMethodNameProperty =
            DependencyProperty.RegisterAttached(
                "LoadedMethodName", 
                typeof(string), 
                typeof(HanldeViewLoadedEventBehaviour), 
                new PropertyMetadata(null, OnLoadedMethodNameChanged));

        /// <summary>
        /// This changed handler runs when the XAML parser evaluate the attached 
        /// property. When this Changed hanlder executes this behaviour tries 
        /// to hook up the view's OnLoaded event to some named method on the view
        /// mmodel for the view. For example, the named method of the view model
        /// specified as value of the attached property may be executed to load 
        /// data only when the view to display it has already been loaded instead 
        /// of in the constructor of the view model
        /// event.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnLoadedMethodNameChanged(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e) {

            // d is the object on which the attached property was set.
            // A view ought to be any descendant of FrameworkElement
            FrameworkElement element = d as FrameworkElement;

            if (element == null) { return; }

            // any FrameworkElement has a Loaded event to hook up to
            element.Loaded += (s, eargs) => {

                // the DC of the view is its view model
                var viewModel = element.DataContext;

                if (viewModel == null) { return; }

                // the value of the EventArgs of the routed event is the string
                // that specifies the name of the method on the VM to invoke when
                // the view OnLoaded vent fires.
                var vmMethodToHookUp = e.NewValue.ToString();
                var methodInfo = viewModel.GetType().GetMethod(vmMethodToHookUp);

                if (methodInfo == null) { return; }

                methodInfo.Invoke(viewModel, null);
            };
        }
    }
}
