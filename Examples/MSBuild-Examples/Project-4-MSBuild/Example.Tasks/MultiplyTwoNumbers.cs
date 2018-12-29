using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Example.Tasks
{
    // One way to create a custom MSBuld task is to provide an iplemetation of ITask. 
    // However it is also possible to procede by implementing the abstract class 
    // Microsoft.Build.Utilities.Task that lives in the referenced assemby 
    // Microsoft.Build.Utilities.v4.0. You still need to also reference the assembly 
    // Microsoft.Build.Framework as some of the attributes used on the property of the
    // task are defined there.

    public class MultiplyTwoNumbers : Task
    {
        public override bool Execute()
        {
            try
            {
                Result = Number1 * Number2;

                // The MSBuild runtime is equipped with its own logging.
                // The abstract Task class adds its own implementations.

                // simplified logging - there are other logging methods as well
                Log.LogMessage(
                    message: $"{nameof(AddTwoNumbers)} : {nameof(Result)}={Result}={Number1}*{Number2}={nameof(Number1)}+{nameof(Number2)}",
                    messageArgs: null);

                // instead of                
                //BuildEngine.LogMessageEvent(
                //    new BuildMessageEventArgs(
                //        message: $"{nameof(AddTwoNumbers)} : {nameof(Result)}={Result}={Number1}*{Number2}={nameof(Number1)}+{nameof(Number2)}",
                //        helpKeyword: $"helpKeyword-{nameof(AddTwoNumbers)}",
                //        senderName: $"sendername-{nameof(AddTwoNumbers)}",
                //        importance: MessageImportance.Normal));

                // true or false to notify the MSBuild runtime on the outcome of the Task.
                return true;
            }
            catch (System.Exception e)
            {
                // an example of the simplification built-in the base class Task.
                Log.LogErrorFromException(e);

                // instead of
                //BuildEngine.LogMessageEvent(new BuildMessageEventArgs(
                //        message: $"exception in {nameof(AddTwoNumbers)} : {e.Message} : {nameof(Result)}={Result}={Number1}*{Number2}={nameof(Number1)}+{nameof(Number2)}",
                //        helpKeyword: $"helpKeyword-{nameof(AddTwoNumbers)}",
                //        senderName: $"sendername-{nameof(AddTwoNumbers)}",
                //        importance: MessageImportance.High));

                throw;
            }
        }

        [Required]
        public double Number1 { get; set; }

        [Required]
        public double Number2 { get; set; }

        // The Output attribute marks the result of the Task.
        [Output]
        public double Result { get; set; }
    }
}
