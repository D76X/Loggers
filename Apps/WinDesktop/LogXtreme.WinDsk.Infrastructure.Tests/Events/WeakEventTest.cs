using LogXtreme.WinDsk.Infrastructure.Tests.Events.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Events {
    [TestClass]
    public class WeakEventTest {

        [TestMethod]
        public void ListenerIsGCedWhenThereAreNoRefsOtherThanWeakEvent() {

            // arrange
            var eventSourceFinalizeTracker = new FinalizeTracker();
            var listenerFinalizeTracker = new FinalizeTracker();
            var eventSource = new StandardNetEventSource(ref eventSourceFinalizeTracker);
            var listener = new StandardNetEventListener(ref listenerFinalizeTracker);

            WeakEventManager<StandardNetEventSource, EventArgs>.AddHandler(eventSource, nameof(eventSource.Event), listener.OnEvent);

            // act
            eventSource.Raise();

            // assert
            Assert.IsTrue(listener.Invokations == 1);

            // act 
            Utils.TriggerGC();
            eventSource.Raise();

            // assert
            Assert.IsTrue(listener.Invokations == 2);
            Assert.IsFalse(listenerFinalizeTracker.IsFinalzed);

            // act
            var secretMessage = @"secret message";
            var payload = new MessageEventArgs(secretMessage);
            eventSource.Raise(payload);

            // assert
            Assert.IsTrue(listener.Invokations == 3);
            Assert.IsFalse(listenerFinalizeTracker.IsFinalzed);
            Assert.IsInstanceOfType(listener.LastReceivedEventArgs, typeof(MessageEventArgs));
            Assert.AreEqual(secretMessage, ((MessageEventArgs)listener.LastReceivedEventArgs).Message);

            // act
            listener = null;
            Utils.TriggerGC();
            eventSource.Raise();

            // assert
            Assert.IsTrue(listenerFinalizeTracker.IsFinalzed);

            // arrange
            listenerFinalizeTracker = new FinalizeTracker();
            listener = new StandardNetEventListener(ref listenerFinalizeTracker);
            WeakEventManager<StandardNetEventSource, EventArgs>.AddHandler(eventSource, nameof(eventSource.Event), listener.OnEvent);            

            // assert
            Assert.IsFalse(listenerFinalizeTracker.IsFinalzed);
            Assert.IsTrue(listener.Invokations == 0);
            Assert.IsNull(listener.LastReceivedEventArgs);

            // act
            eventSource.Raise();

            // assert
            Assert.IsTrue(listener.Invokations == 1);

            // act
            eventSource.Raise(payload);

            // assert
            Assert.IsTrue(listener.Invokations == 2);
            Assert.IsFalse(listenerFinalizeTracker.IsFinalzed);
            Assert.IsInstanceOfType(listener.LastReceivedEventArgs, typeof(MessageEventArgs));
            Assert.AreEqual(secretMessage, ((MessageEventArgs)listener.LastReceivedEventArgs).Message);

            // act
            WeakEventManager<StandardNetEventSource, EventArgs>.RemoveHandler(eventSource, nameof(eventSource.Event), listener.OnEvent);
            eventSource.Raise();

            // assert
            Assert.IsTrue(listener.Invokations == 2);
            Assert.IsFalse(listenerFinalizeTracker.IsFinalzed);

            // act
            listener = null;
            Utils.TriggerGC();

            // arrange
            Assert.IsTrue(listenerFinalizeTracker.IsFinalzed);
        }
    }
}
