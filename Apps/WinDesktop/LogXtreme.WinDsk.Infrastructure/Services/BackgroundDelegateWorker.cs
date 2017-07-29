using System;
using System.ComponentModel;

namespace LogXtreme.WinDsk.Infrastructure.Services {

    /// <summary>
    /// Implements IDelegateWorker using .NET Backgroundworker
    /// 
    /// Refs
    /// https://stackoverflow.com/questions/14734997/how-to-unit-test-backgroundworker-prism-interactionrequest
    /// </summary>
    public class BackgroundDelegateWorker : IDelegateWorker {

        private BackgroundWorker bw;

        public BackgroundDelegateWorker() {

            this.bw = new BackgroundWorker();
        }

        public void Start<TInput, TResult>(
            Func<TInput, TResult> onStart,
            Action<TResult> onComplete,
            TInput param) {

            this.InitialiseWorker(onStart, onComplete);

            this.bw.RunWorkerAsync(param);
        }

        private void InitialiseWorker<TInput, TResult>(
            Func<TInput, TResult> onStart,
            Action<TResult> onComplete) {

            this.bw.DoWork += (s, e) => {
                if (onStart != null) {
                    e.Result = onStart((TInput)e.Argument);
                }
            };

            this.bw.RunWorkerCompleted += (s, e) => {
                onComplete?.Invoke((TResult)e.Result);
            };
        }
    }    
}
