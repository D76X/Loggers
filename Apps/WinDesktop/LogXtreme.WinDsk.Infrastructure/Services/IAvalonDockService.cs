using LogXtreme.WinDsk.Infrastructure.Events;
using System;
using System.Collections.Generic;
using Xceed.Wpf.AvalonDock.Layout;

namespace LogXtreme.WinDsk.Infrastructure.Services {
    public interface IAvalonDockService {

        event EventHandler<AvalonDockEventArgs> DockingManagerChanged;
        void RaiseDockingManagerChanged(object sender, AvalonDockEventArgs args);

        IDictionary<string, Type> RegisteredRegionNames { get; }

        void RegisterRegionName<T>(string regionName) where T: LayoutContent;
    }
}
