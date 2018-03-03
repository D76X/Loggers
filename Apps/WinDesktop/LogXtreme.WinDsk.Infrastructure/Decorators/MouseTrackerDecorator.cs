using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LogXtreme.WinDsk.Infrastructure.Decorators
{

    public class MouseTrackerDecorator : Decorator
    {
        static readonly DependencyProperty MousePositionProperty;
        static MouseTrackerDecorator()
        {
            MousePositionProperty = DependencyProperty.Register("MousePosition", typeof(Point), typeof(MouseTrackerDecorator));
        }

        public override UIElement Child {
            get {
                return base.Child;
            }
            set {
                if (base.Child != null)
                    base.Child.MouseMove -= _controlledObject_MouseMove;
                base.Child = value;
                base.Child.MouseMove += _controlledObject_MouseMove;
            }
        }

        public Point MousePosition {
            get {
                return (Point)GetValue(MouseTrackerDecorator.MousePositionProperty);
            }
            set {
                SetValue(MouseTrackerDecorator.MousePositionProperty, value);
            }
        }

        void _controlledObject_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(base.Child);

            // Here you can add some validation logic
            MousePosition = p;
        }
    }

    //public class MouseTrackerDecorator : Decorator { }

    /// <summary>
    /// 
    /// A decorator to capture the mouse position within its child element.
    /// It defines a readonly attached propety MousePosition that tracks the
    /// coordinate of the mouse position on the child element by handling its 
    /// MouseMove event. This implementation based on a Decorator base class 
    /// rather than a pure attached property the child of teh decorator is the
    /// target of the attached property. This implementation has the advantage 
    /// that the decorator is the element in the visual tree that immediately 
    /// contain the target and has the same size as its target thus the 
    /// coordinate of the point for MousePosition are with respect the ULH 
    /// corner.
    /// 
    /// Refs
    /// https://stackoverflow.com/questions/6714663/wpf-how-do-i-bind-a-controls-position-to-the-current-mouse-position
    /// </summary>
    //public class MouseTrackerDecorator : Decorator
    //{
    //    public Point MousePosition {
    //        get => (Point)GetValue(MousePositionProperty);
    //        private set => SetValue(MousePositionPropertyKey, value);
    //    }

    //    public static readonly DependencyProperty MousePositionProperty =
    //        MousePositionPropertyKey.DependencyProperty;

    //    private static readonly DependencyPropertyKey MousePositionPropertyKey =
    //        DependencyProperty.RegisterAttachedReadOnly(
    //            @"MousePosition",
    //            typeof(Point),
    //            typeof(MouseTrackerDecorator),
    //            new FrameworkPropertyMetadata(new Point(0, 0)));

    //    public override UIElement Child {

    //        get => base.Child;

    //        set {

    //            if (base.Child != null)
    //            {
    //                base.Child.MouseMove -= OnMouseMove;
    //            }

    //            base.Child = value;
    //            base.Child.MouseMove += OnMouseMove;
    //        }
    //    }

    //    private void OnMouseMove(object sender, MouseEventArgs e)
    //    {

    //        Point p = e.GetPosition(base.Child);

    //        // Here you can add some validation logic
    //        MousePosition = p;
    //    }
    //}
}