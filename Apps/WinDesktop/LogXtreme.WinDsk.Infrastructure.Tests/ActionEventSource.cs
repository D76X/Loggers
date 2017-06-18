using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests {

        internal class ActionEventSource {

            public event Action Event = delegate { };
            private FinalizeTracker finalizeTracker;

            public ActionEventSource(ref  FinalizeTracker finalizeTracker) {
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
