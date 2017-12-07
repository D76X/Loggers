using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reactive.Linq;

namespace LogXtreme.Reactive.Extensions.Test._1 {

    [TestClass]
    public class EventsAndEventToObservableTest {

        [TestMethod]
        public void TestSimpleEventLifecycle() {

            // arrange 
            var testInstance = new TestClassWithEvents();           
            
            // assert
            Assert.AreEqual(0, testInstance.SimpleEventInvokationCounter);
            Assert.IsNull(testInstance.SimpleEventHandlersCount);

            // arrange
            bool simpleEventHandled = false;

            EventHandler handler = (object sender, EventArgs e) => {
                simpleEventHandled = true;
            };

            // act 
            testInstance.SimpleEvent += handler;

            // assert
            Assert.AreEqual(0, testInstance.SimpleEventInvokationCounter);
            Assert.AreEqual(1, testInstance.SimpleEventHandlersCount);            

            // act
            testInstance.RaiseSimpleEvent();

            // assert
            Assert.IsTrue(simpleEventHandled);
            Assert.AreEqual(1, testInstance.SimpleEventInvokationCounter);
            Assert.AreEqual(1, testInstance.SimpleEventHandlersCount);

            // act
            testInstance.SimpleEvent -= handler;
            Assert.IsTrue(simpleEventHandled);
            Assert.AreEqual(1, testInstance.SimpleEventInvokationCounter);
            Assert.IsNull(testInstance.SimpleEventHandlersCount);

            // assert
            testInstance.RaiseSimpleEvent();
            Assert.AreEqual(2, testInstance.SimpleEventInvokationCounter);
        }

        [TestMethod]
        public void TestComplexEventLifecycle() {

            // arrange 
            var testInstance = new TestClassWithEvents();

            // assert
            Assert.AreEqual(0, testInstance.ComplexEventInvokationCounter);
            Assert.IsNull(testInstance.ComplexEventHandlersCount);

            // arrange
            bool complexEventHandled = false;
            string testValue = string.Empty;

            EventHandler<TestEventArgs> handler = (object sender, TestEventArgs e) => {
                complexEventHandled = true;
                testValue = e.Value;
            };
            
            string expectedTestValue = @"payload 1";
            var payload1 = new TestEventArgs(expectedTestValue);

            // act
            testInstance.ComplexEvent += handler;

            // assert
            Assert.AreEqual(0, testInstance.ComplexEventInvokationCounter);
            Assert.AreEqual(1, testInstance.ComplexEventHandlersCount);

            // act 
            testInstance.RaiseComplexEvent(payload1);

            // assert
            Assert.IsTrue(complexEventHandled);
            Assert.AreEqual(1, testInstance.ComplexEventInvokationCounter);
            Assert.AreEqual(expectedTestValue, testValue);

            // act
            testInstance.ComplexEvent -= handler;
            Assert.IsTrue(complexEventHandled);
            Assert.AreEqual(1, testInstance.ComplexEventInvokationCounter);
            Assert.IsNull(testInstance.ComplexEventHandlersCount);

            // arrange 
            var payload2 = new TestEventArgs(@"some payload");

            // assert
            testInstance.RaiseComplexEvent(payload2);
            Assert.AreEqual(2, testInstance.ComplexEventInvokationCounter);
            Assert.AreEqual(expectedTestValue, testValue);
        }

