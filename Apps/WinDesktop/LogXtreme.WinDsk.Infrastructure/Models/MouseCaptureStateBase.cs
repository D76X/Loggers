using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Interfaces;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    public class MouseCaptureStateBase : IMouseCaptureState {

        public MouseCaptureStateBase() { }

        public MouseCaptureStateBase(MouseCaptureEventArgs e) {

            this.X = e.X;
            this.Y = e.Y;

            this.LeftButton = e.LeftButton;
            this.RightButton = e.RightButton;
            this.MiddleButton = e.MiddleButton;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public bool LeftButton { get; set; }
        public bool RightButton { get; set; }
        public bool MiddleButton { get; set; }
    }
}
