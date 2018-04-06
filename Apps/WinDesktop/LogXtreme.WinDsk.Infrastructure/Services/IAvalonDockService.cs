using LogXtreme.WinDsk.Infrastructure.Events;
using System;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.Infrastructure.Services {

    public interface IAvalonDockService {

        event EventHandler<AvalonDockEventArgs> DockingManagerChanged;

        void RaiseDockingManagerChanged(object sender, AvalonDockEventArgs args);

        IDictionary<string, Type> RegisteredParts { get; }

        void RegisterPart<T>(string partName);
    }
}
