using System.Diagnostics.Tracing;

namespace SemanticLogging {

    [EventSource(Name = "NewThinkingTechnologies-LogXtreme-SimpleEventSource")]
    public sealed class SimpleEventSource : EventSource {

        public static SimpleEventSource Log = new SimpleEventSource();
        
        public void Message(string message) {

            WriteEvent(1, message);
        }

        public void AccessByPrimaryKey(string primaryKey, string tableName) {

            WriteEvent(2, primaryKey, tableName);
        }
    }
}
