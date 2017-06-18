using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests {

    internal class ActionEventSource {

        private FinalizeTracker finalizeTracker;

        public event Action Event = delegate { };

        public ActionEventSource(ref FinalizeTracker finalizeTracker) {
            this.finalizeTracker = finalizeTracker;
        }

        public void Raise() {
            Event();
        }

        ~ActionEventSource() {
            this.finalizeTracker.MarkAsFinalized();
        }
    }

}
