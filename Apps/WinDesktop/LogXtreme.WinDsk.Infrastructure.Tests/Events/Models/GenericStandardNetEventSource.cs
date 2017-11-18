using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Events.Models {
    class GenericStandardNetEventSource<TEventArgs>
        where TEventArgs : EventArgs {

        private FinalizeTracker finalizeTracker;

        public event EventHandler<TEventArgs> Event = delegate { };

        public GenericStandardNetEventSource(ref FinalizeTracker finalizeTracker) {
            this.finalizeTracker = finalizeTracker;
        }

        public void Raise(TEventArgs args) {
            this.Event(this, args);
        }

        ~GenericStandardNetEventSource() {
            this.finalizeTracker.MarkAsFinalized();
        }
    }
}
