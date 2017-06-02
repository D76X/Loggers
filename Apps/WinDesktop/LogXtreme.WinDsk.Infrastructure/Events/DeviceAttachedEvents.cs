using Prism.Events;

namespace LogXtreme.WinDsk.Infrastructure.Events {

    public class Device { };
    
    public class DeviceAttachedEvent : PubSubEvent<Device> { };

}
