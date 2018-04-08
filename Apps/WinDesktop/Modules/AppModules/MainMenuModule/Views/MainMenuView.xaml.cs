using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.MainMenuModule.Interfaces;
using Prism.Regions;
using System.Windows.Controls;

namespace LogXtreme.WinDsk.MainMenuModule.Views {
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : 
        UserControl, 
        IMainMenuView,
        IRegionManagerAware {

        private IRegionManager regionManager;

        public MainMenuView(IMainMenuViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get => (IMainMenuViewModel)this.DataContext;
            set => this.DataContext = value;
        }

        public IRegionManager RegionManager {

            get => this.regionManager;

            set {
                if (this.regionManager != null) { return; }
                this.regionManager = value;
            }
        }
    }
}
