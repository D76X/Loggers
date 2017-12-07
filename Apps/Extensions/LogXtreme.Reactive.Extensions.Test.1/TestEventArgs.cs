using System;

namespace LogXtreme.Reactive.Extensions.Test._1 {
    public class TestEventArgs : EventArgs {

        public readonly string Value;

        public TestEventArgs(string val) {
            this.Value = val;
        }
    }
}
