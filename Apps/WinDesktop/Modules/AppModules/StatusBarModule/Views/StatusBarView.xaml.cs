using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Prism;
using LogXtreme.WinDsk.StatusBarModule.Interfaces;
using Prism.Regions;
using System.Windows.Controls;

namespace LogXtreme.WinDsk.StatusBarModule.Views {
    /// <summary>
    /// Interaction logic for StatusBarView.xaml
    /// </summary>
    public partial class StatusBarView : 
        UserControl, 
        IStatusBarView,
        IRegionManagerAware {

        IRegionManager regionManager;

        public StatusBarView(IStatusBarViewModel viewModel) {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IViewModel ViewModel {
            get => (IStatusBarViewModel)this.DataContext;
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