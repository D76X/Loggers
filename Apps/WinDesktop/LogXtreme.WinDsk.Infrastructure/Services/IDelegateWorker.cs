using System;

namespace LogXtreme.WinDsk.Infrastructure.Services
{
    /// <summary>   
    /// Refs
    /// https://stackoverflow.com/questions/14734997/how-to-unit-test-backgroundworker-prism-interactionrequest
    /// </summary>
    public interface IDelegateWorker<TInput, TResult> {
        
        void Process(Func<TInput, TResult> toExecute, Action<TResult> onComplete);        
        void StartProcess(TInput initialInput);
    }
}
