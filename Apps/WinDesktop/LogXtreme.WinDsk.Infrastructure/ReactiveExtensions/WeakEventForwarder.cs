using System;

namespace LogXtreme.WinDsk.Infrastructure.ReactiveExtensions {

    internal class WeakEventForwarder<TEventArgs, TEventSource> : IDisposable {

        //private WeakEventHandler weakEventHanlder;
        private IObserver<TEventArgs> observer;

        public WeakEventForwarder(
            TEventSource eventSource,
            Action<TEventSource, EventHandler> subscribeHandler,
            Action<TEventSource, EventHandler> unsubscribeHandler,
            IObserver<EventArgs> observer) {

            
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}
