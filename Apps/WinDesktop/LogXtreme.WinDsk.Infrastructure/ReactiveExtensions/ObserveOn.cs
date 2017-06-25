using LogXtreme.WinDsk.Infrastructure.Expressions;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.Infrastructure.ReactiveExtensions {

    /// <summary>
    /// References
    /// Weak events in .NET using Reactive Extensions (Rx)
    /// https://www.codeproject.com/Tips/1078183/Weak-events-in-NET-using-Reactive-Extensions-Rx
    /// Weak Events in .NET, The Easy Way
    /// https://dzone.com/articles/weak-events-net-easy-way
    /// IObservable from events
    /// http://www.introtorx.com/content/v1.0.10621.0/04_CreatingObservableSequences.html#FromEvent
    /// Observing Property changes
    /// https://github.com/LeeCampbell/RxCookbook/blob/master/Model/PropertyChange.md
    /// PropertyChangedEventHandler Delegate
    /// https://msdn.microsoft.com/en-us/library/system.componentmodel.propertychangedeventhandler(v=vs.110).aspx
    /// NotifyCollectionChangedEventHandler Delegate
    /// https://msdn.microsoft.com/en-us/library/system.collections.specialized.notifycollectionchangedeventhandler(v=vs.110).aspx 
    /// </summary>
    public static partial class ObservableEx {

        /// <summary>
        /// Extension method for implemetations of INotifyPropertyChanged to convert a source of 
        /// INotifyPropertyChanged events into an IObservable of EventPattern with a payload of 
        /// type PropertyChangedEventArgs and by using an expression such as item => item.Property
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
            
            string propertyName = propertyExpression.ToPropertyName();

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
        /// INotifyCollectionChanged events into an IObservable of EventPattern with payload of type
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
