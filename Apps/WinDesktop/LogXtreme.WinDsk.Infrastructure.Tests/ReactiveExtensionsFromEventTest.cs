using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.Infrastructure.Tests {

    /// <summary>  
    /// Refs
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
            Assert.IsTrue(listener.Invokations==1);
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
            var listenerFinalizeTracker = new FinalizeTracker();
            var eventSource = new StandardNetEventSource(ref eventSourceFinalizeTracker);
            var listener = new StandardNetEventListener(ref listenerFinalizeTracker);

            // act
            string secretMessage = @"secret message";
            eventSource.Event += listener.OnEvent;
            var payload = new MessageEventArgs(secretMessage);

            // we do not need a conversion function here as both the source and the listener work with the
            // standard .NET event signature
            var eventObservable = Observable.FromEvent<EventHandler, EventHandler>(               
                h => eventSource.Event += h,
                h => eventSource.Event -= h);

            var completed = false;            

            var subscription = eventObservable.Subscribe(
                observable => { counter += 1; },
                error => { },
                () => { completed = true; });

            // act
            eventSource.Raise(payload);

            // assert
            Assert.IsTrue(listener.Invokations == 1);
            Assert.IsTrue(counter == 1);
            Assert.IsFalse(completed);          
        }
    }
}