        /// <summary>
        /// Refs
        /// How the Extension methods Observable.FromEventPattern are intended to be used.
        /// http://www.introtorx.com/uat/content/v1.0.10621.0/04_CreatingObservableSequences.html#FromEvent
        /// A thourough explanation of the mechanics used by RX to translate .NET events into observables.
        /// https://stackoverflow.com/questions/19895373/how-to-use-observable-fromevent-instead-of-fromeventpattern-and-avoid-string-lit
        /// </summary>
        [TestMethod]
        public void TestFromEventPatternForSimpleEvent() {

            // arrange 
            var testInstance = new TestClassWithEvents();

            // act

            // an observable of event patterns
            // this creates a factory which support subscription
            // at this point, there is no event subscription taking place. 
            var observable = Observable.FromEventPattern(
                h => testInstance.SimpleEvent += h,
                h => testInstance.SimpleEvent -= h);

            // assert   
            
            // the event has not been raised yet
            Assert.IsNull(testInstance.SimpleEventHandlersCount);
            
            // the handler has not been attached yet
            Assert.AreEqual(0, testInstance.SimpleEventInvokationCounter);

            // arrange
            var simpleEventWasCalled = false;
            object receivedSender = null;
            object receivedArgument = null;

            // act 

            // subscribe to the observable 
            var subscription = observable.Subscribe(eventPattern => {
                receivedSender = eventPattern.Sender;
                receivedArgument = eventPattern.EventArgs;
                simpleEventWasCalled = true;
            });

            // assert

            // an handler has been attached when the observable was 
            // subscribed to
            Assert.AreEqual(1,testInstance.SimpleEventHandlersCount);

            // the event has not been raised yet
            Assert.IsFalse(simpleEventWasCalled);
            Assert.IsNull(receivedSender);
            Assert.IsNull(receivedArgument);            
            Assert.AreEqual(0, testInstance.SimpleEventInvokationCounter);            
            
            // act 
            testInstance.RaiseSimpleEvent();

            // assert
            Assert.IsTrue(simpleEventWasCalled);
            Assert.AreEqual(1, testInstance.SimpleEventInvokationCounter);
            Assert.AreSame(testInstance, receivedSender);
            Assert.IsNotNull(receivedArgument);
            Assert.AreEqual(EventArgs.Empty, receivedArgument);

            // act

            // dispose to detach the handler
            subscription.Dispose();

            // assert

            // the event hanlder has been removed
            Assert.IsNull(testInstance.SimpleEventHandlersCount);

            // arrange
            receivedSender = null;
            receivedArgument = null;

            // act 
            testInstance.RaiseSimpleEvent();

            // assert
            Assert.AreEqual(2, testInstance.SimpleEventInvokationCounter);
            Assert.IsNull(receivedSender);
            Assert.IsNull(receivedArgument);
            Assert.IsNull(testInstance.SimpleEventHandlersCount);
        }

        /// <summary>
        /// Refs
        /// How the Extension methods Observable.FromEventPattern are intended to be used.
        /// http://www.introtorx.com/uat/content/v1.0.10621.0/04_CreatingObservableSequences.html#FromEvent
        /// A thourough explanation of the mechanics used by RX to translate .NET events into observables.
        /// https://stackoverflow.com/questions/19895373/how-to-use-observable-fromevent-instead-of-fromeventpattern-and-avoid-string-lit
        /// </summary>
        [TestMethod]
        public void TestFromEventPatternForComplexEvent() {

            // arrange 
            var testInstance = new TestClassWithEvents();
            
            // act 
            
            // an observable of event patterns
            // this creates a factory which support subscription
            // at this point, there is no event subscription taking place. 
            var observable = Observable.FromEventPattern<TestEventArgs>(
                h => testInstance.ComplexEvent += h,
                h => testInstance.ComplexEvent -= h);            

            // assert   

            // the event has not been raised yet
            Assert.IsNull(testInstance.ComplexEventHandlersCount);

            // the handler has not been attached yet
            Assert.AreEqual(0, testInstance.ComplexEventInvokationCounter);

            // arrange

            var complexEventWasCalled = false;
            object receivedSender = null;
            object receivedArgument = null;

            // act 

            // subscribe to the observable 
            var subscription = observable.Subscribe(eventPattern => {
                receivedSender = eventPattern.Sender;
                receivedArgument = eventPattern.EventArgs;
                complexEventWasCalled = true;
            });

            // assert 

            // an handler has been attached when the observable was 
            // subscribed to
            Assert.AreEqual(1, testInstance.ComplexEventHandlersCount);

            // the event has not been raised yet
            Assert.IsFalse(complexEventWasCalled);
            Assert.IsNull(receivedSender);
            Assert.IsNull(receivedArgument);
            Assert.AreEqual(0, testInstance.SimpleEventInvokationCounter);

            // arrange
            string expectedTestValue = @"payload 1";
            var payload1 = new TestEventArgs(expectedTestValue);

            // act 
            testInstance.RaiseComplexEvent(payload1);

            // assert
            Assert.AreEqual(1, testInstance.ComplexEventHandlersCount);
            Assert.IsTrue(complexEventWasCalled);
            Assert.IsNotNull(receivedSender);
            Assert.IsNotNull(receivedArgument);
            Assert.AreSame(testInstance, receivedSender);
            Assert.AreSame(payload1, receivedArgument);

            // act

            // disposing revoves the handler
            subscription.Dispose();

            // assert
            Assert.IsNull(testInstance.ComplexEventHandlersCount);

            // arrange
            receivedArgument = null;
            receivedSender = null;

            // act
            testInstance.RaiseComplexEvent(payload1);

            // arrange
            Assert.AreEqual(2, testInstance.ComplexEventInvokationCounter);
            Assert.IsNull(testInstance.ComplexEventHandlersCount);
            Assert.IsNull(receivedSender);
            Assert.IsNull(receivedArgument);
        }
    }
}
