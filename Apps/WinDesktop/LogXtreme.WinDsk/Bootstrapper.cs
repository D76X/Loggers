using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;

namespace LogXtreme.WinDsk {
    public class Bootstrapper: UnityBootstrapper {

        protected override DependencyObject CreateShell() {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;                
            Application.Current.MainWindow.Show();
        }
    }
}
