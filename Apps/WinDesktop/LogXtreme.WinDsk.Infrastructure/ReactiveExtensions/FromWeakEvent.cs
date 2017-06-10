using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.Infrastructure.ReactiveExtensions {

    public static partial class ObservableEx {

        /// <summary>
        /// Create an IObservable for a source of events of type <typeparamref name="TEventSource"/> 
        /// with event arguments of type EventArgs. The caller can register an event handler with 
        /// to receive the stream of events. The source of events will not be kept in memory by the 
        /// listener because the listener register with the IObservable and the IObservable only 
        /// maintain a weak reference to the source.
        /// </summary>
        /// <typeparam name="TEventSource"></typeparam>
        /// <returns></returns>
        public static IObservable<EventArgs> FromWeakEvent<TEventSource>(
            TEventSource eventSource,
            Action<TEventSource, EventHandler> addHanlder,
            Action<TEventSource, EventHandler> removeHandler
            ) where TEventSource : class {

            return Observable.Create<EventArgs>(observer => {

                var disposable = new CompositeDisposable();

                var observable = new WeakEventForwarder<EventArgs, TEventSource>(
                    eventSource,
                    addHanlder,
                    removeHandler,
                    observer);

                disposable.Add(observable);
                return disposable;

            });
        }
    }
}
