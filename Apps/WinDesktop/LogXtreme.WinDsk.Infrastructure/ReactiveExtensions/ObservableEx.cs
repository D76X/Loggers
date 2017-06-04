using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.Infrastructure.ReactiveExtensions {

    public static class ObservableEx {

        /// <summary>
        /// Convert a source of INotifyPropertyChanged events into an IObservable of EventPattern
        /// with payload PropertyChangedEventArgs.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="collection"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public static IObservable<EventPattern<PropertyChangedEventArgs>> ObserveOn<TSource, TProperty>(
            this TSource collection,
            Expression<Func<TSource, TProperty>> propertyExpression) 
            where TSource : INotifyPropertyChanged {

            Contract.Requires(collection != null);
            Contract.Requires(propertyExpression != null);

            var body = propertyExpression as MemberExpression;

            if (body == null) {
                throw new ArgumentException(@"The specified expression does not reference a property.");
            }


        }
    }
}
