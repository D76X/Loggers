using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace LogXtreme.WinDsk.Infrastructure.Behaviours {

    /// <summary>
    /// This is an example of how to implement custom Blend behaviours.
    /// The generic parameter of Behaviour<T> is the base class on which
    /// the behavior is intended to be used. Adding a behaviour to an 
    /// element can be done in the visual designer either in Blend or VS
    /// by dragging and dropping the behaviour from the Assets/Behaviors
    /// pane to the element in the visual tree shown in the Document
    /// Outline pane. This metaphor of dragging and dropping a behavior
    /// from an assets section onto a elemnt of teh visual tree is one
    /// of the reasons why Blend Behaviors are sometimes preferable to 
    /// Attached Behaviors which are declared using static classes and 
    /// static method for which there's no such metaphor in the designers
    /// for Blend and VS. Finally, when a Behavior is dropped on a visual
    /// target in the Document and Outline the behaviour is added to a 
    /// collection of Behaviours so any visual element can have mutiple 
    /// behaviors.
    /// 
    /// A Behavior will 
    /// 1-declare one or more DPs with changed handlers.
    /// 2-override 
    /// Refs
    /// https://app.pluralsight.com/player?course=wpf-mvvm-in-depth&author=brian-noyes&name=wpf-mvvm-in-depth-m4&clip=8&mode=live
    /// http://briannoyesblog.azurewebsites.net/2012/12/20/attached-behaviors-vs-attached-properties-vs-blend-behaviors/
    /// </summary>
    public class ShowNotificationMessageBehavior : Behavior<ContentControl> {

        public string Message {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // A behaviour normally declare at least a DP 
        // Normally the DP is declared with a changed hanlder so that 
        // some "behavior" can be executed when the value of the DP 
        // changes. The DP can be data bound to either some property
        // on a view model or some other property of a visual element 
        // in the accessible (to the instance of the behavior) visual 
        // tree.
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(
                "Message", 
                typeof(string), 
                typeof(ShowNotificationMessageBehavior), 
                new PropertyMetadata(string.Empty, OnMessageChanged));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnMessageChanged(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e) {

            // this notification can only be triggered by an instance of 
            // this behavior
            var behavior = (ShowNotificationMessageBehavior)d;

            // Behaviour.AssociatedObject gives access to the instance of a XAML element 
            // to which the behavior is added and such instance must be a descendant of 
            // the type T of Behavior<T>. In this specific case it is T is ContentControl.
            behavior.AssociatedObject.Content = e.NewValue;
            behavior.AssociatedObject.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// In the OnAttach override the Behavir gains access to the public
        /// API of the XAML element is is applied to. The Behaviour in OnAttached
        /// can access the public API of the element and do the following
        /// 1-set properties
        /// 2-subscribe to events
        /// </summary>
        protected override void OnAttached() {

            // In this case we want to set the visibility of the instance
            // of ContentControl to which this behavior is added to Collapsed
            // when the user click on it. In other words we want the user to 
            // be able to dismiss the message box by clicking on it.
            this.AssociatedObject.MouseLeftButtonDown += (s, e) => {

                var senderControl = s as ContentControl;
                if(senderControl == null) { return; }
                senderControl.Visibility = Visibility.Collapsed;
            };
        }
    }
}
