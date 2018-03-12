using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using System.Windows;

namespace LogXtreme.WinDsk.TestDocking.Prism {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window, IShellView {

        public Shell(IShellViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get {
                return (IShellViewModel)this.DataContext;
            }

            set {
                this.DataContext = value;
            }
        }
    }
}
