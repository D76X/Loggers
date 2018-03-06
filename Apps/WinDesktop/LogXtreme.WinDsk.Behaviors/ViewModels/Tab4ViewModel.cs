using LogXtreme.WinDsk.Infrastructure.Commands;
using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Wpf;
using System;
using System.Windows;
using System.Windows.Input;

namespace LogXtreme.WinDsk.TestBehaviors.ViewModels {

    /// <summary>
    /// Refs
    /// https://msdn.microsoft.com/en-gb/magazine/dn237302.aspx
    /// </summary>
    public class Tab4ViewModel : NotifyPropertyChangedBase, IMouseCapture {

        private string message = @"No mouse capture yet";       
        
        private RelayCommand<object> startCaptureCommand;
        private RelayCommand<object> stopCaptureCommand;
        private bool startCaptureEnabled;
        private Point mouseDownPoint = new Point();
        private Point mouseMovePoint = new Point();
        private Point mouseUpPoint = new Point();

        public Tab4ViewModel() {

            this.startCaptureCommand = new RelayCommand<object>(
            this.ExecuteStartCapureCommand,
            this.CanExecuteStartCaptureCommand);

            this.stopCaptureCommand = new RelayCommand<object>(
            this.ExecuteStopCapureCommand,
            this.CanExecuteStopCaptureCommand);
        }

        public string Message {
            get => this.message;
            set => this.SetProperty<string>(ref this.message, value);
        }

        public Point MouseDownPoint {
            get => this.mouseDownPoint;
            set => this.SetProperty<Point>(ref this.mouseDownPoint, value);
        }

        public Point MouseMovePoint {
            get => this.mouseMovePoint;
            set => this.SetProperty<Point>(ref this.mouseMovePoint, value);
        }

        public Point MouseUpPoint {
            get => this.mouseUpPoint;
            set => this.SetProperty<Point>(ref this.mouseUpPoint, value);
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

        // when the behavior MouseCaptureBehavior latches to an implementation of 
        // IMouseCapture such as this view model it registers events handlers with
        // the two events IMouseCapture.Capture and IMouseCapture.Release. Raising
        // the events from the implementation of IMouseCapture causes the behavior
        // to invoke the UIElement.CaptureMouse(), UIElement.ReleaseMouseCapture() 
        // respectively.
        public event EventHandler CaptureMouse = delegate { };
        public event EventHandler ReleaseMouseCapture = delegate { };

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

            this.MouseDownPoint = new Point(e.X, e.Y);
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

            this.MouseMovePoint = new Point(e.X, e.Y);
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

            this.MouseUpPoint = new Point(e.X, e.Y);
        }
        #endregion

        public bool StartCaptureEnabled {

            get { return this.startCaptureEnabled; }

            set {

                this.SetProperty<bool>(ref this.startCaptureEnabled, value);
                this.startCaptureCommand.RaiseCanExecuteChanged();

                if (value) {
                    this.ResetMouseCapturePoints();
                }
            }
        }

        public ICommand StartCaptureCommand =>
            this.startCaptureCommand;

        private void ExecuteStartCapureCommand(object param) {

            // When an object captures the mouse, all mouse related events are treated 
            // as if the object with mouse capture perform the event, even if the mouse 
            // pointer is over another object. In this case it is relevant because this 
            // behavior implies that its AssociateObject is the UIElement that captures
            // the mouse events thus it is important that this behavior is applied to 
            // the right UIElement in the visual tree (in the XAML).
            this.CaptureMouse?.Invoke(this, EventArgs.Empty);

            this.StartCaptureEnabled = false;
        }

        private bool CanExecuteStartCaptureCommand(object param) => 
            this.startCaptureEnabled;

        public ICommand StopCaptureCommand =>
            this.startCaptureCommand;

        private void ExecuteStopCapureCommand(object param) {

            this.ReleaseMouseCapture?.Invoke(this, EventArgs.Empty);

            this.StartCaptureEnabled = true;
        }

        private bool CanExecuteStopCaptureCommand(object param) =>
            this.startCaptureEnabled;


        private void ResetMouseCapturePoints() {

            this.MouseDownPoint = new Point();
            this.MouseMovePoint = new Point();
            this.MouseUpPoint = new Point();
        }
    }
}
