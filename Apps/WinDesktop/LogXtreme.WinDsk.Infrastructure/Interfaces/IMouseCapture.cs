using LogXtreme.WinDsk.Infrastructure.Events;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Interfaces {

    /// <summary>
    /// A marker interface.
    /// </summary>
    public interface IMouseCaptureLogic { }

    /// <summary>
    /// Interface that formalizes mouse interaction on the UI.
    /// Refs
    /// https://stackoverflow.com/questions/34984093/mouse-position-with-respect-to-image-in-wpf-using-mvvm/34984467#34984467
    /// On UIElemet.MouseCapture and UIElement.ReleaseMouseCapture.
    /// https://stackoverflow.com/questions/12148158/to-use-or-not-to-use-releasemousecapture
    /// https://msdn.microsoft.com/en-us/library/system.windows.uielement.capturemouse(v=vs.110).aspx
    /// https://msdn.microsoft.com/en-us/library/system.windows.uielement.releasemousecapture(v=vs.110).aspx
    /// </summary>
    public interface IMouseCapture {

        /// <summary>
        /// May hold a reference to the object that implements IMouseCapture
        /// that is that provides the logic for <see cref="IMouseCapture"/>.
        /// </summary>
        IMouseCaptureLogic MouseCaptureLogic { get; set; }

        event EventHandler CaptureMouse;
        void RaiseCaptureMouse(IMouseCaptureLogic sender, MouseCaptureEventArgs e);

        event EventHandler ReleaseMouseCapture;
        void RaiseReleaseMouseCapture(IMouseCaptureLogic sender, MouseCaptureEventArgs e);

        void OnMouseDown(object sender, MouseCaptureEventArgs e);
        void OnMouseMove(object sender, MouseCaptureEventArgs e);
        void OnMouseUp(object sender, MouseCaptureEventArgs e);
    }    
}
