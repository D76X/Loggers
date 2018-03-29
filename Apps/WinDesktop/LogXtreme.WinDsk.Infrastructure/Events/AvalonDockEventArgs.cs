using LogXtreme.Ifrastructure.Enums;
using LogXtreme.Infrastructure.ContractValidators;
using System;
using System.Collections.Specialized;
using Xceed.Wpf.AvalonDock.Layout;

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
            //object sender,
            NotifyCollectionChangedEventArgs args,
            AvalonDockEventEnum eventType) {

            //this.Sender = sender;
            this.NotifyCollectionChangedEventArgs = args;
            this.EventType = eventType;
        }
    }

    /// <summary>
    /// A Prism pub-sub event to be used with its EventAggregator.
    /// PubSubEvent<T> is used for intermodule communication.
    /// Refs
    /// http://prismlibrary.github.io/docs/wpf/Communication.html
    /// </summary>
    //public class AvalonDockEventArgs : EventArgs {

    //    public readonly LayoutContent Content;
    //    public readonly NotifyCollectionChangedAction EventType;

    //    public AvalonDockEventArgs(
    //        LayoutContent content,
    //        NotifyCollectionChangedAction eventType) {

    //        content.Validate(nameof(content)).NotNull();
    //        eventType.Validate(nameof(eventType)).NotNull();

    //        this.Content = content;
    //        this.EventType = eventType;
    //    }
    //}
}
