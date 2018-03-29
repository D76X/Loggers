using LogXtreme.Ifrastructure.Interfaces;
using Prism.Events;

namespace LogXtreme.WinDsk.Infrastructure.Events {

    /// <summary>
    /// A Prism pub-sub event to be used with its EventAggregator.
    /// PubSubEvent<T> is used for intermodule communication.
    /// Refs
    /// http://prismlibrary.github.io/docs/wpf/Communication.html
    /// </summary>
    public class DeviceEvent : PubSubEvent<IDevice> { };
}
