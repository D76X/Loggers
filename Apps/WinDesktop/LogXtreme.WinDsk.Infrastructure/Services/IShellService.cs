using Prism.Regions;
using System.Windows;

namespace LogXtreme.WinDsk.Infrastructure.Services {

    public interface IShellService {

        DependencyObject CreateShell(IRegionManager regionManager=null);
        void ShowShell(DependencyObject shell, string uri = null);
        int ShellCreatedCount { get;  }
    }
}
