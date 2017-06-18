using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests {
    internal class StandardNetEventSource {

        public event EventHandler Event = delegate { };

        public void Raise() {
            Event(this, EventArgs.Empty);
        }
    }

}
