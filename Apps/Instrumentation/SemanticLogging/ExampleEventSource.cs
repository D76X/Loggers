using System;
using System.Diagnostics.Tracing;

namespace SemanticLogging {

    /// <summary>
    /// Enforce the following rule.
    /// 1-Use [Event(EventId)] to indicate the Id of the event.
    /// 2-In WriteEvent the EventId must be used.
    /// 3-Maintain the EventId in the order. 
    /// 4-The order of the parameters in the methods signature is significant.
    /// 5-[Event(2 , Message = "The Key is {0}")] => {0} is for the first param.
    /// 6-Use capitalised parameter names to see them capitalised in the consumer i.e. PrimaryKey.
    /// 7-Level defaults to informational but Critical, Error, Informational, LogAlways, Verbose, Warning are also available.
    /// 8-If you do not supply a Task the name of the Event becomes the name of the Task in the manifest.
    /// 9-If Task & Opcode are supplied the name of the event seen by consumers and specified in the manifest becomes Task-Opcode and the method name is ignored.
    /// 10-If you supply only a Task but not the Opcode this is used as the name of the event by consumers and the method name is ignored.
    /// 11-You can redefined the enums Task, Opcodes, Keywords as illustrated below - use nested classes and constants.
    /// 12-Use Int32 or Int64 values consistently to redifined the enums i.e. 0x0001 for the Int32 value 1 but do not mix Inte32 and Int64.
    /// 13-The event name shown in the manifest is the name of the event when the Task and opcode are not defined in the [Event] attribute. 
    /// 14-If the [Event] attribute defines Task and Opcode the name of the event name shown in the manifest is the concatenation Task-Opcode.
    /// 15-The standard Opcodes are Start & Stop - it is advised not to create new opcodes unless absolutely necessary.
    /// 16-The only methods considered by ETW to create the manifest are those that return void all other methods are ignored.
    /// 17-It is possible to use support methods that do not return void in those that defines events
    /// 18-The access level of the methods which return void (private, protected, internal, public) is not relevant to the creation of the manifest.
    /// 19-Use the [NonEventAttribute] to identify the support methods available on the EventSource i.e. public methods which may use private methods.
    /// 20-By using public methods on the EventSource descendant any ad'hoc public API can be provided to the consumer code. 
    /// 21-Do not start the numbering of events in an EventSource with 0 because 0 is reserved to events that signal problems in the ETW trancing itself.
    /// 22-Minimise the number of event sources because there is a set-up cost. Ideally a sinlge EventSource class may be defined with all the semantic events you need.
    /// 23-Call the IsEnabled() methods in your EventSource methods to keep the application load as low as possible and do preprocessing only after this call.
    /// 24-Avoid the WriteEvent overload that has a signature with the (params object[]) for its last parameter as it is much heavier than the other available overloads.
    /// 25-There are a bunch of default keywords but in general you want to define application specific keywords.
    /// </summary>
    [EventSource(Name = "NewThinkingTechnologies-LogXtreme-ExampleEventSource")]
    public sealed class ExampleEventSource : EventSource {

        private static Lazy<ExampleEventSource> log = new Lazy<ExampleEventSource>();

        private ExampleEventSource() { }

        public static ExampleEventSource Log => log.Value;
        
        [Event(1)]
        public void Message(string message) {

            if (IsEnabled()) {
                WriteEvent(1, message);
            }            
        }

        [Event(2 , Message = "The Key is {0}", 
            Level = EventLevel.Informational,
            Keywords = Keywords.General | Keywords.Data,
            Task = Task.Selection,
            Opcode = Opcodes.George)]
        public void AccessByPrimaryKey(string PrimaryKey, string TableName) {

            if (IsEnabled()) {
                WriteEvent(2, PrimaryKey, TableName); 
            }
        }

        [NonEvent]
        public void test1() { }

        [NonEvent]
        public int test2() { return 9; }

        /// <summary>
        /// Single-value enum. There are no predefined Tasks.
        /// </summary>
        public class Task {
            public const EventTask Selection = (EventTask)0x0001;
            public const EventTask Whatever = (EventTask)0x0002;
        }

        /// <summary>
        /// Single-value enum.
        /// Normally you do not want to defined your own opt-code because 
        /// consumers would not understand them. Instad you should use the
        /// predefined op-codes.
        /// </summary>
        public class Opcodes {
            public const EventOpcode George = (EventOpcode)0x0001;
        }

        /// <summary>
        /// Flag enums can be combined with the bitwise | operator.
        /// You normally want to define application specific keywords.
        /// </summary>
        public class Keywords {
            public const EventKeywords General = (EventKeywords)0x0001;
            public const EventKeywords Assert = (EventKeywords)0x0002;
            public const EventKeywords Data = (EventKeywords)0x0003;
        }
    }
}
