using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Services;
using System;

namespace LogXtreme.WinDsk.Modules.Services {

    public class AvalonDockService : IAvalonDockService {            

        public event EventHandler<AvalonDockEventArgs> DockingManagerChanged;

        public void RaiseDockingManagerChanged(
            object sender,
            AvalonDockEventArgs args) {

            this.DockingManagerChanged?.Invoke(sender, args);
        }
    }
}
