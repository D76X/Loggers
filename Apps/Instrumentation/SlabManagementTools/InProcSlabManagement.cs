using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;
using System.Diagnostics.Tracing;

namespace SlabManagementTools {

    public class InProcSlabManagement {

        private static ObservableEventListener listener = new ObservableEventListener();
        private static EventSource eventSource;

        public static void StartInProcTracing(EventSource source) {

            if (eventSource != null) {
                throw new InvalidOperationException(@"Only a single event source can be started");
            }

            eventSource = source;
            listener.EnableEvents(eventSource, EventLevel.LogAlways, Keywords.All);
            listener.LogToConsole();
        }

        public static void StopInProcTracing() {

            if (eventSource == null) { return; }

            listener.DisableEvents(eventSource);
            listener.Dispose();
            eventSource.Dispose();
        }
    }
}
