using System;
using System.Diagnostics.Tracing;

/// <summary>
/// Refs
/// https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx#sec9
/// https://stackoverflow.com/questions/23101017/why-is-my-eventsource-not-logging
/// https://dzimchuk.net/troubleshooting-slab-out-of-process-logging/
/// </summary>
namespace SemanticLogging {    

    [EventSource(Name = "NewThinkingTechnologies-LogXtreme-ApplicationEventSource")]   
    public sealed class ApplicationEventSource : EventSource {

        /// <summary>
        /// 1-There are a bunch of default keywords but in general you want to define application 
        /// specific keywords.
        /// 2-Each keyword value is a 64-bit integer, which is treated as a bit array enabling you 
        /// to define up to 64 different keywords.
        /// 3-Keywords are indipendent and orthogonal.
        /// </summary>
        public class Keywords {
            public const EventKeywords DATA = (EventKeywords)1;
            public const EventKeywords COMMUNICATION = (EventKeywords)2;
            public const EventKeywords USB = (EventKeywords)4;
            public const EventKeywords ETHERNET = (EventKeywords)8;
            public const EventKeywords TCPIP = (EventKeywords)16;
            public const EventKeywords LICENCE = (EventKeywords)32;
            public const EventKeywords REGISTRY = (EventKeywords)64;
            public const EventKeywords CONFIG = (EventKeywords)128;
            public const EventKeywords INITIALIZATION = (EventKeywords)256;
            public const EventKeywords PERFORMANCE = (EventKeywords)512;
            public const EventKeywords OPTIMIZATION = (EventKeywords)1024;
            public const EventKeywords EXPIRED = (EventKeywords)2048;
            public const EventKeywords TIME = (EventKeywords)4096;
            public const EventKeywords DATE = (EventKeywords)8192;
            public const EventKeywords CONNECTION = (EventKeywords)16384;
            public const EventKeywords LOST = (EventKeywords)32768;
            public const EventKeywords MADE = (EventKeywords)65536;
            public const EventKeywords FAILED = (EventKeywords)131072;
            public const EventKeywords TRIED = (EventKeywords)262144;
            public const EventKeywords SENT = (EventKeywords)524288;
            public const EventKeywords RECEIVED = (EventKeywords)1048576;
            public const EventKeywords CHANGED = (EventKeywords)2097152;
            public const EventKeywords ENABLED = (EventKeywords)4194304;
            public const EventKeywords DISABLED = (EventKeywords)8388608;
            public const EventKeywords PRISM = (EventKeywords)16777216;
            public const EventKeywords NAVIGATION = (EventKeywords)33554432;
            public const EventKeywords ACCESS = (EventKeywords)67108864;
            public const EventKeywords READ = (EventKeywords)134217728;
            public const EventKeywords WRITE = (EventKeywords)268435456;
            public const EventKeywords FILE = (EventKeywords)536870912;
        }

        /// <summary>
        /// Singletone pattern in .NET 4 or higher
        /// Refs
        /// http://csharpindepth.com/Articles/General/Singleton.aspx
        /// </summary>
        private static Lazy<ApplicationEventSource> logger = new Lazy<ApplicationEventSource>(() => new ApplicationEventSource());
        private ApplicationEventSource() { }
        public static ApplicationEventSource Logger =>logger.Value;

        [Event(1, Level = EventLevel.Informational)]
        public void LogInfo(string message, string source, string value) {

            if (IsEnabled()) {
                WriteEvent(1, message, source, value);
            }            
        }

        [Event(2, Level = EventLevel.Warning)]

        public void LogWarning(string message, string source, string value) {

            if (IsEnabled()) {
                WriteEvent(2, message, source, value);
            }
        }

        [Event(3, Level = EventLevel.Error)]
        public void LogError(string message, string source, string value) {

            if (IsEnabled()) {
                WriteEvent(3, message, source, value);
            }
        }

        [Event(4, Level = EventLevel.Critical)]
        public void LogCritical(string message, string source, string value) {

            if (IsEnabled()) {
                WriteEvent(4, message, source, value);
            }
        }

        [Event(5, Level = EventLevel.LogAlways)]
        public void LogAlways(string message, string source, string value) {

            if (IsEnabled()) {
                WriteEvent(5, message, source, value);
            }
        }

        [Event(6, Level = EventLevel.Verbose)]
        public void LogVerbose(string message, string source, string value) {

            if (IsEnabled()) {
                WriteEvent(6, message, source, value);
            }
        }
    }
}
