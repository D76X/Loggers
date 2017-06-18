namespace LogXtreme.WinDsk.Infrastructure.Tests {

    internal class ActionEventListener {

        private int invokations = 0;
        private FinalizeTracker finalizeTracker;

        public ActionEventListener(ref FinalizeTracker finalizeTracker) {
            this.finalizeTracker = finalizeTracker;
        }

        public void OnEvent() { this.invokations += 1; }

        public int Invokations => this.invokations;

        ~ActionEventListener() {
            this.finalizeTracker.MarkAsFinalized();
        }
    }

}
