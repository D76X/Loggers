using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Refs
/// Observable factory methods
/// http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html#ObservableRange
/// How to implement an observer
/// https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-implement-an-observer
/// Subjects
/// http://www.introtorx.com/Content/v1.0.10621.0/02_KeyTypes.html#Subject
/// http://reactivex.io/documentation/subject.html
/// Observable.Create
/// http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html#ObservableCreate
/// </summary>
namespace LogXtreme.Reactive.Extensions.Test._1 {

    [TestClass]
    public class FlushOnTriggerTest {

        [TestMethod]
        public void EventsAreQueuedUntilThresholdIsReachedAndThenTheyPassThroughTheQueueToTheObserverTest() {

            // arrange
            int threshold = 3;
            int bufferSize = threshold+3;
            var receivedValuesFromUnbufferedSubscription = new List<int>();
            var receivedValuesFromBufferedSubscription = new List<int>();

            var subject = new Subject<int>();
            IObservable<int> observable = subject;

            Func<int, bool> triggerPredicate = i => {
                return i > threshold;
            };

            IObservable<int> bufferedObservable = 
                observable.FlushOnTrigger<int>(triggerPredicate, bufferSize);

            var subcriberToUnbuffered = observable
                .Subscribe(i => receivedValuesFromUnbufferedSubscription.Add(i));

            var subcriberToBuffered = bufferedObservable
                .Subscribe(i => receivedValuesFromBufferedSubscription.Add(i));

            // act
            // do nothing!

            // assert - if threshold fails the test below must be adjusted
            Assert.AreEqual(3, threshold);

            // assert
            Assert.AreEqual(0, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(0, receivedValuesFromBufferedSubscription.Count);

            // act
            subject.OnNext(1);

            // assert
            Assert.AreEqual(1, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(0, receivedValuesFromBufferedSubscription.Count);

            // act
            subject.OnNext(2);

            // assert
            Assert.AreEqual(2, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(0, receivedValuesFromBufferedSubscription.Count);

            // act
            subject.OnNext(3);

            // assert
            Assert.AreEqual(3, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(0, receivedValuesFromBufferedSubscription.Count);

            // act - this is the first value above the threshold 
            subject.OnNext(4);

            // assert            
            Assert.AreEqual(4, receivedValuesFromUnbufferedSubscription.Count);
            // the bufferred values are flushed to the subscriber
            Assert.AreEqual(4, receivedValuesFromBufferedSubscription.Count);

            // act - this is the second value above the threshold 
            subject.OnNext(5);

            // assert - now buffered and unbuffed should stay in sync
            Assert.AreEqual(5, receivedValuesFromUnbufferedSubscription.Count);            
            Assert.AreEqual(5, receivedValuesFromBufferedSubscription.Count);
        }

        [TestMethod]
        public void EventsAreQueuedUntilThresholdIsReachedAndThenRepeatTheSameBehaviourAfterTheBufferIsFlushed() {

            // arrange
            int threshold = 3;
            int bufferSize = threshold + 3;
            var receivedValuesFromUnbufferedSubscription = new List<int>();
            var receivedValuesFromBufferedSubscription = new List<int>();

            var subject = new Subject<int>();
            IObservable<int> observable = subject;

            Func<int, bool> triggerPredicate = i => {
                return i > threshold;
            };

            IObservable<int> bufferedObservable =
                observable.FlushOnTrigger<int>(triggerPredicate, bufferSize);

            var subcriberToUnbuffered = observable
                .Subscribe(i => receivedValuesFromUnbufferedSubscription.Add(i));

            var subcriberToBuffered = bufferedObservable
                .Subscribe(i => receivedValuesFromBufferedSubscription.Add(i));

            // act
            // do nothing!

            // assert - if threshold fails the test below must be adjusted
            Assert.AreEqual(3, threshold);

            // assert
            Assert.AreEqual(0, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(0, receivedValuesFromBufferedSubscription.Count);

            // act
            subject.OnNext(1);

            // assert
            Assert.AreEqual(1, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(0, receivedValuesFromBufferedSubscription.Count);

            // act
            subject.OnNext(2);

            // assert
            Assert.AreEqual(2, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(0, receivedValuesFromBufferedSubscription.Count);

            // act
            subject.OnNext(3);

            // assert
            Assert.AreEqual(3, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(0, receivedValuesFromBufferedSubscription.Count);

            // act - this is the first value above the threshold 
            subject.OnNext(4);

            // assert            
            Assert.AreEqual(4, receivedValuesFromUnbufferedSubscription.Count);
            // the bufferred values are flushed to the subscriber
            Assert.AreEqual(4, receivedValuesFromBufferedSubscription.Count);

            // now we repeat the sequence from the begining and we expect the 
            // same behavior where four items are flushed to the observer when
            // the threshold is passed again.            

            // act
            subject.OnNext(1);

            // assert
            Assert.AreEqual(5, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(4, receivedValuesFromBufferedSubscription.Count);

            // act
            subject.OnNext(2);

            // assert
            Assert.AreEqual(6, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(4, receivedValuesFromBufferedSubscription.Count);

            // act
            subject.OnNext(3);

            // assert
            Assert.AreEqual(7, receivedValuesFromUnbufferedSubscription.Count);
            Assert.AreEqual(4, receivedValuesFromBufferedSubscription.Count);

            // act - this causes to thread the threshold a second time
            subject.OnNext(4);

            // assert            
            Assert.AreEqual(8, receivedValuesFromUnbufferedSubscription.Count);
            // the bufferred values are flushed to the subscriber
            Assert.AreEqual(8, receivedValuesFromBufferedSubscription.Count);
        }
    }
}
