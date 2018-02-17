using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace LogXtreme.WinDsk.Infrastructure.Behaviors {

    /// <summary>
    /// A Blend behavior to force the focus on a specific focusable control
    /// in the visual tree.
    /// </summary>
    public class KeyboardFocusBehavior : Behavior<FrameworkElement> {

        protected override void OnAttached() {

            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;            
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {

            var target = sender as IInputElement;

            if (target == null ) {
                return;
            }

            Keyboard.Focus(target);
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }        
    }
}
