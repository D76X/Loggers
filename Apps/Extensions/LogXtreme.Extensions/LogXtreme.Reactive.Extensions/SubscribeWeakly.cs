using System;
using System.Reactive.Linq;

namespace LogXtreme.Reactive.Extensions {

    public static partial class ObservableEx {

        /// <summary>
        /// This extension methods creates a IDisposable as a IObservable subscription
        /// which runs a static hanlder which invokes an event handler on an instance of
        /// a class to which only a weak reference is retained. This extension method is 
        /// normally used together withe the extension method Observable.FromEvent or any
        /// of its variants.
        /// 
        /// Refs
        /// http://blog.functionalfun.net/2012/03/weak-events-in-net-easy-way.html
        /// 
        /// </summary>
        /// <typeparam name="TEventPattern">Type for any derived class from EventPattern</typeparam>
        /// <typeparam name="TTarget">Tye of the class that provides the event hanlder for the event</typeparam>
        /// <param name="observable">The object extension</param>
        /// <param name="target">The class instance that holds the hanlder for the event</param>
        /// <param name="onNext">The event hanlder to subscribe weakly</param>
        /// <returns></returns>
        public static IDisposable SubscribeWeakly<TEventPattern, TTarget>(
            this IObservable<TEventPattern> observable,
            TTarget target,
            Action<TTarget, TEventPattern> onNext) 
            where TTarget : class 
            where TEventPattern : class {

            IDisposable subscription = null;

            // only a weak reference is retained to the class that provides 
            // the hanlder, thus if this weak reference is the only reference
            // left the target will be collected and the subscription disposed of.
            var weakReferenceToTarget = new WeakReference(target);           

            // create a closure to a static handler
            subscription = observable.Subscribe(eventPattern => {

                var currentTarget = weakReferenceToTarget.Target as TTarget;

                if (currentTarget != null) {
                    StaticHandler(currentTarget, eventPattern, onNext);
                }
                else {
                    subscription.Dispose();
                }
            });

            return subscription;
        }

        public static void StaticHandler<TTarget, TEventPattern>(
            TTarget target,
            TEventPattern eventPattern,
            Action<TTarget, TEventPattern> onNextHandler) {

            onNextHandler(target, eventPattern);
        }
    }
}
