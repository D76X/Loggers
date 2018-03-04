using System;
using System.Windows;
using System.Windows.Input;

namespace LogXtreme.WinDsk.Infrastructure.Events {

    /// <summary>
    /// Event arguments of interest in the interaction between the UI and the mouse.
    /// Refs
    /// https://stackoverflow.com/questions/34984093/mouse-position-with-respect-to-image-in-wpf-using-mvvm/34984467#34984467
    /// </summary>
    public class MouseCaptureEventArgs : EventArgs {

        public MouseCaptureEventArgs() { }

        public MouseCaptureEventArgs(
            IInputElement relativeTo,
            MouseEventArgs e) {

            var position = e.GetPosition(relativeTo);

            this.X = position.X;
            this.Y = position.Y;
            this.LeftButton = (e.LeftButton == MouseButtonState.Pressed);
            this.RightButton = (e.RightButton == MouseButtonState.Pressed);
        }

        public double X { get; set; }
        public double Y { get; set; }
        public bool LeftButton { get; set; }
        public bool RightButton { get; set; }
    }
}
