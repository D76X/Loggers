using System;
using System.ComponentModel;

namespace LogXtreme.WinDsk.Infrastructure.Services {

    /// <summary>
    /// 
    /// Implements IDelegateWorker using .NET Backgroundworker
    /// Implements IProgressWorker using .NET Backgroundworker
    /// 
    /// Refs
    /// https://stackoverflow.com/questions/14734997/how-to-unit-test-backgroundworker-prism-interactionrequest
    /// 
    /// BackgroundWorker breakdown
    /// 
    /// Methods: RunWorkerAsync, ReportProgress, CancelAsync
    /// Properties: WorkerReportsProgress, WorkerSupportCancellation, CancellationPending, IsBusy
    /// Events: DoWork, RunWorkerCompleted, ProgressChanged
    /// 
    /// 1-BackgroundWorker basic implementation - with no progress reporting and calcellation
    /// 
    /// Methods: RunWorkerAsync 
    /// Events: DoWork, RunWorkerCompleted
    /// 
    /// 2-BackgroundWorker with reporting
    /// 
    /// Methods: ReportProgress
    /// Properties: WorkerReportsProgress
    /// Events: ProgressChanged
    /// 
    /// 3-BackgroundWorker with cancellation
    /// 
    /// Methods: CancelAsync
    /// Properties: WorkersSupportCancellation, CancellationPending (readonly)
    /// Event: RunworkerCompleted
    /// 
    /// </summary>
    public class BackgroundDelegateWorker<TInput, TResult> :
        IDelegateWorker<TInput, TResult>,
        IProgressWorker,
        ICancelWork {
        
        private BackgroundWorker bw;        

        public BackgroundDelegateWorker() {

            this.bw = new BackgroundWorker();
        }

        public BackgroundDelegateWorker(
            bool supportReportProgress = false,
            bool supportCancellation = false) {

            this.bw = new BackgroundWorker();
            this.bw.WorkerReportsProgress = supportReportProgress;
            this.bw.WorkerSupportsCancellation = supportCancellation;
        }

        public bool IsCancellationPending => this.bw.CancellationPending;

        public void SignalCancellation() {
            this.bw.CancelAsync();
        }

        public void ReportProgress(int progress) {

            if (!this.bw.WorkerReportsProgress) {
                return;
            }

            this.bw.ReportProgress(progress);
        }

        public void ProgressChange(Action<int> onProgressChanged) {

            this.bw.ProgressChanged += (s, e) => {

                if (this.bw.WorkerReportsProgress && onProgressChanged != null) {
                    onProgressChanged(e.ProgressPercentage);
                }
            };
        }

        public void Process(
            Func<TInput, IDelegateWorkerResult<TResult>> toExecute,
            Action<IDelegateWorkerResult<TResult>> onComplete = null,
            Action<IDelegateWorkerResult<TResult>> onCancelled = null) {
            
            this.InitialiseWorker(toExecute, onComplete, onCancelled);            
        }

        public void StartProcess(TInput initialInput) {
            this.bw.RunWorkerAsync(initialInput);
        }

        private void InitialiseWorker(
            Func<TInput, IDelegateWorkerResult<TResult>> toExecute,
            Action<IDelegateWorkerResult<TResult>> onComplete,
            Action<IDelegateWorkerResult<TResult>> onCancelled) {

            // in this implementation of IDelegateWorker<TInput, TResult>
            // which is based on BackgroundWorker the real type of the 
            // arguments in the lambdas are as below

            // s : BackgroundWorker 
            // e : DoWorkEventArgs
            this.bw.DoWork += (s, e) => {                

                if (toExecute != null) {
                    e.Result = toExecute((TInput)e.Argument);                    
                }
            };

            // s : BackgroundWorker 
            // e : RunWorkerCompletedEventArgs
            this.bw.RunWorkerCompleted += (s, e) => {

                // Contrary to the standard implementation of BackgroundWorker
                // in this implementation the e.Result is always accessible 
                // even when the process has been cancelled because the caller
                // cannot access e : RunWorkerCompletedEventArgs and set the 
                // property e.Canceled to true. When e.Canceled = true accessing
                // the e.Result throws but in this case we retain cancellation 
                // logic and access to the partial resullt in the onCancelled 
                // delegate if we wish to.

                var result = (IDelegateWorkerResult<TResult>)e.Result;

                if(result.Cancelled) {                    
                    onCancelled?.Invoke(result);
                } else {
                    onComplete?.Invoke(result);
                }                                
            };            
        }        
    }    
}
