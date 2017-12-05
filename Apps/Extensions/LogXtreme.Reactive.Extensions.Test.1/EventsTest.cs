using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
    }

    [TestClass]
    public class EventsTest {

        [TestMethod]
        public void TestSimpleEventIsRaisedAndHandled() {

            // arrange 
            var testInstance = new TestClassWithEvents();

            bool simpleEventHandled = false;

            EventHandler handler = (object sender, EventArgs e) => {
                simpleEventHandled = true;
            };

            testInstance.SimpleEvent += handler;

            // act 
            testInstance.RaiseSimpleEvent();

            // assert
            Assert.IsTrue(simpleEventHandled);
            Assert.AreEqual(testInstance.SimpleEventInvokationCounter, 1);
        }

        [TestMethod]
        public void TestComplexEventIsRaisedAndHandled() {

            // arrange 
            var testInstance = new TestClassWithEvents();

            bool complexEventHandled = false;
            string testValue = string.Empty;
            string expectedTestValue = @"test";
            var payload = new TestEventArgs(expectedTestValue);            

            EventHandler<TestEventArgs> handler = (object sender, TestEventArgs e) => {
                complexEventHandled = true;
                testValue = e.Value;
            };

            testInstance.ComplexEvent += handler; ;

            // act 
            testInstance.RaiseComplexEvent(payload);

            // assert
            Assert.IsTrue(complexEventHandled);
            Assert.AreEqual(testInstance.ComplexEventInvokationCounter, 1);
            Assert.AreEqual(testValue, expectedTestValue);
        }
    }
}
