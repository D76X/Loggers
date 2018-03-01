using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LogXtreme.WinDsk.Infrastructure.Decorators {

    /// <summary>
    /// A decorator to capture the mouse position within its child elment.
    /// It defines a dependency propety
    /// Refs
    /// https://stackoverflow.com/questions/6714663/wpf-how-do-i-bind-a-controls-position-to-the-current-mouse-position
    /// </summary>
    public class MouseTrackerDecorator : Decorator {

        public Point MousePosition {
            get => (Point)GetValue(MousePositionProperty);
            private set => SetValue(MousePositionPropertyKey, value);
        }

        public static readonly DependencyProperty MousePositionProperty =
            MousePositionPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey MousePositionPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                @"MousePosition",
                typeof(Point),
                typeof(MouseTrackerDecorator),
                new FrameworkPropertyMetadata(new Point(0,0)));

        public override UIElement Child {

            get => base.Child;

            set {

                if (base.Child != null) {
                    base.Child.MouseMove -= OnMouseMove;
                }

                base.Child = value;
                base.Child.MouseMove += OnMouseMove;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e) {

            Point p = e.GetPosition(base.Child);

            // Here you can add some validation logic
            MousePositionPropertyKey = p;
        }
    }
}
