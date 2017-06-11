using LogXtreme.WinDsk.Infrastructure.Events;
using System;

namespace LogXtreme.WinDsk.Infrastructure.ReactiveExtensions {

    internal class WeakEventForwarder<TEventArgs, TEventSource> : IDisposable 
        where  TEventArgs : EventArgs 
        where TEventSource : class {

        private WeakEventHanlder weakEventHanlder;
        private IObserver<TEventArgs> observer;

        public WeakEventForwarder(
            TEventSource eventSource,
            Action<TEventSource, EventHandler> subscribeHandler,
            Action<TEventSource, EventHandler> unsubscribeHandler,
            IObserver<EventArgs> observer) {

            this.observer = (IObserver<TEventArgs>)observer;

            var listeningObject = this;

            // get a weak reference to a listener object
            // the listener is this object
            // do not pass *this* in the last delegate because
            // it would crate a strong reference to this object
            // from the delegate by closure, pass *me* instead
            // so that a static method 
            this.weakEventHanlder = WeakEventHanlder.Register(
                eventSource,
                subscribeHandler,
                unsubscribeHandler,
                listeningObject,
                (me, sender, args) => me.OnNext((TEventArgs)args));
        }

        private void OnNext(TEventArgs args) {
            this.observer.OnNext(args);
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}
