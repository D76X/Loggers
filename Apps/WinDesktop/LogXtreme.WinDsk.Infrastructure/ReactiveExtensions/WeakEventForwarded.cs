using System;

namespace LogXtreme.WinDsk.Infrastructure.ReactiveExtensions {

    internal class WeakEventForwarded<TEventArgs, TEventSource> : IDisposable {

        private WeakEventHandler weakEventHanlder;
        private IObserver<TEventArgs> observer;

        public WeakEventForwarded(
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
