using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests {

    internal class StandardNetEventListener {

        private int invokations = 0;
        private FinalizeTracker finalizeTracker;
        private EventArgs lastReceivedEventArgs;

        public StandardNetEventListener(ref FinalizeTracker finalizeTracker) {
            this.finalizeTracker = finalizeTracker;
        }

        public void OnEvent(object source, EventArgs args) {
            this.invokations += 1;
            this.lastReceivedEventArgs = args;
        }

        public int Invokations => this.invokations;

        public EventArgs LastReceivedEventArgs => this.LastReceivedEventArgs;

        ~StandardNetEventListener() {
            this.finalizeTracker.MarkAsFinalized();
        }
    }
}

