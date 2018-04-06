using LogXtreme.WinDsk.Infrastructure.Events;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Services {
    public interface IAvalonDockService {

        event EventHandler<AvalonDockEventArgs> DockingManagerChanged;

        void RaiseDockingManagerChanged(object sender, AvalonDockEventArgs args);
    }
}
