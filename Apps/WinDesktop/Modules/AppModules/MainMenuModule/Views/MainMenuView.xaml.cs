using LogXtreme.WinDsk.Infrastructure.Models;
using MainMenuModule.Interfaces;
using System.Windows.Controls;

namespace MainMenuModule.Views {
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl, IMainMenuView {

        public MainMenuView(IMainMenuViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get {
                return (IMainMenuViewModel)this.DataContext;
            }

            set {
                this.DataContext = value;
            }
        }
    }
}
