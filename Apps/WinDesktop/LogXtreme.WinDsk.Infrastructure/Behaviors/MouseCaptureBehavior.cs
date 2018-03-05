using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace LogXtreme.WinDsk.Infrastructure.Behaviors {

    /// <summary>
    /// This behavior exposes a read-only attached property 
    /// 
    /// Refs
    /// https://stackoverflow.com/questions/34984093/mouse-position-with-respect-to-image-in-wpf-using-mvvm/34984467#34984467
    /// https://msdn.microsoft.com/en-us/library/system.windows.uielement.capturemouse(v=vs.110).aspx
    /// https://msdn.microsoft.com/en-us/library/system.windows.uielement.releasemousecapture(v=vs.110).aspx
    /// </summary>
    public class MouseCaptureBehavior : Behavior<FrameworkElement> {

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

        //public IMouseCapture MouseCapture {
        //    get => GetMouseCapture(AssociatedObject);            
        //}

        //public static IMouseCapture GetMouseCapture(DependencyObject obj) {
        //    return (IMouseCapture)obj.GetValue(MouseCaptureProperty);
        //}

        //private static void SetMouseCapture(
        //    DependencyObject obj,
        //    IMouseCapture value) {

        //    obj.SetValue(MouseCaptureProperty, value);
        //}

        //public static readonly DependencyPropertyKey MouseCapturePropertyKey =
        //    DependencyProperty.RegisterAttachedReadOnly(
        //        @"MouseCaptureProperty", 
        //        typeof(IMouseCapture), 
        //        typeof(MouseCaptureBehavior), 
        //        new PropertyMetadata(null, OnMouseCaptureChanged));

        //// The declarration of the key must come first otherwise the XAML 
        //// designer breaks as it fails to construct an instance of the 
        //// behavior.
        //public static readonly DependencyProperty MouseCaptureProperty =
        //    MouseCapturePropertyKey.DependencyProperty;

        private static void OnMouseCaptureProxyChanged(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e) {

            if (e.OldValue is IMouseCapture) {
                (e.OldValue as IMouseCapture).Capture -= OnCapture;
                (e.OldValue as IMouseCapture).Release -= OnRelease;
            }
            if (e.NewValue is IMouseCapture) {
                (e.NewValue as IMouseCapture).Capture += OnCapture;
                (e.NewValue as IMouseCapture).Release += OnRelease;
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
