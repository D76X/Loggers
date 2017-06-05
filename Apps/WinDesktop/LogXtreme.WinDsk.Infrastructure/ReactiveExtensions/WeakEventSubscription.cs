using System;
using System.Reactive.Linq;

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

        const string ErrMsgWeakSubscriptionHanlderMustBeStatic = "The action that handle observable events subscribed by a weak reference must be static otherwise the refernce to the handler cannot be weak";

        /// <summary>
        /// Returns a IDisposable reference on a subscription to an IObservable<typeparamref name="TEventPattern"/> 
        /// and handler of type Action<typeparamref name="TEventPattern">. The subscription 
        /// </summary>
        /// <typeparam name="TEventPattern">The type of the values produced ny the IObservable<typeparamref name="TEventPattern"/></typeparam>
        /// <param name="observable">The IObservable<typeparamref name="TEventPattern"/> instance to subscribe to</param>
        /// <param name="onNext">The Action<T> to execute on any next value from the subscribed IObservable<typeparamref name="TEventPattern"/></param>
        /// <returns>The subscription as IDisposable</returns>
        public static IDisposable SubscribeWeakly<TEventPattern>(
            this IObservable<TEventPattern> observable,
            Action<TEventPattern> onNext) 
            where TEventPattern : class {

            IDisposable subscription = null;

            WeakSubscriberHelper<TEventPattern> subscriberHelper = new WeakSubscriberHelper<TEventPattern>(
                observable,
                ref subscription,
                actionOnNext: onNext);

            return subscription;
        }

        /// <summary>
        /// Helper class used to create a weak subscription to an IObservable<typeparamref name="TEventPattern"/>
        /// </summary>
        /// <typeparam name="TEventPattern">The type of the values produced ny the IObservable<typeparamref name="TEventPattern"/></typeparam>
        internal class WeakSubscriberHelper<TEventPattern> where TEventPattern : class {

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="observable">the IObservable<typeparamref name="TEventPattern"/> to create a wek subscription to</param>
            /// <param name="subscription">the reference to the weak subscription to create</param>
            /// <param name="actionOnNext">The action that is the handler for each value produced by the the IObservable<typeparamref name="TEventPattern"/></param>
            public WeakSubscriberHelper(
                IObservable<TEventPattern> observable,
                ref IDisposable subscription,
                Action<TEventPattern> actionOnNext) {

                // take a weak subscription to actionOnNext and use a static hanlder declared
                // on this class to execute actionOnNext with the emitted values from the 
                // observable
                subscription = observable.InternalSubscribeWeakly(
                    actionOnNext,
                    WeakSubscriberHelper<TEventPattern>.StaticEventHandler);
            }

            /// <summary>
            /// This static method executes the handler with the event value.
            /// </summary>
            /// <param name="handler">The hanlder to execute</param>
            /// <param name="eventPatternValue">The value to execute the hanlder with</param>
            public static void StaticEventHandler(
                Action<TEventPattern> handler,
                TEventPattern eventPatternValue) {

                handler(eventPatternValue);
            }
        }

        /// <summary>
        /// This class is used to create a weak refrence to a hanlder for the events from an instance of 
        /// IObservable<typeparamref name="TEventPattern"/>. 
        /// </summary>
        /// <typeparam name="TEventPattern">The type of an IObservable<typeparamref name="TEventPattern"/></typeparam>
        /// <typeparam name="TEvent">The type of an event handler</typeparam>
        /// <param name="observable">The IObservable<typeparamref name="TEventPattern"/></param>
        /// <param name="actionOnNext">The hanlder to take a weak reference on</param>
        /// <param name="onNextStaticHanlder">A static handler that proxies to the hanlder to which a weak refrence is held</param>
        /// <returns></returns>
        private static IDisposable InternalSubscribeWeakly<TEventPattern, TEvent>(
            this IObservable<TEventPattern> observable,
            TEvent actionOnNext,
            Action<TEvent, TEventPattern> onNextStaticHanlder) where TEvent : class {

            // The Target property of an action is null when the target is a static method on a class.
            // If the target property of the Action is not null then there exist an instance of a class 
            // to which a reference to a non static handler is held. In order to make sure that the caller
            // is not held in memory by a strong reference we must have a static handler.
            if (onNextStaticHanlder.Target != null) {
                throw new ArgumentException(ErrMsgWeakSubscriptionHanlderMustBeStatic);
            }

            // take a weak reference to the handler actionOnNext this refrence will not prevent the GC
            // from collecting the class instance on which actionOnNext exists.
            var weakReferenceToHandler = new WeakReference(actionOnNext);

            IDisposable subscription = null;

            subscription = observable.Subscribe( eventPatternValue => {

                // test the weak reference to the handler to see whether it is still in memory 
                // and capture it in the closure of this lamba expression so that it will not
                // die until the next value is sent to it by the static hanlder
                var handlerForNextValue = weakReferenceToHandler.Target as TEvent;

                if (handlerForNextValue != null) {

                    // the handler was still alive in memory thus send the emitted value over to the static handler 
                    // so that it will pass it as an argument to the hanlder designated by the caller
                    onNextStaticHanlder(handlerForNextValue, eventPatternValue);

                } else {

                    // the weak reference has not target so the instance of the class where the hanlder is declared
                    // has been already garbage colleced thus the subscription can be didposed of so that this lambda
                    // will no longer be executed.
                    subscription.Dispose();
                }                

            });

            return subscription;
        }

    }
}
