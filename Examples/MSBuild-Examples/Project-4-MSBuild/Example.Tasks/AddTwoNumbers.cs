using Microsoft.Build.Framework;

namespace Example.Tasks
{
    public class AddTwoNumbers : ITask
    {
        // this property can be used to gain access to the MSBuild runtime
        public IBuildEngine BuildEngine {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        // ?
        public ITaskHost HostObject {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        // the entry point used by the MSBuild runtime at the invokation of the 
        // custom task. The return value of the ITask.Execute method is a bool
        // and it is intended as a flag to the MSBuild runtime on the outcome of
        // the task execution and not as a retun value of the task. Use a property
        // of the implementing class to provide a result if required.
        public bool Execute()
        {
            try
            {
                Result = Number1 + Number2;

                // The MSBuild runtime is equipped with its own logging.
                BuildEngine.LogMessageEvent(
                    new BuildMessageEventArgs(
                        message:$"{nameof(AddTwoNumbers)} : {nameof(Result)}={Result}={Number1}+{Number2}={nameof(Number1)}+{nameof(Number2)}",
                        helpKeyword:$"helpKeyword-{nameof(AddTwoNumbers)}",
                        senderName:$"sendername-{nameof(AddTwoNumbers)}",
                        importance: MessageImportance.Normal));

                // true or false to notify the MSBuild runtime on the outcome of the Task.
                return true;
            }
            catch (System.Exception e)
            {
                BuildEngine.LogMessageEvent(new BuildMessageEventArgs(
                        message: $"exception in {nameof(AddTwoNumbers)} : {e.Message} : {nameof(Result)}={Result}={Number1}+{Number2}={nameof(Number1)}+{nameof(Number2)}",
                        helpKeyword: $"helpKeyword-{nameof(AddTwoNumbers)}",
                        senderName: $"sendername-{nameof(AddTwoNumbers)}",
                        importance: MessageImportance.High));
                throw;
                
            }            
        }

        // Custom properties on the implemeting class can be used as needed.
        // Attributes are used to specify the semantic of the properties in respect of the MSBuild engine.

        [Required]
        public double Number1 { get; set; }

        [Required]
        public double Number2 { get; set; }

        // The Output attribute marks the result of the Task.
        [Output]
        public double Result { get; set; }
    }
}
