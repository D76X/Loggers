using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Events {

    /// <summary>
    /// Tests used for reference to show how to use RX's FromEvent and FromEventPatter.
    /// 
    /// Refs
    /// https://msdn.microsoft.com/en-us/library/hh229241(v=vs.103).aspx
    /// https://stackoverflow.com/questions/19895373/how-to-use-observable-fromevent-instead-of-fromeventpattern-and-avoid-string-lit
    /// http://rxwiki.wikidot.com/101samples#toc6
    /// https://www.codeproject.com/Articles/738109/The-NET-weak-event-pattern-in-Csharp
    /// </summary>
    [TestClass]
    public partial class ReactiveExtensionsFromEventTest {

        [TestMethod]
        public void FromEventActionBasedEvent() {

            // arrange
            var eventSourceFinalizeTracker = new FinalizeTracker();
            var listenerFinalizeTracker = new FinalizeTracker();
            var eventSource = new ActionEventSource(ref eventSourceFinalizeTracker);
            var listener = new ActionEventListener(ref listenerFinalizeTracker);

            eventSource.Event += listener.OnEvent;

            var eventObservable = Observable.FromEvent(
                h => eventSource.Event += h,
                h => eventSource.Event -= h);

            var completed = false;
            var counter = 0;

            var subscription = eventObservable.Subscribe(
                observable => { counter += 1; },
                error => { },
                () => { completed = true; });

            // act
            eventSource.Raise();

            // assert
            Assert.IsTrue(listener.Invokations == 1);
            Assert.IsTrue(counter == 1);
            Assert.IsFalse(completed);

            // act              
            subscription.Dispose(); // this unsubcribes but does not complete!        

            // assert
            Assert.IsFalse(completed);

            // act 
            eventSource.Raise();

            // assert
            Assert.IsTrue(listener.Invokations == 2);
            Assert.IsTrue(counter == 1);
            Assert.IsFalse(completed);

            // act
            eventSource.Event -= listener.OnEvent;
            eventSource.Raise();

            // assert
            Assert.IsTrue(listener.Invokations == 2);
            Assert.IsTrue(counter == 1);

            // act
            Utils.TriggerGC();

            // assert
            Assert.IsFalse(eventSourceFinalizeTracker.IsFinalzed);
            Assert.IsFalse(listenerFinalizeTracker.IsFinalzed);

            // act
            eventSource = null;
            listener = null;
            Utils.TriggerGC();

            // assert
            Assert.IsTrue(eventSourceFinalizeTracker.IsFinalzed);
            Assert.IsTrue(listenerFinalizeTracker.IsFinalzed);
        }

        [TestMethod]
        public void FromEventStandardNetEvent() {

            // arrange
            var eventSourceFinalizeTracker = new FinalizeTracker();
            var listenerFinalizeTracker1 = new FinalizeTracker();
            var listenerFinalizeTracker2 = new FinalizeTracker();
            var eventSource = new StandardNetEventSource(ref eventSourceFinalizeTracker);
            var listener1 = new StandardNetEventListener(ref listenerFinalizeTracker1);
            var listener2 = new StandardNetEventListener(ref listenerFinalizeTracker2);

            string secretMessage = @"secret message";
            var payload = new MessageEventArgs(secretMessage);
            eventSource.Event += listener1.OnEvent;

            // https://stackoverflow.com/questions/19895373/how-to-use-observable-fromevent-instead-of-fromeventpattern-and-avoid-string-lit
            // The first delegate is a factory that provides a conversion from Action<EventArgs> to EventHadler.
            var eventObservable = Observable.FromEvent<EventHandler, EventArgs>(
                h => { return (s, e) => h(e); },
                h => eventSource.Event += h,
                h => eventSource.Event -= h);         

            var completed = false;           

            var subscription = eventObservable.Subscribe(
                args => listener2.OnEvent(null, args),
                error => { Debug.Write($"error in {nameof(FromEventStandardNetEvent)}"); },
                () => { completed = true; });

            // act
            eventSource.Raise(payload);

            // assert
            Assert.IsTrue(listener1.Invokations == 1);
            Assert.IsTrue(listener2.Invokations == 1);
            Assert.IsTrue(listener1.LastReceivedEventArgs is MessageEventArgs);
            Assert.IsTrue(listener2.LastReceivedEventArgs is MessageEventArgs);
            Assert.IsTrue(((MessageEventArgs)listener1.LastReceivedEventArgs).Message == secretMessage);
            Assert.IsTrue(((MessageEventArgs)listener2.LastReceivedEventArgs).Message == secretMessage);
            Assert.IsFalse(completed);
        }
    }
}
