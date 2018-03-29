using System;
using System.Collections.Generic;
using Xceed.Wpf.AvalonDock.Layout;

namespace LogXtreme.WinDsk.Modules.Services {

    public class AvalonDockService : IAvalonDockService {

        private Dictionary<string, Type> registerdRegionNames = 
            new Dictionary<string, Type>();

        public IDictionary<string, Type> RegisteredRegionNames =>
            this.registerdRegionNames;        

        public event EventHandler<AvalonDockEventArgs> DockingManagerChanged;

        public void RaiseDockingManagerChanged(
            object sender,
            AvalonDockEventArgs args) {

            this.DockingManagerChanged?.Invoke(sender, args);
        }

        public void RegisterRegionName<T>(string regionName) where T : LayoutContent {

            if (this.registerdRegionNames.ContainsKey(regionName)) {
                return;
            }

            this.registerdRegionNames.Add(regionName, typeof(T));
        }
    }
}
