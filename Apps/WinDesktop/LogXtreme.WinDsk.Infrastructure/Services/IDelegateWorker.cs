using System;

namespace LogXtreme.WinDsk.Infrastructure.Services
{
    public interface IDelegateWorkerResult<T> {

        T Result { get; }
        bool Cancelled { get; set; }
    }

    public class DelegateWorkerResult<T> : IDelegateWorkerResult<T> {

        public DelegateWorkerResult(T result) {
            this.Result = result;
        }

        public T Result { get; }

        public bool Cancelled { get; set; }
    }

    /// <summary>   
    /// Refs
    /// https://stackoverflow.com/questions/14734997/how-to-unit-test-backgroundworker-prism-interactionrequest
    /// </summary>
    public interface IDelegateWorker<TInput, TResult> {
        
        void Process(
            Func<TInput, IDelegateWorkerResult<TResult>> toExecute, 
            Action<IDelegateWorkerResult<TResult>> onComplete,
            Action<IDelegateWorkerResult<TResult>> onCancelled);

        void StartProcess(TInput initialInput);
    }
}
