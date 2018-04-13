using LogXtreme.Infrastructure.ContractValidators;
using System;

namespace LogXtreme.Reactive.Extensions {

    /// <summary>
    /// Basic implementation of <see cref="IObserver"/><T> 
    /// Refs
    /// https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-implement-an-observer
    /// </summary>
    public class Reporter<T> : 
        IObserver<T>, 
        IDisposable {

        private IDisposable subscription;
        private Action<T> onNext;
        private Action onCompleted;
        private Action<Exception> onError;

        public Reporter(
            Action<T> onNext, 
            Action onCompleted,
            Action<Exception> onError) {

            this.onNext = onNext;
            this.onCompleted = onCompleted;
            this.onError = onError;
        }

        public virtual void Subscribe(IObservable<T> observable) {

            observable.Validate(nameof(observable)).NotNull();

            this.subscription = observable.Subscribe(this);
        }

        public virtual void Unsubscribe() {
            if (this.subscription == null) { return; }
            this.subscription.Dispose();
        }

        public virtual void OnCompleted() {
            if(this.onCompleted == null) { return; }
            this.onCompleted();
        }

        public virtual void OnError(Exception error) {
            if (this.onError == null) { return; }
            this.onError(error);
        }

        public virtual void OnNext(T value) {
            if (this.onNext == null) { return; }
            this.onNext(value);
        }

        #region IDisposable

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {

            if (disposing) {
                
                // dispose of subcriptions, etc.   
                this.subscription?.Dispose();
                this.subscription = null;
            }
        }

        #endregion
    }
}
