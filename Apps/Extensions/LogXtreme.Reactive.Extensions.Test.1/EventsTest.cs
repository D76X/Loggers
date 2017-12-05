using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace LogXtreme.Reactive.Extensions.Test._1 {

    public class TestEventArgs : EventArgs {

        public readonly string Value;

        public TestEventArgs(string val) {
            this.Value = val;
        }
    }

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

    [TestClass]
    public class EventsTest {

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
        /// http://www.introtorx.com/uat/content/v1.0.10621.0/04_CreatingObservableSequences.html#FromEvent
        /// 
        /// https://stackoverflow.com/questions/19895373/how-to-use-observable-fromevent-instead-of-fromeventpattern-and-avoid-string-lit
        /// </summary>
        [TestMethod]
        public void TestFromEventPattern() {

            // arrange 
            var testInstance = new TestClassWithEvents();

            // an observable of event patterns
            var observable = Observable.FromEventPattern(
                h => testInstance.SimpleEvent += h,
                h => testInstance.SimpleEvent -= h);

            // assert            
            Assert.IsNull(testInstance.SimpleEventHandlersCount);
            Assert.AreEqual(0, testInstance.SimpleEventInvokationCounter);

            var simpleEventWasCalled = false;
            object receivedSender = null;
            object receivedArgument = null;

            // subscribe to the observable to 
            var subscription = observable.Subscribe(eventPattern => {
                receivedSender = eventPattern.Sender;
                receivedArgument = eventPattern.EventArgs;
            });

            // assert
            Assert.IsNull(receivedSender);
            Assert.IsNull(receivedArgument);
            Assert.IsNull(testInstance.SimpleEventHandlersCount);
            Assert.AreEqual(0, testInstance.SimpleEventInvokationCounter);

            // act 
            testInstance.RaiseComplexEvent();

            // assert
            Assert.Fail("test not implemented");
        }
    }
}
