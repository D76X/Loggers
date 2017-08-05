
namespace LogXtreme.WinDsk.Infrastructure.Services {

    public interface ICancelWork {

        void SignalCancellation();
        bool IsCancellationPending { get; }
    }
}
