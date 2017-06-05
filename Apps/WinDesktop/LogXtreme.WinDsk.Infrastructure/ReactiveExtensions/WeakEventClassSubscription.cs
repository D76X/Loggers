using System;

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
        /// The same as the version without the weak class parameter but with the added 
        /// feature that the caller can pass a reference to an instance of a class on 
        /// which the handler is defined without having to hold a reference to it. The
        /// instance of the class on which the hanlder is defined can carry on with its
        /// own indipended lifecycle which may be determined by other factors however 
        /// such that it will not depend by the fact that it is the class instance 
        /// bearing the designated hanlder of the events emitted by the observable. 
        /// </summary>
        /// <typeparam name="TEventPattern"></typeparam>
        /// <typeparam name="TWeakClass"></typeparam>
        /// <param name="observable"></param>
        /// <param name="weakClass"></param>
        /// <param name="onNext"></param>
        /// <returns></returns>
        public static IDisposable SubscribeWeakly<TEventPattern, TWeakClass>(
            this IObservable<TEventPattern> observable,
            TWeakClass weakClass,
            Action<TEventPattern> onNext)
            where TEventPattern : class
            where TWeakClass : class {

            IDisposable subscription = null;

            WeakClassSubscriberHelper<TEventPattern> SubscriptionHelper =
                new WeakClassSubscriberHelper<TEventPattern>(
                    observable,
                    weakClass,
                    ref subscription,
                    onNext);

            return subscription;
        }

        private class WeakClassSubscriberHelper<TEventPattern> where TEventPattern : class {

            public WeakClassSubscriberHelper(
                IObservable<TEventPattern> observable,
                object weakClass,
                ref IDisposable subscription,
                Action<TEventPattern> eventAction) {

                subscription = observable.InternalSubscribeWeaklyToClass(
                    eventAction,
                    weakClass,
                    WeakClassSubscriberHelper<TEventPattern>.StaticEventHandler);
            }

            public static void StaticEventHandler(
                Action<TEventPattern> subscriber,
                TEventPattern item) {

                subscriber(item);
            }
        }
       
        private static IDisposable InternalSubscribeWeaklyToClass<TEventPattern, TEvent, TClass>(
            this IObservable<TEventPattern> observable,
            TEvent actionOnNext, 
            TClass weakClass,
            Action<TEvent, TEventPattern> onNext)
            where TEvent : class
            where TClass : class {

            if (onNext.Target != null)
                throw new ArgumentException(ErrMsgWeakSubscriptionHanlderMustBeStatic);

            // The class instance could live in a differnt place than the eventhandler. If either one is null,
            // terminate the subscribtion.
            var weakReferenceToClassInstance = new WeakReference(weakClass);
            var weakReferenceToHanlder = new WeakReference(actionOnNext);

            IDisposable subscription = null;
            subscription = observable.Subscribe(item => {

                var currentWeakClass = weakReferenceToClassInstance.Target as TClass;
                var handlerForNextValue = weakReferenceToHanlder.Target as TEvent;

                if (currentWeakClass != null && handlerForNextValue != null) {
                    onNext(handlerForNextValue, item);
                } else {
                    subscription.Dispose();
                }
            });

            return subscription;
        }
    }
}
