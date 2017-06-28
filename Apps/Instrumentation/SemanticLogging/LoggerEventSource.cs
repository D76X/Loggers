using System;
using System.Diagnostics.Tracing;

namespace SemanticLogging {
    
    /// <summary>
    /// Keywords are indipendent and orthogonal
    /// </summary>
    public class Keywords {
        public const EventKeywords DATA = (EventKeywords)0x0001;
        public const EventKeywords COMMUNICATION = (EventKeywords)0x0002;
        public const EventKeywords USB = (EventKeywords)0x0003;
        public const EventKeywords ETHERNET = (EventKeywords)0x0004;
        public const EventKeywords TCPIP = (EventKeywords)0x0005;
        public const EventKeywords LICENCE = (EventKeywords)0x0006;
        public const EventKeywords REGISTRY = (EventKeywords)0x0007;
        public const EventKeywords CONFIG = (EventKeywords)0x0008;
        public const EventKeywords INITIALIZATION = (EventKeywords)0x0009;
        public const EventKeywords PERFORMANCE = (EventKeywords)0x0010;
        public const EventKeywords OPTIMIZATION = (EventKeywords)0x0011;
        public const EventKeywords EXPIRED = (EventKeywords)0x0012;
        public const EventKeywords TIME = (EventKeywords)0x0013;
        public const EventKeywords DATE = (EventKeywords)0x0014;
        public const EventKeywords CONNECTION = (EventKeywords)0x0014;
        public const EventKeywords LOST = (EventKeywords)0x0015;
        public const EventKeywords MADE = (EventKeywords)0x0016;
        public const EventKeywords FAILED = (EventKeywords)0x0017;
        public const EventKeywords TRIED = (EventKeywords)0x0018;
        public const EventKeywords SENT = (EventKeywords)0x0019;
        public const EventKeywords RECEIVED = (EventKeywords)0x0020;
        public const EventKeywords CHANGED = (EventKeywords)0x0021;
        public const EventKeywords ENABLED = (EventKeywords)0x0022;
        public const EventKeywords DISABLED = (EventKeywords)0x0023;
    }

    [EventSource(Name = "NewThinkingTechnologies-LogXtreme-LoggerEventSource")]
    public sealed class LoggerEventSource : EventSource {  

        private static Lazy<LoggerEventSource> logger = new Lazy<LoggerEventSource>();
        private LoggerEventSource() { }
        public static LoggerEventSource Logger => logger.Value;

        [Event(1, Level = EventLevel.Informational)]
        public void LogInfo(string message, string source, object value) {

            if (IsEnabled()) {
                WriteEvent(1, message, source, value);
            }            
        }

        [Event(2, Level = EventLevel.Warning)]
        public void LogWarning(string message, string source, object value) {

            if (IsEnabled()) {
                WriteEvent(2, message, source, value);
            }
        }

        [Event(3, Level = EventLevel.Error)]
        public void LogError(string message, string source, object value) {

            if (IsEnabled()) {
                WriteEvent(3, message, source, value);
            }
        }

        [Event(4, Level = EventLevel.Critical)]
        public void LogCritical(string message, string source, object value) {

            if (IsEnabled()) {
                WriteEvent(4, message, source, value);
            }
        }

        [Event(5, Level = EventLevel.LogAlways)]
        public void LogAlways(string message, string source, object value) {

            if (IsEnabled()) {
                WriteEvent(5, message, source, value);
            }
        }

        [Event(6, Level = EventLevel.Verbose)]
        public void LogVerbose(string message, string source, object value) {

            if (IsEnabled()) {
                WriteEvent(6, message, source, value);
            }
        }
    }
}
