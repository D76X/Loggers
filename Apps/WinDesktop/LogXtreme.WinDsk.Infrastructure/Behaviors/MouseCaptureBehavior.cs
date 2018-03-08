using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace LogXtreme.WinDsk.Infrastructure.Behaviors {

    /// <summary>
    /// This behavior exposes a read-only attached property.
    /// 
    /// Refs
    /// https://stackoverflow.com/questions/34984093/mouse-position-with-respect-to-image-in-wpf-using-mvvm/34984467#34984467
    /// On UIElement.MouseCapture and UIElement.ReleaseMouseCapture.
    /// https://stackoverflow.com/questions/12148158/to-use-or-not-to-use-releasemousecapture
    /// https://msdn.microsoft.com/en-us/library/system.windows.uielement.capturemouse(v=vs.110).aspx
    /// https://msdn.microsoft.com/en-us/library/system.windows.uielement.releasemousecapture(v=vs.110).aspx
    /// </summary>
    public class MouseCaptureBehavior : 
        Behavior<FrameworkElement>, 
        IMouseCaptureLogic {

        public static void SetMouseCaptureProxy(
        DependencyObject source,
        IMouseCapture
        value) {

            source.SetValue(MouseCaptureProxyProperty, value);
        }

        public static IMouseCapture GetMouseCaptureProxy(DependencyObject source) {
            return (IMouseCapture)source.GetValue(MouseCaptureProxyProperty);
        }

        public static readonly DependencyProperty MouseCaptureProxyProperty = DependencyProperty.RegisterAttached(
        "MouseCaptureProxy",
        typeof(IMouseCapture),
        typeof(MouseCaptureBehavior),
        new PropertyMetadata(null, OnMouseCaptureProxyChanged));

        private static void OnMouseCaptureProxyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e) {

            if (e.OldValue is IMouseCapture) {

                (e.OldValue as IMouseCapture).CaptureMouse -= OnCapture;
                (e.OldValue as IMouseCapture).ReleaseMouseCapture -= OnRelease;
                (e.OldValue as IMouseCapture).MouseCaptureLogic = null;
            }

            if (e.NewValue is IMouseCapture) {
                (e.NewValue as IMouseCapture).CaptureMouse += OnCapture;
                (e.NewValue as IMouseCapture).ReleaseMouseCapture += OnRelease;

                // take a refererence to the DO which is the behavior itself.
                (e.NewValue as IMouseCapture).MouseCaptureLogic = d as IMouseCaptureLogic;
            }
        }

        protected override void OnAttached() {

            base.OnAttached();            
            this.AssociatedObject.PreviewMouseDown += OnMouseDown;
            this.AssociatedObject.PreviewMouseMove += OnMouseMove;
            this.AssociatedObject.PreviewMouseUp += OnMouseUp;
        }        

        protected override void OnDetaching() {

            base.OnDetaching();
            this.AssociatedObject.PreviewMouseDown -= OnMouseDown;
            this.AssociatedObject.PreviewMouseMove -= OnMouseMove;
            this.AssociatedObject.PreviewMouseUp -= OnMouseUp;

            var mouseCapture = GetMouseCaptureProxy(this);
            mouseCapture.MouseCaptureLogic = null;
        }

        /// <summary>
        /// Refs
        /// https://msdn.microsoft.com/en-us/library/system.windows.uielement.capturemouse(v=vs.110).aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnCapture(object sender, EventArgs e) {

            var behavior = sender as MouseCaptureBehavior;

            if (behavior == null) { return; }

            // When an object captures the mouse, all mouse related events are treated 
            // as if the object with mouse capture performs the event, even if the mouse 
            // pointer is over another object. In this case it is relevant because this 
            // behavior implies that its AssociateObject is the UIElement that captures
            // the mouse events thus it is important that this behavior is applied to 
            // the right UIElement in the visual tree (in the XAML).
            behavior.AssociatedObject.CaptureMouse();           
        }

        /// <summary>
        /// Refs
        /// https://msdn.microsoft.com/en-us/library/system.windows.uielement.releasemousecapture(v=vs.110).aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnRelease(object sender, EventArgs e) {

            var behavior = sender as MouseCaptureBehavior;

            if (behavior == null) { return; }
            
            behavior.AssociatedObject.ReleaseMouseCapture();
        }        

        private void OnMouseDown(object sender, MouseButtonEventArgs e) {

            var mouseCapture = GetMouseCaptureProxy(this);

            if (mouseCapture == null) { return; }

            var mouseCaptureEventArgs = new MouseCaptureEventArgs(
                    relativeTo: this.AssociatedObject,
                    e: e);            
            
            mouseCapture.OnMouseDown(
                this,
                mouseCaptureEventArgs);

            // It is essential stop the routed event from being handled from any 
            // other control up in the visual tree. Some UIElements such as Buttons
            // capture the mouse in their logic thus if the OnMouseUp routed is not
            // marked as handled the visual to which this behavior is attached 
            // may lose the mosue capture to another visual as the event bubbles.
            // By marking the event as handled it still bubbles up the visual tree
            // but it is ignored by it and the mause capture remains with the control
            // that holds it last.            
            e.Handled = true;
        }

        private void OnMouseMove(object sender, MouseEventArgs e) {

            var mouseCapture = GetMouseCaptureProxy(this);

            if (mouseCapture == null) { return; }

            var mouseCaptureEventArgs = new MouseCaptureEventArgs(
                    relativeTo: this.AssociatedObject,
                    e: e);

            mouseCapture.OnMouseMove(
                this,
                mouseCaptureEventArgs);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e) {

            var mouseCapture = GetMouseCaptureProxy(this);

            if (mouseCapture == null) { return; }

            var mouseCaptureEventArgs = new MouseCaptureEventArgs(
                    relativeTo: this.AssociatedObject,
                    e: e);

            mouseCapture.OnMouseUp(
                this,
                mouseCaptureEventArgs);
        }
    }
}
