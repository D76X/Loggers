using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;
using System.Diagnostics.Tracing;

namespace SlabManagementTools {

    /// <summary>
    /// This class sets up a listener that implements IObservable and which is used to receive
    /// the event messages in-process. The same listener then is  
    /// </summary>
    public class SlabManager {

        private static EventSource eventSource;

        // define any number of listeners you may see fit
        private static ObservableEventListener listener = new ObservableEventListener();        

        /// <summary>
        /// Setups the in-process tracing.
        /// </summary>
        /// <param name="source"></param>
        public static void StartInProcTracing(EventSource source) {

            if (eventSource != null) {
                throw new InvalidOperationException(@"Only a single event source can be started");
            }

            eventSource = source;

            // set up the listeners by specifying the following 
            // 1-their event sources 
            // 2-the condition under which an event from the source is let to the listener 
            // The set-up rules may be defined in the App.config of the consumer code instead of being hard coded!
            listener.EnableEvents(eventSource, EventLevel.LogAlways, Keywords.All);

            // connect the sinks to each of the event listeners.

            // here the listener is connetted to the console sink using the available extension method.
            // there are other sinks Console, SQL DB, File flat & rolling, Azure tables.
            // In order to get the extension methods to create the other kinds of sinks you must use the 
            // corresponding NuGet packages.
            listener.LogToConsole();          
        }

        /// <summary>
        /// Stops the in-process tracing.
        /// </summary>
        public static void StopInProcTracing() {

            if (eventSource == null) { return; }

            listener.DisableEvents(eventSource);
            listener.Dispose();
            eventSource.Dispose();
        }
    }
}
