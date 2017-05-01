namespace LogXtreme.WinDsk.Infrastructure {
    public interface IShellService {

        void ShowShell();        

        int RegisteredShellCount { get; }

        int RegisterShellId();
    }
}
