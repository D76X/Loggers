
namespace LogXtreme.WinDsk.Infrastructure.Interfaces {

    public interface IMouseCaptureState {

        double X { get; set; }
        double Y { get; set; }

        bool LeftButton { get; set; }
        bool RightButton { get; set; }
        bool MiddleButton { get; set; }        
    }
}
