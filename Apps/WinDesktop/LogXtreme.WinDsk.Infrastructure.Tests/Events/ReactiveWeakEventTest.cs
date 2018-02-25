using LogXtreme.Ifrastructure.Tests;
using LogXtreme.Reactive.Extensions;
using LogXtreme.WinDsk.Infrastructure.Tests.Events.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Events {

    /// <summary>
    /// Tests that illustrate the usage of the IObservable<T>.SubscribeWeakly
    /// extension methods.
    /// </summary>
    [TestClass]
    public class ReactiveWeakEventTest : TestBase {

        [TestMethod]
        public void CanCreateWeakEventSubscriptionOnObservableEventAndReceiveTheEventTest() {

            // arrange
            var eventSourceFinalizeTracker = new FinalizeTracker();
            var listenerFinalizeTracker = new FinalizeTracker();
            var eventSource = new GenericStandardNetEventSource<EventArgs>(ref eventSourceFinalizeTracker);
            var listener = new StandardNetEventListener(ref listenerFinalizeTracker);
            var secretMessage = @"secretMessage";

            eventSource.Event += listener.OnEvent;

            // act
            eventSource.Raise(new MessageEventArgs(secretMessage));

            // assert
            Assert.AreEqual(listener.Invokations, 1);
            Assert.IsInstanceOfType(listener.LastReceivedEventArgs, typeof(MessageEventArgs));
            Assert.AreEqual(secretMessage, ((MessageEventArgs)listener.LastReceivedEventArgs).Message);

            // arrange
            var eventObservable = Observable
                .FromEventPattern<EventArgs>(
                h => eventSource.Event += h,
                h => eventSource.Event += h);

            var weakEventSubscription =
                (eventObservable as IObservable<EventPattern<EventArgs>>)
                .SubscribeWeakly(
                    listener,
                    (target, ep) => listener.OnEvent(ep.Sender,ep.EventArgs));            

            // act
            eventSource.Raise(new MessageEventArgs(secretMessage));

            // assert
            Assert.AreEqual(listener.Invokations, 3);
            Assert.IsInstanceOfType(listener.LastReceivedEventArgs, typeof(MessageEventArgs));
            Assert.AreEqual(secretMessage, ((MessageEventArgs)listener.LastReceivedEventArgs).Message);

            // clean-up
            eventSource.Event -= listener.OnEvent;
            weakEventSubscription.Dispose();
            weakEventSubscription = null;
            eventObservable = null;
            listenerFinalizeTracker = null;
            eventSourceFinalizeTracker = null;
            eventSource = null;
            listener = null;
        }

        [TestMethod]
        public void Test() {
            Assert.Fail("Shows what happen with disposing and GC...");
        }
    }
}
