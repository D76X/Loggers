using LogXtreme.DataStructures;
using System;
using System.Reactive.Linq;

namespace LogXtreme.Reactive.Extensions {

    public static partial class ObservableEx {

        /// <summary>
        /// Extension method that allows buffering of events from an <see cref="IObservable"/><typeparamref name="T"/>.
        /// The consumer code provides a predicate that expresses the condition on which point the buffered events are 
        /// flushed to the subscriber. The consumer provides also the size of the internal circular buffer used to store
        /// the events. 
        /// Refs
        /// https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx#sec9
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable">The observable providing the events to buffer</param>
        /// <param name="triggerPredicate">The predicate that triggers the buffer flush</param>
        /// <param name="bufferSize">The size of the events buffer</param>
        /// <returns></returns>
        public static IObservable<T> FlushOnTrigger<T>(
            this IObservable<T> observable,
            Func<T, bool> triggerPredicate,
            int bufferSize) {

            // given any observer 
            // create a buffer
            // and queue the events in the buffer
            // until the trigger is true
            // keep only the last bufferSize events in the queue
            // when the event is enqueued the observer.OnNext is not invoked
            // when the trigger becomes true all the enqueued events are passed to the observer OnNext
            Func<IObserver<T>, IDisposable> subscribe = observer => {

                var buffer = new CircularBuffer<T>(bufferSize);

                var subscription = observable.Subscribe(item => {

                    if (triggerPredicate(item)) {

                        foreach (var bufferedItem in buffer) {

                            observer.OnNext(bufferedItem);
                        }

                        observer.OnNext(item);

                    } else {

                        buffer.Enqueue(item);
                    }
                },
                observer.OnError,
                observer.OnCompleted); 

                return subscription;
            };

            return Observable.Create<T>(subscribe);
        }
    }
}
