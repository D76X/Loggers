using System;

namespace LogXtreme.Reactive.Extensions.Test._1 {
    public class TestClassWithEvents {

        public event EventHandler SimpleEvent;
        public event EventHandler<TestEventArgs> ComplexEvent;

        public int SimpleEventInvokationCounter;
        public int ComplexEventInvokationCounter;

        public void RaiseSimpleEvent() {

            this.SimpleEventInvokationCounter += 1;
            this.SimpleEvent?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseComplexEvent(TestEventArgs payload) {

            this.ComplexEventInvokationCounter += 1;
            this.ComplexEvent?.Invoke(this, payload);
        }

        public int? SimpleEventHandlersCount {
            get => this.SimpleEvent?.GetInvocationList()?.Length;            
        }

        public int? ComplexEventHandlersCount {
            get => this.ComplexEvent?.GetInvocationList()?.Length;
        }
    }
}
