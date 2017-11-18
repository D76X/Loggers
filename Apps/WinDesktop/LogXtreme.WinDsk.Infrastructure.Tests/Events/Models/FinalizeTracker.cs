namespace LogXtreme.WinDsk.Infrastructure.Tests.Events.Models {
    internal class FinalizeTracker {

        private bool isFinalized = false;

        public bool IsFinalzed => this.isFinalized;

        public void MarkAsFinalized() { this.isFinalized = true; }
    }

}
