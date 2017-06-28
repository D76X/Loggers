using System.Windows;

namespace LogXtreme.WinDsk {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {

            base.OnStartup(e);

#if DEBUG
            SlabManagementTools.InProcSlabManagement.StartInProcTracing(SemanticLogging.LoggerEventSource.Logger);
#endif
            SemanticLogging.LoggerEventSource.Logger.LogInfo(@"log started", nameof(App), nameof(App.OnStartup));

            SemanticLogging.LoggerEventSource.Logger.LogAlways(@"log test always", nameof(App), nameof(App.OnStartup));
            SemanticLogging.LoggerEventSource.Logger.LogCritical(@"log test critical", nameof(App), nameof(App.OnStartup));
            SemanticLogging.LoggerEventSource.Logger.LogError(@"log test  error", nameof(App), nameof(App.OnStartup));
            SemanticLogging.LoggerEventSource.Logger.LogVerbose(@"log test verbose", nameof(App), nameof(App.OnStartup));
            SemanticLogging.LoggerEventSource.Logger.LogWarning(@"log test warning", nameof(App), nameof(App.OnStartup));

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        protected override void OnExit(ExitEventArgs e) {

#if DEBUG
            SemanticLogging.LoggerEventSource.Logger.LogInfo(@"log stopped ", nameof(App), nameof(App.OnExit));
            SlabManagementTools.InProcSlabManagement.StopInProcTracing();
#endif

            base.OnExit(e);
        }
    }
}
