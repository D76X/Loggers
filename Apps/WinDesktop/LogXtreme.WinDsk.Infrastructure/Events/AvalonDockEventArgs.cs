using LogXtreme.Ifrastructure.Enums;
using LogXtreme.Infrastructure.ContractValidators;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Events {

    /// <summary>
    /// A Prism pub-sub event to be used with its EventAggregator.
    /// PubSubEvent<T> is used for intermodule communication.
    /// Refs
    /// http://prismlibrary.github.io/docs/wpf/Communication.html
    /// </summary>
    public class AvalonDockEventArgs : EventArgs {

        public readonly object Sender;
        public readonly AvalonDockEventEnum EventType;

        public AvalonDockEventArgs(
            object sender,
            AvalonDockEventEnum eventType) {

            sender.Validate(nameof(sender)).NotNull();
            eventType.Validate(nameof(eventType)).NotNull();

            this.Sender = sender;
            this.EventType = eventType;
        }
    }
}
