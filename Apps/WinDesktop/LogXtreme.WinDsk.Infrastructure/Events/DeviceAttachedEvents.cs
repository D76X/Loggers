using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace LogXtreme.WinDsk.Infrastructure.Events {

    public class Device { };
    
    public class DeviceAttachedEvent : PubSubEvent<Device> { };

}
