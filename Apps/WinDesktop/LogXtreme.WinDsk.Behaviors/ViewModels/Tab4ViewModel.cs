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
        //private IMouseCapture mouseCapture;
        
        //private RelayCommand<object> notifyCommand;
        //private bool enabled;
        //private int clickCounts;

        public Tab4ViewModel() {

            //this.notifyCommand = new RelayCommand<object>(
            //this.ExecuteSomeCommand,
            //this.CanExecuteSomeCommand);

            //this.Enabled = true;
        }

        private void OnCapture(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        public string Message {
            get => this.message;
            set => this.SetProperty<string>(ref this.message, value);
        }

        /// <summary>
        /// In this case the view model implements IMouseCapture and is 
        /// exposed as a proxy for the mouse capture but in general a 
        /// specialized model or sub-view-model may also be used.
        /// </summary>
        public IMouseCapture MouseCaptureProxy {
            get => this;
            //set => this.SetProperty<IMouseCapture>(ref this.mouseCapture, value);
        }

        #region IMouseCapture

        // when the behavior MouseCaptureBehavior latches to an implementation of 
        // IMouseCapture such as this view model it registers events handlers with
        // the two events IMouseCapture.Capture and IMouseCapture.Release. Raising
        // the events from the implementation of IMouseCapture causes the behavior
        // to invoke the UIElement.CaptureMouse(), UIElement.ReleaseMouseCapture() 
        // respectively.
        public event EventHandler Capture = delegate { };
        public event EventHandler Release = delegate { };

        public void OnMouseDown(object sender, MouseCaptureEventArgs e) {
            throw new NotImplementedException();
        }

        public void OnMouseMove(object sender, MouseCaptureEventArgs e) {
            throw new NotImplementedException();
        }

        public void OnMouseUp(object sender, MouseCaptureEventArgs e) {
            throw new NotImplementedException();
        } 
        #endregion

        //public bool Enabled {

        //    get { return this.enabled; }

        //    set {
        //        this.SetProperty<bool>(ref this.enabled, value);
        //        this.notifyCommand.RaiseCanExecuteChanged();
        //    }
        //}

        //public ICommand NotifyCommand =>
        //    this.notifyCommand;

        //private void ExecuteSomeCommand(object param) {

        //    string triggeredOn = $" {DateTime.Now}";
        //    string triggeredAt = $"x=?, y=?";

        //    if (param is Point) {

        //        Point p = (Point) param;

        //        triggeredAt = $"x={p.X}, y={p.Y}";
        //    }


        //    this.Message = $"Triggered on {triggeredOn} at {triggeredAt}";
        //}

        //private bool CanExecuteSomeCommand(object param) => this.enabled;
    }
}
