using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests {

    internal class StandardNetEventListener {

        private int invokations = 0;

        public void OnEvent(object source, EventArgs args) {
            this.invokations += 1;
        }

        public int Invokations => this.invokations;
    }
}

