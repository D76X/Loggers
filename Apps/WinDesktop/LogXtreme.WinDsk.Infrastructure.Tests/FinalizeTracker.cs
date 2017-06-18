﻿namespace LogXtreme.WinDsk.Infrastructure.Tests {
    internal class FinalizeTracker {

        private bool isFinalized = false;

        public bool IsFinalzed => this.isFinalized;

        public void MarkAsFinalized() { this.isFinalized = true; }
    }

}
