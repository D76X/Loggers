using System;

namespace LogXtreme.WinDsk.Infrastructure.Services {

    public interface IProgressWorker {

        void ProgressChange(Action<int> onProgressChanged);
        void ReportProgress(int progress);        
    }
}
