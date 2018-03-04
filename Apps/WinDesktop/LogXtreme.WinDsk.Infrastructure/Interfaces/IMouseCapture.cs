using LogXtreme.WinDsk.Infrastructure.Events;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Interfaces {

    /// <summary>
    /// Interface that formalizes mouse interaction on the UI.
    /// Refs
    /// https://stackoverflow.com/questions/34984093/mouse-position-with-respect-to-image-in-wpf-using-mvvm/34984467#34984467
    /// </summary>
    public interface IMouseCapture {

        event EventHandler Capture;
        event EventHandler Release;

        void OnMouseDown(object sender, MouseCaptureEventArgs e);
        void OnMouseMove(object sender, MouseCaptureEventArgs e);
        void OnMouseUp(object sender, MouseCaptureEventArgs e);
    }
}
