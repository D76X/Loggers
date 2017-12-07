
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reactive.Linq;

namespace LogXtreme.Reactive.Extensions.Test._1 {

    [TestClass]
    public class SubscribeWeaklyTest {

        class HandlingClassState {

            public int SimpleEventHanlderInvokationCounter { get; set; }
            public int ComplexEventHanlderInvokationCounter { get; set; }
        }

        class HandlingClass {

            private HandlingClassState state;

            public HandlingClass(HandlingClassState state) {
                this.state = state;
            }
            
            public int SimpleEventHanlderInvokationCounter { get; private set; }
            public int ComplexEventHanlderInvokationCounter { get; private set; }

            public void SimpleEventHanlder(object sender, EventArgs e) {

                this.SimpleEventHanlderInvokationCounter += 1;
                this.state.SimpleEventHanlderInvokationCounter += 1;
            }

            public void ComplexEventHanlder(object sender, TestEventArgs e) {

                this.ComplexEventHanlderInvokationCounter += 1;
                this.state.ComplexEventHanlderInvokationCounter += 1;
            }
        }

        [TestMethod]
        public void TestSimpleEventStrongAndWeakSubcriptionLifecycles() {

            // arrange
            var testClassWithEvents = new TestClassWithEvents();

            // form a direct strong reference to the handling instance
            var strongState = new HandlingClassState();
            var strongSubscriber = new HandlingClass(strongState);

            // form a strong reference to a weak subscriber
            var weakState = new HandlingClassState();
            var weakSubscriber = new HandlingClass(weakState);

            // act

            // form a strong reference to the handling instance via the event
            testClassWithEvents.SimpleEvent += strongSubscriber.SimpleEventHanlder;            

            // form a weak subscription from the event to the weak subscriber
            var observable = Observable.FromEventPattern(
                h => testClassWithEvents.SimpleEvent += h,
                h => testClassWithEvents.SimpleEvent -= h)
                .SubscribeWeakly(
                weakSubscriber,
                (target, ep) => target.SimpleEventHanlder(ep.Sender, ep.EventArgs as EventArgs));

            // assert
            Assert.AreEqual(0, strongSubscriber.SimpleEventHanlderInvokationCounter);
            Assert.AreEqual(0, weakSubscriber.SimpleEventHanlderInvokationCounter);
            Assert.AreEqual(0, strongState.SimpleEventHanlderInvokationCounter);
            Assert.AreEqual(0, weakState.SimpleEventHanlderInvokationCounter);

            // act 
            testClassWithEvents.RaiseSimpleEvent();

            // assert
            Assert.AreEqual(1, strongSubscriber.SimpleEventHanlderInvokationCounter);
            Assert.AreEqual(1, weakSubscriber.SimpleEventHanlderInvokationCounter);
            Assert.AreEqual(1, strongState.SimpleEventHanlderInvokationCounter);
            Assert.AreEqual(1, weakState.SimpleEventHanlderInvokationCounter);

            // act
            strongSubscriber = null;
            GC.Collect();
            testClassWithEvents.RaiseSimpleEvent();

            // assert

            // the strong subscriber cannot be collected because of the event reference
            // we tets the strong state to verify the invokation on the strongSubscriber
            // is still possible. This proves that we have a memory leak as we have not
            // removed the handler before nulling the strong reference.

            // The following is no longer possible
            //Assert.AreEqual(2, strongSubscriber.SimpleEventHanlderInvokationCounter);

            Assert.AreEqual(2, strongState.SimpleEventHanlderInvokationCounter);

            // the weak subscriber cannot be collected because there is a reference to it 
            // other than the weak event subscription
            Assert.AreEqual(2, weakSubscriber.SimpleEventHanlderInvokationCounter);
            Assert.AreEqual(2, weakState.SimpleEventHanlderInvokationCounter);

            // act
            weakSubscriber = null;
            GC.Collect();
            testClassWithEvents.RaiseSimpleEvent();

            // assert
            // the strong state still gets updated by the uncollected strongReference
            Assert.AreEqual(3, strongState.SimpleEventHanlderInvokationCounter);

            // The following is no longer possible 
            //Assert.AreEqual(2, weakSubscriber.SimpleEventHanlderInvokationCounter);

            // the weak reference has been collected and the weak state is unchanged
            Assert.AreEqual(2, weakState.SimpleEventHanlderInvokationCounter);
        }
    }
}
