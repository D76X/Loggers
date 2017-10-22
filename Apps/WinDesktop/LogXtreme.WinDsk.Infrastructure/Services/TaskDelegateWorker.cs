using System;
using System.Threading.Tasks;

namespace LogXtreme.WinDsk.Infrastructure.Services {

    /// <summary>
    /// Implementation of the Backgroundworker pattern based on TPL.
    /// </summary>    
    public class TaskDelegateWorker<TInput, TResult> :
        IDelegateWorker<TInput, TResult> {

        private Task<TResult> task;

        public TaskDelegateWorker() {

        }

        public TaskDelegateWorker(
           bool supportReportProgress = false,
           bool supportCancellation = false) {

        }

        public void Process(
        Func<TInput, IDelegateWorkerResult<TResult>> toExecute,
        Action<IDelegateWorkerResult<TResult>> onComplete,
        Action<IDelegateWorkerResult<TResult>> onCancelled) {

            throw new NotImplementedException();
        }

        public void StartProcess(TInput initialInput) {

            this.task.Start();
        }
    }
}
