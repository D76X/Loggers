using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;

namespace LogXtreme.WinDsk.Infrastructure.ReactiveExtensions {

    /// <summary>
    /// References
    /// Weak events in .NET using Reactive Extensions (Rx)
    /// https://www.codeproject.com/Tips/1078183/Weak-events-in-NET-using-Reactive-Extensions-Rx
    /// Weak Events in .NET, The Easy Way
    /// https://dzone.com/articles/weak-events-net-easy-way
    /// IObservable from events
    /// http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html#FromEvent
    /// PropertyChangedEventHandler Delegate
    /// https://msdn.microsoft.com/en-us/library/system.componentmodel.propertychangedeventhandler(v=vs.110).aspx
    /// NotifyCollectionChangedEventHandler Delegate
    /// https://msdn.microsoft.com/en-us/library/system.collections.specialized.notifycollectionchangedeventhandler(v=vs.110).aspx 
    /// </summary>
    public static partial class ObservableEx {

        /// <summary>
        /// Return a IDisposable reference on a subscription to an IObservable<T> and handler of type Action<typeparamref name="T">.
        /// </summary>
        /// <typeparam name="T">The type of the values produced ny the IObservable<typeparamref name="T"/></typeparam>
        /// <param name="observable">The IObservable<typeparamref name="T"/> instance to subscribe to</param>
        /// <param name="onNext">The Action<T> to execute on any next value from the subscribed IObservable<typeparamref name="T"/></param>
        /// <returns>The subscription as IDisposable</returns>
        public static IDisposable SubscribeWeakly<T>(
            this IObservable<T> observable,
            Action<T> onNext
            ) where T : class {

            IDisposable subscription = null;

            WeakSubscriberHelper<T> subscriberHelper = new WeakSubscriberHelper<T>(
                observable, 
                ref subscription, 
                eventActionOnNext: onNext);

            return subscription;
        }

        /// <summary>
        /// Helper class used to create a weak subscription to an IObservable<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the values produced ny the IObservable<typeparamref name="T"/></typeparam>
        internal class WeakSubscriberHelper<T> where T : class {

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="observable">the IObservable<typeparamref name="T"/> to create a wek subscription to</param>
            /// <param name="subscription">the reference to the weak subscription to create</param>
            /// <param name="eventActionOnNext">The action that is the handler for each value produced by the the IObservable<typeparamref name="T"/></param>
            public WeakSubscriberHelper(
                IObservable<T> observable,
                ref IDisposable subscription,
                Action<T> eventActionOnNext) {

                subscription = observable.InternalSubscribeWeakly(
                    eventActionOnNext, 
                    WeakSubscriberHelper<T>.StaticEventHandler);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="subscriber"></param>
            /// <param name="item"></param>
            public static void StaticEventHandler(
                Action<T> subscriber, 
                T item) {

                subscriber(item);
            }
        }

        private static IDisposable InternalSubscribeWeakly<TEventPattern, TEvent>(
            this IObservable<TEventPattern> observable,
            TEvent WeakOnNext,
            Action<TEvent, TEventPattern> onNextStaticHanlder) where TEvent : class {

            // The Target property of an action is null when the traget is a static method on a class.
            // If the target property of the Action is not null then the
            if (onNextStaticHanlder.Target != null) {

            }

        }

    }
}
