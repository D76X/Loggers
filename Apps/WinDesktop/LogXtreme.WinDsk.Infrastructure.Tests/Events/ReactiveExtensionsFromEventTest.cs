using LogXtreme.Ifrastructure.Tests;
using LogXtreme.WinDsk.Infrastructure.Tests.Events.Models;
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
    /// https://rehansaeed.com/reactive-extensions-part2-wrapping-events/
    /// </summary>
    [TestClass]
    public partial class ReactiveExtensionsFromEventTest : TestBase {

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

            // clean-up
            subscription.Dispose();
            subscription = null;
            eventObservable = null;
            listenerFinalizeTracker = null;
            eventSourceFinalizeTracker = null;
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
            // The first delegate must be provided otherwise the hanlder will not be executed on the observer.
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

            // act
            eventSource.Raise(payload);

            // assert
            Assert.IsTrue(listener1.Invokations == 2);
            Assert.IsTrue(listener2.Invokations == 2);
            Assert.IsTrue(listener1.LastReceivedEventArgs is MessageEventArgs);
            Assert.IsTrue(listener2.LastReceivedEventArgs is MessageEventArgs);
            Assert.IsTrue(((MessageEventArgs)listener1.LastReceivedEventArgs).Message == secretMessage);
            Assert.IsTrue(((MessageEventArgs)listener2.LastReceivedEventArgs).Message == secretMessage);
            Assert.IsFalse(completed);

            // act 
            eventSource.Event -= listener1.OnEvent;
            eventSource.Raise(payload);

            // assert
            Assert.IsTrue(listener1.Invokations == 2);
            Assert.IsTrue(listener2.Invokations == 3);
            Assert.IsTrue(listener1.LastReceivedEventArgs is MessageEventArgs);
            Assert.IsTrue(listener2.LastReceivedEventArgs is MessageEventArgs);
            Assert.IsTrue(((MessageEventArgs)listener1.LastReceivedEventArgs).Message == secretMessage);
            Assert.IsTrue(((MessageEventArgs)listener2.LastReceivedEventArgs).Message == secretMessage);
            Assert.IsFalse(completed);

            // act
            Utils.TriggerGC();

            // assert
            Assert.IsFalse(eventSourceFinalizeTracker.IsFinalzed);
            Assert.IsFalse(listenerFinalizeTracker1.IsFinalzed);
            Assert.IsFalse(listenerFinalizeTracker2.IsFinalzed);

            // act
            subscription.Dispose(); // this couse the handlers to get detached from the source
            eventSource = null;
            listener1 = null;
            Utils.TriggerGC();

            // assert
            Assert.IsTrue(eventSourceFinalizeTracker.IsFinalzed);
            Assert.IsTrue(listenerFinalizeTracker1.IsFinalzed);
            Assert.IsFalse(listenerFinalizeTracker2.IsFinalzed); // the subscription still points to listener2

            // act            
            listener2 = null;
            Utils.TriggerGC();

            // assert
            Assert.IsTrue(listenerFinalizeTracker2.IsFinalzed);

            // clean-up
            subscription.Dispose();
            subscription = null;
            eventObservable = null;
            listenerFinalizeTracker1 = null;
            listenerFinalizeTracker2 = null;
            eventSourceFinalizeTracker = null;
        }

        [TestMethod]
        public void FromEventPattern() {

            // arrange 
            var eventSourceFinalizeTracker = new FinalizeTracker();
            var listenerFinalizeTracker = new FinalizeTracker();
            var eventSource = new GenericStandardNetEventSource<EventArgs>(ref eventSourceFinalizeTracker);
            var listener = new StandardNetEventListener(ref listenerFinalizeTracker);

            var eventObservable = Observable.FromEventPattern<EventArgs>(
                h => eventSource.Event += h,
                h => eventSource.Event -= h);

            var subscription = eventObservable.Subscribe(
                eventPattern => listener.OnEvent(eventPattern.Sender, eventPattern.EventArgs),
                error => { },
                () => { });

            var secretMessage = @"secretMessage";

            // act
            eventSource.Raise(new MessageEventArgs(secretMessage));

            // assert
            Assert.AreEqual(listener.Invokations, 1);
            Assert.IsInstanceOfType(listener.LastReceivedEventArgs, typeof(MessageEventArgs));
            Assert.AreEqual(secretMessage, ((MessageEventArgs)listener.LastReceivedEventArgs).Message);

            // clean-up
            subscription.Dispose();
            subscription = null;
            eventObservable = null;
            listenerFinalizeTracker = null;
            eventSourceFinalizeTracker = null;
        }

        [TestMethod]
        public void FromEventPatternWithTypeCastTest() {

            // arrange 
            var eventSourceFinalizeTracker = new FinalizeTracker();
            var listenerFinalizeTracker = new FinalizeTracker();
            var eventSource = new StandardNetEventSource(ref eventSourceFinalizeTracker);
            var listener = new StandardNetEventListener(ref listenerFinalizeTracker);

            var eventObservable = Observable.FromEventPattern(
                h => eventSource.Event += h,
                h => eventSource.Event -= h);

            var subscription = eventObservable.Subscribe(
                eventPattern => listener.OnEvent(eventPattern.Sender, eventPattern.EventArgs as EventArgs),
                error => { },
                () => { });

            // act
            eventSource.Raise();

            // assert
            Assert.AreEqual(listener.Invokations, 1);

            // clean-up
            subscription.Dispose();
            subscription = null;
            eventObservable = null;
            listenerFinalizeTracker = null;
            eventSourceFinalizeTracker = null;
        }        

        [TestMethod]
        public void FromEventPatternToAction() {

            // arrange 
            var eventSourceFinalizeTracker = new FinalizeTracker();
            var listenerFinalizeTracker = new FinalizeTracker();
            var eventSource = new GenericStandardNetEventSource<MessageEventArgs>(ref eventSourceFinalizeTracker);

            var eventObservable = Observable.FromEventPattern<MessageEventArgs>(                
                h => eventSource.Event += h,
                h => eventSource.Event -= h).
                Select(x => x.EventArgs.Message);

            var readMessage = string.Empty;
            var secretMessage = @"secretMessage";
            var subscription = eventObservable.Subscribe(m => readMessage = m);

            // act
            eventSource.Raise(new MessageEventArgs(secretMessage));

            // assert
            Assert.AreEqual(readMessage, secretMessage);

            // clean-up
            subscription.Dispose();
            subscription = null;
            eventObservable = null;
            listenerFinalizeTracker = null;
            eventSourceFinalizeTracker = null;

        }
    }
}
