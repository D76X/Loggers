﻿using LogXtreme.Infrastructure.ContractValidators;
using System;
using System.Collections.Generic;

namespace LogXtreme.Reactive.Extensions {

    /// <summary>
    /// Basic implementation of <see cref="IObservable"/><T>.
    /// Refs
    /// https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-implement-a-provider
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Provider<T> : IObservable<T> {

        /// <summary>
        /// 
        /// </summary>
        private class DisposableSubscription : IDisposable {

            private List<IObserver<T>> observers;
            private IObserver<T> observer;

            public DisposableSubscription(
                IObserver<T> observer,
                List<IObserver<T>> observers) {

                observer.Validate(nameof(observer)).NotNull();
                observers.Validate(nameof(observers)).NotNull();

                this.observer = observer;
                this.observers = observers;
            }

            #region IDisposable

            public void Dispose() {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing) {

                if (disposing) {

                    // dispose of subcriptions, etc.   
                    if (this.observer == null) { return; }
                    this.observers.Remove(this.observer);
                }
            }

            #endregion
        }

        private List<IObserver<T>> observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer) {

            if (!this.observers.Contains(observer)) {
                this.observers.Add(observer);
            }

            return new DisposableSubscription(observer, this.observers);
        }

        public void Emit(T value) {

            foreach (var observer in this.observers) {
                observer.OnNext(value);
            }
        }

        public void Complete() {

            foreach (var observer in this.observers) {
                observer.OnCompleted();
            }
        }

        public void Error(Exception e) {

            foreach (var observer in this.observers) {
                observer.OnError(e);
            }
        }
    }
}
