using System.Windows;

namespace LogXtreme.WinDsk {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        public App() {

            // this will not be caught in process!
            SemanticLogging.ApplicationEventSource.Logger.LogInfo(@"application", nameof(App), @"constructor");
        }

        protected override void OnStartup(StartupEventArgs e) {

            // this will not be caught in-process
            SemanticLogging.ApplicationEventSource.Logger.LogInfo(@"before call to base base.OnStartup", nameof(App), nameof(App.OnStartup));

            base.OnStartup(e);

#if DEBUG

            // this line setups and starts the in-process tracing.
            // the in-process tracing is going to be available only when debugging and not in production.
            SlabManagementTools.SlabManager.StartInProcTracing(SemanticLogging.ApplicationEventSource.Logger);

            // these lines show how to log basic (semantic) messages by reffering the EventSource.
            // these lines are not going to be present in production.
            SemanticLogging.ApplicationEventSource.Logger.LogInfo(@"test-info", nameof(App), nameof(App.OnStartup));
            SemanticLogging.ApplicationEventSource.Logger.LogAlways(@"test-always", nameof(App), nameof(App.OnStartup));
            SemanticLogging.ApplicationEventSource.Logger.LogCritical(@"test-critical", nameof(App), nameof(App.OnStartup));
            SemanticLogging.ApplicationEventSource.Logger.LogError(@"test-error", nameof(App), nameof(App.OnStartup));
            SemanticLogging.ApplicationEventSource.Logger.LogVerbose(@"test-verbose", nameof(App), nameof(App.OnStartup));
            SemanticLogging.ApplicationEventSource.Logger.LogWarning(@"test-warning", nameof(App), nameof(App.OnStartup));
#endif
            SemanticLogging.ApplicationEventSource.Logger.LogInfo(@"before Bootstrapper ctor", nameof(App), nameof(App.OnStartup));
            var bootstrapper = new Bootstrapper();
            SemanticLogging.ApplicationEventSource.Logger.LogInfo(@"after Bootstrapper ctor", nameof(App), nameof(App.OnStartup));

            SemanticLogging.ApplicationEventSource.Logger.LogInfo(@"before Bootstrapper bootstrapper.Run", nameof(App), nameof(App.OnStartup));
            bootstrapper.Run();
        }

        protected override void OnExit(ExitEventArgs e) {

            SemanticLogging.ApplicationEventSource.Logger.LogInfo(@"before teardown", nameof(App), nameof(App.OnExit));
#if DEBUG   
            // In production there isn't any in-process listeners to tear down. 
            // this line tears down the in-process tracing.
            SlabManagementTools.SlabManager.StopInProcTracing();
#endif

            base.OnExit(e);
        }
    }
}
