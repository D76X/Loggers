using LogXtreme.WinDsk.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace LogXtreme.WinDsk.Modules.Services {

    [Module(ModuleName = nameof(DeviceServiceModule))]
    public class DeviceServiceModule : IModule {

        private readonly IUnityContainer container;

        public DeviceServiceModule(IUnityContainer container) {
            this.container = container;
        }

        public void Initialize() {

            // Register types
            // Here we register the service(s) with the container.
            this.container.RegisterType<IDeviceService, DeviceService>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            // service modules normally do not register region names and do not need
            // to take a reference to the Prism region manager.
        }
    }
}