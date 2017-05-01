namespace LogXtreme.WinDsk.Infrastructure {
    public interface IShellService {

        void ShowShell(string uri=null);        

        int RegisteredShellCount { get; }

        int RegisterShellId();
    }
}
