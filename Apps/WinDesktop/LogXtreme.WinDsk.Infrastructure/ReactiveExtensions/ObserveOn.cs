using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;

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

        const string ErrMsgExpressionIsNotProperty = @"The specified expression does not reference a property.";
        const string ErrMsgNoPropertyInfoForMemberExpression = @"The specified member expression does not have property information.";

        /// <summary>
        /// Extension method for implemetations of INotifyPropertyChanged to convert a source of 
        /// INotifyPropertyChanged events into an IObservable of EventPattern with payload 
        /// PropertyChangedEventArgs.
        /// </summary>
        /// <typeparam name="TSource">The type of the INotifyPropertyChanged implemetation to observe</typeparam>
        /// <typeparam name="TProperty">The type of the property to observe</typeparam>
        /// <param name="source">reference to an instance of INotifyPropertyChanged to observe</param>
        /// <param name="propertyExpression">Typically the lamba expression : source => source.Property</param>
        /// <returns></returns>
        public static IObservable<EventPattern<PropertyChangedEventArgs>> ObserveOn<TSource, TProperty>(
        this TSource source,
        Expression<Func<TSource, TProperty>> propertyExpression)
        where TSource : INotifyPropertyChanged {

            Contract.Requires(source != null);
            Contract.Requires(propertyExpression != null);

            var expressionBody = propertyExpression.Body as MemberExpression;

            if (expressionBody == null) {
                throw new ArgumentException(ErrMsgExpressionIsNotProperty);
            }

            var propertyInfo = expressionBody.Member as PropertyInfo;

            if (propertyInfo == null) {
                throw new ArgumentException(ErrMsgNoPropertyInfoForMemberExpression);
            }

            string propertyName = propertyInfo.Name;

            // http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html#FromEvent
            // Observable.FromEventPattern<TEvent, TEventArgs>
            // 1st parameter : how to convert the event from EventHandler<TEventArgs> to TEvent
            // 2nd parameter : what to do when IObservable.Subscribe(delegate) is invoked
            // 3rd parameter : what to do when IObservable.Dispose() is invoked 
            return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                h => h.Invoke,
                handler => source.PropertyChanged += handler,
                handler => source.PropertyChanged -= handler)
                .Where(e => e.EventArgs.PropertyName == propertyName);
        }

        /// <summary>
        /// Extension method for implemetations of INotifyCollectionChanged to convert a source of 
        /// INotifyCollectionChanged events into an IObservable of EventPattern with payload 
        /// NotifyCollectionChangedEventArgs.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IObservable<EventPattern<NotifyCollectionChangedEventArgs>> ObserveOn(this INotifyCollectionChanged collection) {

            // http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html#FromEvent
            // Observable.FromEventPattern<TEvent, TEventArgs>
            // 1st parameter : how to convert the event from EventHandler<TEventArgs> to TEvent
            // 2nd parameter : what to do when IObservable.Subscribe(delegate) is invoked
            // 3rd parameter : what to do when IObservable.Dispose
            return Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                handler => (sender, e) => handler(sender, e),
                handler => collection.CollectionChanged += handler,
                handler => collection.CollectionChanged -= handler);
        }
    }
}
