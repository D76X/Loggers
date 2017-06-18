using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests {
    internal class StandardNetEventSource {

        private int invokations = 0;
        private FinalizeTracker finalizeTracker;

        public event EventHandler Event = delegate { };

        public StandardNetEventSource(ref FinalizeTracker finalizeTracker) {
            this.finalizeTracker = finalizeTracker;
        }

        public void Raise() {
            Event(this, EventArgs.Empty);
        }

        ~StandardNetEventSource() {
            this.finalizeTracker.MarkAsFinalized();
        }
    }

}
