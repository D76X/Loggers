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
    /// BackgroundWorker basic implementation - with no progress reporting and calcellation
    /// 
    /// Methods: RunWorkerAsync 
    /// Events: DoWork, RunWorkerCompleted
    /// 
    /// BackgroundWorker with reporting
    /// 
    /// Methods: ReportProgress
    /// Properties: WorkerReportsProgress
    /// Events: ProgressChanged
    /// 
    /// BackgroundWorker with cancellation
    /// 
    /// </summary>
    public class BackgroundDelegateWorker<TInput, TResult> :
        IDelegateWorker<TInput, TResult>,
        IProgressWorker {
        
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
            Func<TInput, TResult> toExecute,
            Action<TResult> onComplete) {
            
            this.InitialiseWorker(toExecute, onComplete);            
        }

        public void StartProcess(TInput initialInput) {
            this.bw.RunWorkerAsync(initialInput);
        }

        private void InitialiseWorker(
            Func<TInput, TResult> toExecute,
            Action<TResult> onComplete) {

            this.bw.DoWork += (s, e) => {                

                if (toExecute != null) {
                    e.Result = toExecute((TInput)e.Argument);                    
                }
            };

            this.bw.RunWorkerCompleted += (s, e) => {
                onComplete?.Invoke((TResult)e.Result);
            };            
        }        
    }    
}
