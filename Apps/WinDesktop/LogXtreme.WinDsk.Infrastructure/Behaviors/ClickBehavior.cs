using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace LogXtreme.WinDsk.Infrastructure.Behaviors {

    /// <summary>
    /// A blend behavior for any UIElement to provides the same behavior as the common
    /// click of a control of base type Button. It exposes a DP of type ICommand to 
    /// bind a command on a VM to be invoked when the UIElement instance raises the
    /// OnMouseDown event.
    /// 
    /// Refs
    /// https://serialseb.com/blog/2007/01/25/attached-events-by-example-adding/
    /// </summary>
    public class ClickBehavior : Behavior<UIElement> {


        /// <summary>
        /// Command should be bound to a property of type ICommand exposed
        /// by the VM.
        /// </summary>
        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(ClickBehavior));

        /// <summary>
        /// Use Behavior<T>.OnAttached to subscribe to events, etc.
        /// </summary>
        protected override void OnAttached() {

            base.OnAttached();
            this.AssociatedObject.MouseDown += this.MouseDown;
        }

        /// <summary>
        /// Use Behavior<T>.OnAttached to clean up i.e. detach event handlers. 
        /// </summary>
        protected override void OnDetaching() {

            base.OnDetaching();
            this.AssociatedObject.MouseDown -= this.MouseDown;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDown(
            object sender,
            MouseButtonEventArgs e) {
            
            var cmd = this.Command;

            // need DP for payload
            if (cmd != null && cmd.CanExecute(null)) {
                this.Command.Execute(null);
            }

            // raise the attached event Button.ClickEvent as if
            // the target of the behavior were a Button. This 
            // attached event can then be handled by some other
            // UIElement in the cisual tree if so chosen.
            (sender as UIElement)?
            .RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
