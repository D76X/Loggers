using LogXtreme.WinDsk.Infrastructure.Events;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    public class MouseCaptureState : MouseCaptureStateBase {

        public MouseCaptureState() { }

        public MouseCaptureState(
            MouseCaptureEventArgs e): base(e) {            
        }        
    }
}
