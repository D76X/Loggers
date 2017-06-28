using System;
using System.Diagnostics.Tracing;

namespace SemanticLogging {

    /// <summary>
    /// Enforce the following rule
    /// 1-Use [Event(EventId)] to indicate the Id of the event
    /// 2-In WriteEvent the EventId must be used
    /// 3-Maintain the EventId in the order 
    /// 4-The order of the parameters in the methods signature is significant 
    /// 5-[Event(2 , Message = "The Key is {0}")] => {0} is for the first param
    /// 6-use capitalised Parameter Names to see them capitalised in the consumer
    /// 7-Level defaults to informational (Critical,Error, Informational, LogAlways, Verbose, Warning)
    /// 8-If you do not supply a Task the name of the Event becomes the name of the Task
    /// 9-If Task & Opcode are supplied the name of the event seen by consumers becomes Task-Opcode and the method name is ignored
    /// 10-If you supply only a Task but not the Opcode this is used as the name of the event by consumers and the method name is ignored
    /// 11-You can redefined the enums Task, Opcodes, Keywords as illustrated below - use nested classes and costs
    /// 12-Use Int32 or Int64 values consistently to redifined the enums i.e. 0x0001 for the Int32 value 1 but do not mix Inte32 and Int64
    /// 13-The event name shown in the manifest is the name of the event when the Task and opcode are not defined in the [Event] attribute 
    /// 14-If the [Event] attribute defines Task and Opcode the name of the event name shown in the manifest is the concatenation Task-Opcode
    /// 15-The standard Opcodes are Start & Stop - it is advised not to create new opcodes unless absolutely necessary
    /// 16-The only methods considered by ETW to create the manifest are those that return void!
    /// 17-It is possible to use support methods that do not return void in those that defines events
    /// 18-The access level of the methods return void that define events is irrelevant
    /// 19-Use the [NonEventAttribute] to identify the support methods
    /// </summary>
    [EventSource(Name = "NewThinkingTechnologies-LogXtreme-SimpleEventSource")]
    public sealed class SimpleEventSource : EventSource {

        private static Lazy<SimpleEventSource> log = new Lazy<SimpleEventSource>();

        private SimpleEventSource() { }

        public static SimpleEventSource Log => log.Value;
        
        [Event(1)]
        public void Message(string message) {

            WriteEvent(1, message);
        }

        [Event(2 , Message = "The Key is {0}", 
            Level = EventLevel.Informational,
            Keywords = Keywords.General | Keywords.Data,
            Task = Task.Selection,
            Opcode = Opcodes.George)]
        public void AccessByPrimaryKey(string PrimaryKey, string TableName) {

            WriteEvent(2, PrimaryKey, TableName);
        }

        [NonEvent]
        public void test1() { }

        [NonEvent]
        public int test2() { return 9; }

        /// <summary>
        /// Single-value enum
        /// </summary>
        public class Task {
            public const EventTask Selection = (EventTask)0x0001;
            public const EventTask Whatever = (EventTask)0x0002;
        }

        /// <summary>
        /// Single-value enum
        /// </summary>
        public class Opcodes {
            public const EventOpcode George = (EventOpcode)0x0001;
        }

        /// <summary>
        /// Flag enums - bitwise
        /// </summary>
        public class Keywords {
            public const EventKeywords General = (EventKeywords)0x0001;
            public const EventKeywords Assert = (EventKeywords)0x0002;
            public const EventKeywords Data = (EventKeywords)0x0003;
        }
    }
}
