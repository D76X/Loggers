using LogXtreme.WinDsk.Infrastructure.Events;
using LogXtreme.WinDsk.Infrastructure.Services;
using System;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.Modules.Services {

    public class AvalonDockService : IAvalonDockService {

        private Dictionary<string, Type> registeredParts =
            new Dictionary<string, Type>();

        public IDictionary<string, Type> RegisteredParts =>
            this.registeredParts;

        public event EventHandler<AvalonDockEventArgs> DockingManagerChanged;

        public void RaiseDockingManagerChanged(
            object sender,
            AvalonDockEventArgs args) {

            this.DockingManagerChanged?.Invoke(sender, args);
        }

        public void RegisterPart<T>(string partName) {

            if (this.registeredParts.ContainsKey(partName)) {
                return;
            }

            this.registeredParts.Add(partName, typeof(T));
        }
    }
}
