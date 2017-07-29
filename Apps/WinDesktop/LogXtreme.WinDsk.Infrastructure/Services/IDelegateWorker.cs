
using System;

namespace LogXtreme.WinDsk.Infrastructure.Services
{
    /// <summary>
    /// Refs
    /// https://stackoverflow.com/questions/14734997/how-to-unit-test-backgroundworker-prism-interactionrequest
    /// </summary>
    public interface IDelegateWorker {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="onStart">Comparable to BackgroundWorker.DoWork</param>
        /// <param name="onComplete">Comparable to BackgroundWorker.RunWorkerCompleted</param>
        /// <param name="param">The initial value to pass into the onStart delegate or null</param>
        void Start<TInput, TResult>(Func<TInput, TResult> onStart, Action<TResult> onComplete, TInput param);

    }
}
