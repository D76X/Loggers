using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.Infrastructure.Tests {

    [TestClass]
    public class ReactiveExtensionsFromEventTest {

        private class ActionEventSource {

            public event Action Event = delegate { };

            public void Raise() {
                Event();
            }
        }

        private class ActionEventListener {

            private int invokations = 0;

            public void OnEvent() {
                this.invokations += 1;
            }        

            public int Invokations => this.invokations;
        }

        private class StandardNetEventSource {

            public event EventHandler Event = delegate { };

            public void Raise() {
                Event(this, EventArgs.Empty);
            }
        }

        private class StandardNetEventListener {

            private int invokations = 0;  

            public void OnEvent(object source, EventArgs args) {
                this.invokations += 1;
            }          

            public int Invokations => this.invokations;
        }

        private static void TriggerGC() {
            Console.WriteLine("Starting GC.");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.WriteLine("GC finished.");
        }

        [TestMethod]
        public void FromEventActionBasedEvent() {

            // arrange
            var eventSource = new ActionEventSource();
            var listener = new ActionEventListener();

            eventSource.Event += listener.OnEvent;

            var eventObservable = Observable.FromEvent(
                h => eventSource.Event += h,
                h => eventSource.Event -= h);

            var counter = 0;
            // var subscription = eventObservable.Subscribe(observer => { counter += 1; });
            var subscription = eventObservable.Subscribe(o => { }, err => { }, () => { });

            // act
            eventSource.Raise();

            // assert
            Assert.IsTrue(listener.Invokations==1);
            Assert.IsTrue(counter == 1);
        }
    }
}
