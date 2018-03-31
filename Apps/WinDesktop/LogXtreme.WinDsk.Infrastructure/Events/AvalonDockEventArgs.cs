using LogXtreme.Ifrastructure.Enums;
using LogXtreme.Infrastructure.ContractValidators;
using System;
using System.Collections.Specialized;
namespace LogXtreme.WinDsk.Infrastructure.Events {

    /// <summary>
    /// A Prism pub-sub event to be used with its EventAggregator.
    /// PubSubEvent<T> is used for intermodule communication.
    /// Refs
    /// http://prismlibrary.github.io/docs/wpf/Communication.html
    /// </summary>
    public class AvalonDockEventArgs : EventArgs {

        //public readonly object Sender;
        public readonly NotifyCollectionChangedEventArgs NotifyCollectionChangedEventArgs;
        public readonly AvalonDockEventEnum EventType;

        public AvalonDockEventArgs(
            NotifyCollectionChangedEventArgs args,
            AvalonDockEventEnum eventType) {

            args.Validate(nameof(args)).NotNull();
            eventType.Validate(nameof(eventType)).NotNull();

            this.NotifyCollectionChangedEventArgs = args;
            this.EventType = eventType;
        }
    }
}
