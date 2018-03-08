using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Wpf;
using System;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestBehaviors.ViewModels {

    /// <summary>
    /// Refs
    /// https://msdn.microsoft.com/en-gb/magazine/dn237302.aspx
    /// </summary>
    public class Tab4ViewModel : NotifyPropertyChangedBase, IMouseCapture {

        private MouseCaptureState mouseDownPoint = new MouseCaptureState();
        private MouseCaptureState mouseMovePoint = new MouseCaptureState();
        private MouseCaptureState mouseUpPoint = new MouseCaptureState();

        public Tab4ViewModel() { }

        /// <summary>
        /// When the tab for this View Model is loaded raise the events 
        /// to set up the state of the UI as appropriate.
        /// </summary>
        public void OnViewLoadedHandler() { }

        public MouseCaptureState MouseDownPoint {
            get => this.mouseDownPoint;
            set => this.SetProperty<MouseCaptureState>(ref this.mouseDownPoint, value);
        }

        public MouseCaptureState MouseMovePoint {
            get => this.mouseMovePoint;
            set => this.SetProperty<MouseCaptureState>(ref this.mouseMovePoint, value);
        }

        public MouseCaptureState MouseUpPoint {
            get => this.mouseUpPoint;
            set => this.SetProperty<MouseCaptureState>(ref this.mouseUpPoint, value);
        }

        /// <summary>
        /// In this case the view model implements IMouseCapture and is 
        /// exposed as a proxy for the mouse capture but in general a 
        /// specialized model or sub-view-model may also be used.
        /// </summary>
        public IMouseCapture MouseCaptureProxy {
            get => this;
        }

        #region IMouseCapture

        /// <summary>
        /// Holds a reference to the object that implements the logic for 
        /// the mouse capture. In this case this is the instance of the 
        /// MouseCaptureBehavior.
        /// </summary>
        public IMouseCaptureLogic MouseCaptureLogic {
            get;
            set;
        }

        // when the behavior MouseCaptureBehavior latches to an implementation of 
        // IMouseCapture such as this view model it registers events handlers with
        // the two events IMouseCapture.Capture and IMouseCapture.Release. Raising
        // the events from the implementation of IMouseCapture causes the behavior
        // to invoke the UIElement.CaptureMouse(), UIElement.ReleaseMouseCapture() 
        // respectively.
        public event EventHandler CaptureMouse = delegate { };
        public event EventHandler ReleaseMouseCapture = delegate { };

        public void RaiseCaptureMouse(
            IMouseCaptureLogic sender,
            MouseCaptureEventArgs e) {

            this.CaptureMouse?.Invoke(
                this.MouseCaptureLogic,
                e);
        }

        public void RaiseReleaseMouseCapture(
            IMouseCaptureLogic sender,
            MouseCaptureEventArgs e) {

            this.ReleaseMouseCapture?.Invoke(
                this.MouseCaptureLogic,
                e);
        }

        /// <summary>
        /// The behavior sends OnMouseDown events to the IMouseCapture proxy
        /// which in this case is the whole View Model but in general it needs 
        /// not to be.
        /// </summary>
        /// <param name="sender">an isntace of the MouseCaptureBehavior</param>
        /// <param name="e">an instance of MouseCaptureEventArgs</param>
        public void OnMouseDown(
            object sender,
            MouseCaptureEventArgs e) {

            // you must call the capture OnMouseDown and
            // release it on MouseUp
            this.MouseCaptureProxy.RaiseCaptureMouse(
                sender as IMouseCaptureLogic,
                e);

            // do something with the mouse event args.
            this.MouseDownPoint = new MouseCaptureState(e);
        }

        /// <summary>
        /// The behavior sends OnMouseMove events to the IMouseCapture proxy
        /// which in this case is the whole View Model but in general it needs 
        /// not to be.
        /// </summary>
        /// <param name="sender">an isntace of the MouseCaptureBehavior</param>
        /// <param name="e">an instance of MouseCaptureEventArgs</param>
        public void OnMouseMove(
            object sender,
            MouseCaptureEventArgs e) {

            // do something with the mouse event args.
            this.MouseMovePoint = new MouseCaptureState(e);
        }

        /// <summary>
        /// The behavior sends OnMouseDown events to the IMouseCapture proxy
        /// which in this case is the whole View Model but in general it needs 
        /// not to be.
        /// </summary>
        /// <param name="sender">an isntace of the MouseCaptureBehavior</param>
        /// <param name="e">an instance of MouseCaptureEventArgs</param>
        public void OnMouseUp(
            object sender,
            MouseCaptureEventArgs e) {

            // you must call the capture OnMouseDown and
            // release it on MouseUp
            this.MouseCaptureProxy.RaiseReleaseMouseCapture(
                sender as IMouseCaptureLogic,
                e);

            // do something with the mouse event args..
            this.MouseUpPoint = new MouseCaptureState(e);
        }

        #endregion                
    }
}
