using System;
using System.Diagnostics.Tracing;

namespace SemanticLogging {

    /// <summary>
    /// Flag enums - bitwise
    /// </summary>
    public class Keywords {
        public const EventKeywords Info = (EventKeywords)0x0001;
        public const EventKeywords Assert = (EventKeywords)0x0002;
        public const EventKeywords Data = (EventKeywords)0x0003;
    }

    [EventSource(Name = "NewThinkingTechnologies-LogXtreme-LoggerEventSource")]
    public sealed class LoggerEventSource : EventSource {

        private static Lazy<LoggerEventSource> logger = new Lazy<LoggerEventSource>();
        private LoggerEventSource() { }
        public static LoggerEventSource Logger => logger.Value;

        [Event(1, Level = EventLevel.Informational)]
        public void LogInfo(string message, string source, object value) {

            if (true) {
                WriteEvent(1, message, source, value);
            }            
        }

        [Event(1, Level = EventLevel.Warning)]
        public void LogWarning(string message, string source, object value) {

            if (true) {
                WriteEvent(1, message, source, value);
            }
        }

        [Event(1, Level = EventLevel.Error)]
        public void LogError(string message, string source, object value) {

            if (true) {
                WriteEvent(1, message, source, value);
            }
        }

        [Event(1, Level = EventLevel.Critical)]
        public void LogCritical(string message, string source, object value) {

            if (true) {
                WriteEvent(1, message, source, value);
            }
        }

        [Event(1, Level = EventLevel.LogAlways)]
        public void LogAlways(string message, string source, object value) {

            if (true) {
                WriteEvent(1, message, source, value);
            }
        }

        [Event(1, Level = EventLevel.Verbose)]
        public void LogVerbose(string message, string source, object value) {

            if (true) {
                WriteEvent(1, message, source, value);
            }
        }
    }
}
